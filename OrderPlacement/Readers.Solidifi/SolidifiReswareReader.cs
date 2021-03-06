﻿using System;
using System.Collections.Generic;
using OrderPlacement.Utilities;
using Resware.Entities.Orders;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;
using ReswareCommon.Constants;

namespace OrderPlacement.Readers.Solidifi
{
    internal class SolidifiReswareReader : ReswareReader
    {
        internal SolidifiReswareReader() { }
        internal SolidifiReswareReader(BuyerSellerReaderResultUtility buyerSellerReaderResultUtility) : base(buyerSellerReaderResultUtility) { }

        private const string CustomerId = "L17100";

        internal override Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate, int productId, string notes, int clientId, int transactionTypeId)
        {
            var order = new Order
            {
                FileNumber = fileNumber,
                CustomerId = CustomerId,
                LenderName = lender?.Name,
                Notes = notes,
                ClientId = clientId,
                ClosingDateTime = estimatedSettlementDate,
                CreatedDateTime = DateTime.Now,
                PropertyAddress = new List<PropertyAddress>(),
                BuyerAndSellers = new List<BuyerSeller>()
            };

            MapProductAndCustomerProduct(order, productId, transactionTypeId);
            return order;
        }

        internal override void MapProductAndCustomerProduct(Order order, int productId, int transactionTypeId)
        {
            if (transactionTypeId == 2 && productId == 11)
            {
                order.Product = ProductNameConstants.EClosingsProductNames.InvestmentProperty;
                order.CustomerProduct = ProductNameConstants.SolidifiProductNames.InvestmentProperty;
            }
            else if (transactionTypeId == 1 && productId == 413)
            {
                order.Product = ProductNameConstants.EClosingsProductNames.ModAssumptionDil;
                order.CustomerProduct = ProductNameConstants.SolidifiProductNames.DeedInLieu;
            }
            else if (transactionTypeId == 1 && (productId == 2 || productId == 299))
            {
                order.Product = ProductNameConstants.EClosingsProductNames.Purchase;
                order.CustomerProduct = ProductNameConstants.SolidifiProductNames.BuyerSidePurchase;
            }
            else if (transactionTypeId == 1)
            {
                order.Product = ProductNameConstants.EClosingsProductNames.Purchase;
                order.CustomerProduct = ProductNameConstants.EClosingsProductNames.Purchase;
            }
            else
            {
                order.Product = ProductNameConstants.EClosingsProductNames.Refinance;
                order.CustomerProduct = ProductNameConstants.EClosingsProductNames.Refinance;
            }
        }
    }
}