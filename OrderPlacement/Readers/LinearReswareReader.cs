using System;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal class LinearReswareReader : ReswareReader
    {
        private const string CustomerContact = "TEAM CLOSINGS";
        private const string DeliveryMethod = "eDoc";
        private const string CustomerId = "L17100";

        internal override Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate, int productId)
        {
            // TODO - Product(Need Refinance, Purchase, and Investment Property ID) map based on what Ron supplies
            return new Order
            {
                FileNumber = fileNumber,
                CustomerId = CustomerId,
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