using System;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal class LinearReswareReader : ReswareReader
    {
        private const string CustomerContact = "TEAM CLOSINGS";
        private const string DeliveryMethod = "eDoc";

        internal override Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate)
        {
            // TODO - CustomerName, CustomerId, and Product
            return new Order
            {
                FileNumber = fileNumber,
                // CustomerName = MapCustomerName(),
                // CustomerId = MapClientIdToEClosingsCustomerId(clientId),
                CustomerContact = CustomerContact,
                LenderName = lender.Name,
                // Product = MapProduct(),
                ClosingDateTime = estimatedSettlementDate,
                DeliveryMethod = DeliveryMethod,
                CreatedDateTime = DateTime.Now
            };
        }
    }
}