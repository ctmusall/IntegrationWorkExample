using System;
using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal class SolidifiReader : ReswareReader
    {
        private const string CustomerId = "L17100";

        internal override Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate, int productId, string notes, int clientId, int transactionTypeId)
        {
            var order = new Order
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

            MapProductAndCustomerProduct(order, productId, transactionTypeId);
            return order;
            // TODO - Product(Need Refinance, Purchase, and Investment Property ID) map based on what Keith and the boyz supplies
        }

        private void MapProductAndCustomerProduct(Order order, int productId, int transactionTypeId)
        {
            
        }
    }
}