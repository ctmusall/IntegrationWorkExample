using System;

namespace OrderPlacement.Managers
{
    public class OrderPlacementManager : IOrderPlacementManager
    {
        public PlaceOrderResponse PlaceOrder(int clientId, int officeId, string fileNumber,
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

            // TODO - Determine reader (LSI, etc.) based on ClientID

            // var order = ReswareOrderFactory.ResolveReader(ClientID).ParseInput(ClientID, OfficeID, FileNumber, PropertyAddress, ClientsClientID, TransactionTypeID, ProductID, UnderwriterID, PrimaryContactID, EstimatedSettlementDate, SalesPrice, LoanAmount, LoanNumber, CashOut, PayoffMortgagees, OptionalActionGroupIDs, Lender, IsLender, Buyers, Sellers, AdditionalPartners, ClientsClient, Notes, RequestAQUADecision, OriginalDebtAmount, IsWholesaleOrder, CPLCompany, CPLDivision, CPLStreet1, CPLStreet2, CPLCity, CPLState, CPLZip, AssetNumber, PriorLenderPolicy, PriorOwnerPolicy, BuyerPayoffs, SellerPayoffs)



            // Reader parses and maps
            // Parses product, action events, and doc types
            // Assigns Customer Name and Customer Contact

            //throw new NotImplementedException();



            return new PlaceOrderResponse
            {
                Response = "(Test) OrderPlacement Received",
                ResponseCode = 0,
                Timestamp = DateTime.Now,
                ResWareFileID = 233,
                ResWareFileNumber = "233456"
            };
        }
    }
}