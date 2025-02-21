using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Domain.Exceptions;
using Crayon.Cloud.Sales.Domain.Extensions;
using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Extensions;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISoftwareService _softwareService;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IAccountRepository accountRepository, ICustomerRepository customerRepository, ISoftwareService softwareService)
        {
            _subscriptionRepository = subscriptionRepository;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _softwareService = softwareService;
        }
        public async Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwaresFromCCP()
        {
            return await _softwareService.GetAvailableSoftwareServices();
        }
        public async Task<Result> CancelSubscription(int subscriptionId)
        {
            await _subscriptionRepository.ChangeSubscriptionState(subscriptionId, licenseState.Canceled.ToString());
            return Result.Success();
        }

        public async Task<Result> ExtendSubscriptionValidDate(int subscriptionId, DateTime newValidTo)
        {
            await _subscriptionRepository.ExtendSubscriptionValidationTime(subscriptionId, newValidTo);
            return Result.Success();
        }

        public async Task<Result> ChangeSubscriptionQuantity(int subscriptionId, int newQuantity)
        {
            await _subscriptionRepository.ChangeSubscriptionQuantity(subscriptionId, newQuantity);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<Subscription>>> GetSubscriptionsForSpecificAccount(int accountId)
        {
            var subscriptions = await _subscriptionRepository.GetSubscriptionsByAccountId(accountId);
            if (!subscriptions.IsSuccess) throw new SubscriptionException(subscriptions.Error);
            var domainSubscription = SubscriptionExtensions.ToDomainCollection(subscriptions.Value);
            return Result<IEnumerable<Subscription>>.Success(domainSubscription);
        }

        public async Task<Result<Subscription>> ProvisionSubscription(Subscription subscription)
        {
            var availableSubscription = await _softwareService.GetAvailableSoftwareServicesById(subscription.SoftwareId);
            if(!availableSubscription.IsSuccess) return Result<Subscription>.Failure(availableSubscription.Error);

            var accountEntity = await _accountRepository.GetAccountById(subscription.AccountId);
            if (!accountEntity.IsSuccess) throw new AccountException(accountEntity.Error);
            var customer = await _customerRepository.GetCustomerById(accountEntity.Value.Id);
            if (!customer.IsSuccess) throw new CustomerException(customer.Error);

            var softwareDTO = SoftwareExtensions.ToCcpProvisionDto(subscription, customer.Value.CustomerCcpId);
            var result = await _softwareService.ProvisionSoftware(softwareDTO);

            if (!result.IsSuccess) throw new SubscriptionException(result.Error);

            subscription.MaxQuantity = availableSubscription.Value.MaxQuantity;
            subscription.MinQuantity = availableSubscription.Value.MinQuantity;

            var subscriptionEntity = SubscriptionExtensions.ToEntity(subscription, accountEntity.Value);

            await _subscriptionRepository.Add(subscriptionEntity);
            return Result<Subscription>.Success(subscription);
        }
    }
}
