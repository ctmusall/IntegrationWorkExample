using System;
using OrderPlacement.Factories;

namespace OrderPlacement.Managers
{
    public class OrderPlacementManager : IOrderPlacementManager
    {
        private readonly ReswareReaderFactory _reswareReaderFactory;

        public OrderPlacementManager():this(DependencyFactory.Resolve<ReswareReaderFactory>())
        {
            
        }

        public OrderPlacementManager(ReswareReaderFactory reswareReaderFactory)
        {
            _reswareReaderFactory = reswareReaderFactory;
        }

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
            var order = _reswareReaderFactory.ResolveReader(clientId).ParseInput(clientId, officeId, fileNumber,propertyAddress, clientsClientId,
                transactionTypeId, productId, underwriterId, primaryContactId, estimatedSettlementDate, salesPrice,loanAmount, loanNumber, cashOut,
                payoffMortgagees, optionalActionGroupIDs, lender, isLender, buyers, sellers, additionalPartners,clientsClient, notes, requestAquaDecision,
                originalDebtAmount, isWholesaleOrder, cplCompany, cplDivision, cplStreet1, cplStreet2, cplCity,cplState, cplZip, assetNumber,
                priorLenderPolicy, priorOwnerPolicy, buyerPayoffs, sellerPayoffs);

            // TODO - Save order context to db

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