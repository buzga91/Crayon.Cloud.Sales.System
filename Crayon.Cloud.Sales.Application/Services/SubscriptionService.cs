using Crayon.Cloud.Sales.Application.Contracts;
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
            var result = await _softwareService.GetAvailableSoftwareServices();
            if (!result.IsSuccess) return Result<IEnumerable<AvailableSoftwareDTO>>.Failure(result.Error);
            return result;
        }
        public async Task<Result> CancelSubscription(int subscriptionId)
        {
            var result = await _subscriptionRepository.ChangeSubscriptionState(subscriptionId, licenseState.Canceled.ToString());
            if (!result.IsSuccess) return Result.Failure(result.Error);
            return Result.Success();
        }

        public async Task<Result> ExtendSubscriptionValidDate(int subscriptionId, DateTime newValidTo)
        {
            var result = await _subscriptionRepository.ExtendSubscriptionValidationTime(subscriptionId, newValidTo);
            if (!result.IsSuccess) return Result.Failure(result.Error);
            return Result.Success();
        }

        public async Task<Result> ChangeSubscriptionQuantity(int subscriptionId, int newQuantity)
        {
            var result = await _subscriptionRepository.ChangeSubscriptionQuantity(subscriptionId, newQuantity);
            if (!result.IsSuccess) return Result.Failure(result.Error);
            return Result.Success();
        }

        //public async Task<Result<IEnumerable<SubscriptionDTO>>> GetSubscriptionsForEachAccountId()
        //{
        //    var subscriptions = await _subscriptionRepository.GetSubscriptionsByAccountId();
        //    if (!subscriptions.IsSuccess) return Result<IEnumerable<SubscriptionDTO>>.Failure(subscriptions.Error);

        //    var domainSubscription = SubscriptionExtensions.ToDtoCollection(subscriptions.Value);
        //    return Result<IEnumerable<SubscriptionDTO>>.Success(domainSubscription);
        //}

        public async Task<Result<SubscriptionDTO>> ProvisionSubscription(Subscription subscription)
        {
            var availableSubscription = await _softwareService.GetAvailableSoftwareServicesById(subscription.SoftwareId);
            if (!availableSubscription.IsSuccess) return Result<SubscriptionDTO>.Failure(availableSubscription.Error);

            var accountEntity = await _accountRepository.GetAccountById(subscription.AccountId);
            if (!accountEntity.IsSuccess) return Result<SubscriptionDTO>.Failure(accountEntity.Error);

            var customer = await _customerRepository.GetCustomerById(accountEntity.Value.Id);
            if (!customer.IsSuccess) return Result<SubscriptionDTO>.Failure(customer.Error);

            var softwareDTO = SoftwareExtensions.ToCcpProvisionDto(subscription);
            var result = await _softwareService.ProvisionSoftware(softwareDTO);

            if (!result.IsSuccess) return Result<SubscriptionDTO>.Failure(result.Error);

            subscription.MaxQuantity = availableSubscription.Value.MaxQuantity;
            subscription.MinQuantity = availableSubscription.Value.MinQuantity;

            var subscriptionEntity = SubscriptionExtensions.ToEntity(subscription, accountEntity.Value);

            await _subscriptionRepository.Add(subscriptionEntity);
            var subsctiptionDto = SubscriptionExtensions.ToDto(subscriptionEntity);

            return Result<SubscriptionDTO>.Success(subsctiptionDto);
        }
    }
}
