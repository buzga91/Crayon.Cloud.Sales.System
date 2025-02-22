﻿using Crayon.Cloud.Sales.Domain.Models;
using Crayon.Cloud.Sales.Shared.DTO;

namespace Crayon.Cloud.Sales.Integration.Extensions
{
    public static class SoftwareExtensions
    {
        public static ProvisionSoftwareDTO ToCcpProvisionDto(this Subscription subscription)
        {
            return new ProvisionSoftwareDTO
            {
                AccountId = subscription.AccountId,
                Id = subscription.SoftwareId,
                ValidTo = subscription.ValidTo,
                State = subscription.State.ToString(),
                Quantity = subscription.Quantity,
            };
        }

        public static PurchasedSoftwareDTO ToCcpPurchasedDto(this ProvisionSoftwareDTO provisionSoftwareDto, string softwareName)
        {
            return new PurchasedSoftwareDTO
            {
                ValidTo = provisionSoftwareDto.ValidTo,
                State = provisionSoftwareDto.State.ToString(),
                Quantity = provisionSoftwareDto.Quantity,
                Name = softwareName
            };
        }
    }
}
