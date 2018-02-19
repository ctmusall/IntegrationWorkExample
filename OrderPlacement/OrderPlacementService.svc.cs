using System;
using OrderPlacement.Factories;
using OrderPlacement.Managers;

namespace OrderPlacement
{    
    public class Service : IOrderPlacementService
    {
        private readonly IOrderPlacementManager _orderPlacementManager;

        public Service() : this(DependencyFactory.Resolve<IOrderPlacementManager>())
        {
            
        }

        public Service(IOrderPlacementManager orderPlacementManager)
        {
            _orderPlacementManager = orderPlacementManager;
        }

        public PlaceOrderResponse PlaceOrder(int ClientID, int OfficeID, string FileNumber, OrderPlacementServicePropertyAddress PropertyAddress, int ClientsClientID, int TransactionTypeID, int ProductID, int UnderwriterID, int PrimaryContactID, DateTime? EstimatedSettlementDate, decimal SalesPrice, decimal LoanAmount, string LoanNumber, decimal CashOut, string[] PayoffMortgagees, int[] OptionalActionGroupIDs, OrderPlacementServicePartner Lender, bool IsLender, OrderPlacementServiceBuyerSeller[] Buyers, OrderPlacementServiceBuyerSeller[] Sellers, OrderPlacementServicePartner[] AdditionalPartners, OrderPlacementServicePartner ClientsClient, string Notes, bool RequestAQUADecision, decimal? OriginalDebtAmount, bool IsWholesaleOrder, string CPLCompany, string CPLDivision, string CPLStreet1, string CPLStreet2, string CPLCity, string CPLState, string CPLZip, string AssetNumber, OrderPlacementServicePriorPolicy PriorLenderPolicy, OrderPlacementServicePriorPolicy PriorOwnerPolicy, OrderPlacementServiceBuyerPayoff[] BuyerPayoffs, OrderPlacementServiceSellerPayoff[] SellerPayoffs)
        {
            PlaceOrderResponse placeOrderResponse;

            try
            {
                var placeOrderResult = _orderPlacementManager.PlaceOrder(ClientID, FileNumber, PropertyAddress, ProductID, EstimatedSettlementDate, Lender, Buyers, Sellers);

                if (placeOrderResult.Result == 0 || placeOrderResult.Result == -1)
                {
                    placeOrderResponse = new PlaceOrderResponse
                    {
                        Response = $"ERROR saving! Did not receive filenumber {FileNumber}. {placeOrderResult.Message}",
                        ResponseCode = -1,
                        Timestamp = DateTime.Now,
                        ResWareFileNumber = FileNumber
                    };
                }
                else
                {
                    placeOrderResponse = new PlaceOrderResponse
                    {
                        Response = $"FileNumber {FileNumber}: OrderPlacement Received",
                        ResponseCode = 0,
                        Timestamp = DateTime.Now,
                        ResWareFileNumber = FileNumber
                    };
                }


            }
            catch (Exception ex)
            {
                placeOrderResponse = new PlaceOrderResponse
                {
                    Response = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}",
                    ResponseCode = -1,
                    Timestamp = DateTime.Now
                };
            }

            return placeOrderResponse;
        }
    }
}
