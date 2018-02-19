using System;
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

        internal ReswareReader() : this(DependencyFactory.Resolve<BuyerSellerReaderResultUtility>())
        {
            
        }

        internal ReswareReader(BuyerSellerReaderResultUtility buyerSellerReaderResultUtility)
        {
            _buyerSellerReaderResultUtility = buyerSellerReaderResultUtility;
        }


        // TODO - Remove data not needed from method
        public ReaderResult ParseInput(int clientId, int officeId, string fileNumber,
            OrderPlacementServicePropertyAddress propertyAddress, int clientsClientId, int transactionTypeId,
            int productId, int underwriterId, int primaryContactId, DateTime? estimatedSettlementDate,
            decimal salesPrice, decimal loanAmount, string loanNumber, decimal cashOut, string[] payoffMortgagees,
            int[] optionalActionGroupIDs, OrderPlacementServicePartner lender, bool isLender,
            OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers,
            OrderPlacementServicePartner[] additionalPartners, OrderPlacementServicePartner clientsClient, string notes,
            bool requestAquaDecision, decimal? originalDebtAmount, bool isWholesaleOrder, string cplCompany,
            string cplDivision, string cplStreet1, string cplStreet2, string cplCity, string cplState, string cplZip,
            string assetNumber, OrderPlacementServicePriorPolicy priorLenderPolicy,
            OrderPlacementServicePriorPolicy priorOwnerPolicy, OrderPlacementServiceBuyerPayoff[] buyerPayoffs,
            OrderPlacementServiceSellerPayoff[] sellerPayoffs)
        {
            var result = new ReaderResult {Order = MapReswareOrder(fileNumber, lender, estimatedSettlementDate)};
            result.PropertyAddress = MapPropertyAddress(result.Order, propertyAddress);
            result.BuyerSellersReaderResult = MapBuyerSellers(result.Order, buyers, sellers);

            return result;
        }

        internal abstract Order MapReswareOrder(string fileNumber, OrderPlacementServicePartner lender, DateTime? estimatedSettlementDate);

        internal virtual PropertyAddress MapPropertyAddress(Order order, OrderPlacementServicePropertyAddress propertyAddress)
        {
            return new PropertyAddress
            {
                Order = order,
                AddressStreetInfo = propertyAddress.AddressStreetInfo,
                City = propertyAddress.City,
                County = propertyAddress.County,
                Description = propertyAddress.Description,
                State = propertyAddress.State,
                StreetDirection = propertyAddress.StreetDirection,
                StreetName = propertyAddress.StreetName,
                StreetNumber = propertyAddress.StreetNumber,
                StreetSuffix = propertyAddress.StreetSuffix,
                Unit = propertyAddress.Unit,
                Zip = propertyAddress.Zip
            };
        }

        internal virtual BuyerSellerReaderResult MapBuyerSellers(Order order, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers)
        {
            var result = new BuyerSellerReaderResult();

            buyers.ForEach(buyer => _buyerSellerReaderResultUtility.CreateBuyerSellerAndBuyerSellerAddressAssociation(buyer, order, BuyerSellerEnum.Buyer, result));

            sellers.ForEach(seller => _buyerSellerReaderResultUtility.CreateBuyerSellerAndBuyerSellerAddressAssociation(seller, order, BuyerSellerEnum.Seller, result));

            return result;
        }
    }
}