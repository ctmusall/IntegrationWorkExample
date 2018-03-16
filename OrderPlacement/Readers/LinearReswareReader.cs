using System;
using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal class LinearReswareReader : ReswareReader
    {
        private const string CustomerId = "L17100";

        internal override Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate, int productId, string notes, int clientId)
        {
            // TODO - Product(Need Refinance, Purchase, and Investment Property ID) map based on what Keith and the boyz supplies
            return new Order
            {
                FileNumber = fileNumber,
                CustomerId = CustomerId,
                LenderName = lender.Name,
                // Product = MapProduct(),
                Notes = notes,
                ClientId = clientId,
                // CustomerProduct = MapCustomerProduct(),
                ClosingDateTime = estimatedSettlementDate,
                CreatedDateTime = DateTime.Now,
                PropertyAddress = new List<PropertyAddress>(),
                BuyerAndSellers = new List<BuyerSeller>()
            };
        }
    }
}