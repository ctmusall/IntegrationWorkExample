using System;
using System.Collections.Generic;
using OrderPlacement.Common;
using OrderPlacement.Factories;
using OrderPlacement.Models;
using OrderPlacement.Utilities;
using Unity.Interception.Utilities;

namespace OrderPlacement.Readers
{
    internal abstract class ReswareReader : IReswareReader
    {
        private readonly BuyerSellerReaderResultUtility _buyerSellerReaderResultUtility;

        internal ReswareReader() : this(OrderDependencyFactory.Resolve<BuyerSellerReaderResultUtility>()) { }

        internal ReswareReader(BuyerSellerReaderResultUtility buyerSellerReaderResultUtility)
        {
            _buyerSellerReaderResultUtility = buyerSellerReaderResultUtility;
        }


        public ReaderResult ParseInput(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate, 
            OrderPlacementServicePartner lender,OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers, string notes, int clientId, int transactionTypeId)
        {
            var result = new ReaderResult { Order = MapReswareOrder(fileNumber, lender, estimatedSettlementDate, productId, notes, clientId, transactionTypeId) };
            result.PropertyAddress = MapPropertyAddress(result.Order, propertyAddress);
            result.BuyerSellersReaderResult = MapBuyerSellers(result.Order, buyers, sellers);

            return result;
        }

        internal abstract Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate, int productId, string notes, int clientId, int transactionTypeId);

        internal abstract void MapProductAndCustomerProduct(Order order, int productId, int transactionTypeId);

        internal virtual PropertyAddress MapPropertyAddress(Order order, OrderPlacementServicePropertyAddress propertyAddress)
        {
            return new PropertyAddress
            {
                Order = order,
                AddressStreetInfo = propertyAddress?.AddressStreetInfo,
                City = propertyAddress?.City,
                County = propertyAddress?.County,
                Description = propertyAddress?.Description,
                State = propertyAddress?.State,
                StreetDirection = propertyAddress?.StreetDirection,
                StreetName = propertyAddress?.StreetName,
                StreetNumber = propertyAddress?.StreetNumber,
                StreetSuffix = propertyAddress?.StreetSuffix,
                Unit = propertyAddress?.Unit,
                Zip = propertyAddress?.Zip
            };
        }

        internal virtual BuyerSellerReaderResult MapBuyerSellers(Order order, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers)
        {
            var result = new BuyerSellerReaderResult
            {
                BuyerSellers = new List<BuyerSeller>(),
                BuyerSellerAddresses = new List<BuyerSellerAddress>()
            };

            buyers?.ForEach(buyer => _buyerSellerReaderResultUtility.CreateBuyerSellerAndBuyerSellerAddressAssociation(buyer, order, BuyerSellerEnum.Buyer, result));

            sellers?.ForEach(seller => _buyerSellerReaderResultUtility.CreateBuyerSellerAndBuyerSellerAddressAssociation(seller, order, BuyerSellerEnum.Seller, result));

            return result;
        }
    }
}