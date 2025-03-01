﻿using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Shared;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Application.Contracts
{
    public interface ISubscriptionService
    {
        Task<Result<IEnumerable<AvailableSoftwareDTO>>> GetAvailableSoftwaresFromCCP();
        Task<Result<SubscriptionDTO>> ProvisionSubscription(Subscription license);
        Task<Result> CancelSubscription(int licenseId);
        Task<Result> ExtendSubscriptionValidDate(int licenseId, DateTime newValidTo);
        Task<Result> ChangeSubscriptionQuantity(int licenseId, int newQuantity);
    }
}
