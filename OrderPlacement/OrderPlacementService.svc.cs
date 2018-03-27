using System;
using System.Collections.Generic;
using OrderPlacement.Factory;
using OrderPlacement.Managers;
using OrderPlacement.Models;

namespace OrderPlacement
{    
    public class OrderPlacementService : IOrderPlacementService
    {
        private readonly IOrderPlacementManager _orderPlacementManager;

        public OrderPlacementService() : this(DependencyFactory.Resolve<IOrderPlacementManager>())
        {
            
        }

        public OrderPlacementService(IOrderPlacementManager orderPlacementManager)
        {
            _orderPlacementManager = orderPlacementManager;
        }

        public PlaceOrderResponse PlaceOrder(int ClientID, int OfficeID, string FileNumber, OrderPlacementServicePropertyAddress PropertyAddress, int ClientsClientID, int TransactionTypeID, int ProductID, int UnderwriterID, int PrimaryContactID, DateTime? EstimatedSettlementDate, decimal SalesPrice, decimal LoanAmount, string LoanNumber, decimal CashOut, string[] PayoffMortgagees, int[] OptionalActionGroupIDs, OrderPlacementServicePartner Lender, bool IsLender, OrderPlacementServiceBuyerSeller[] Buyers, OrderPlacementServiceBuyerSeller[] Sellers, OrderPlacementServicePartner[] AdditionalPartners, OrderPlacementServicePartner ClientsClient, string Notes, bool RequestAQUADecision, decimal? OriginalDebtAmount, bool IsWholesaleOrder, string CPLCompany, string CPLDivision, string CPLStreet1, string CPLStreet2, string CPLCity, string CPLState, string CPLZip, string AssetNumber, OrderPlacementServicePriorPolicy PriorLenderPolicy, OrderPlacementServicePriorPolicy PriorOwnerPolicy, OrderPlacementServiceBuyerPayoff[] BuyerPayoffs, OrderPlacementServiceSellerPayoff[] SellerPayoffs)
        {
            try
            {
                var placeOrderResult = _orderPlacementManager.PlaceOrder(ClientID, FileNumber, PropertyAddress, ProductID, EstimatedSettlementDate, Lender, Buyers, Sellers, Notes, TransactionTypeID);

                if (placeOrderResult.Result > 0)
                {

                    return new PlaceOrderResponse
                    {
                        Response = $"FileNumber {FileNumber}: OrderPlacement Received",
                        ResponseCode = 0,
                        Timestamp = DateTime.Now,
                        ResWareFileNumber = FileNumber
                    };
                }

                return new PlaceOrderResponse
                {
                    Response = $"ERROR saving! Did not receive filenumber {FileNumber}. {placeOrderResult.Message}",
                    ResponseCode = -1,
                    Timestamp = DateTime.Now,
                    ResWareFileNumber = FileNumber
                };


            }
            catch (Exception ex)
            {
                return new PlaceOrderResponse
                {
                    Response = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}",
                    ResponseCode = -1,
                    Timestamp = DateTime.Now
                };
            }
        }
    }
}
