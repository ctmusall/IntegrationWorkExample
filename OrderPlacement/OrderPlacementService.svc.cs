#region Copyright Header
// Copyright (c) 2017 Adeptive Software Corporation. All rights reserved. This
// software and documentation constitute an unpublished work and contain
// valuable trade secrets and proprietary information belonging to Adeptive
// Software Corporation (ASC). None of the foregoing material may be copied,
// duplicated or disclosed without the express written permission of ASC. ASC
// EXPRESSLY DISCLAIMS ANY AND ALL WARRANTIES CONCERNING THIS SOFTWARE AND
// DOCUMENTATION, INCLUDING ANY WARRANTIES OF MERCHANTABILITY AND/OR FITNESS
// FOR ANY PARTICULAR PURPOSE, AND WARRANTIES OF PERFORMANCE, AND ANY WARRANTY
// THAT MIGHT OTHERWISE ARISE FROM COURSE OF DEALING OR USAGE OF TRADE. NO
// WARRANTY IS EITHER EXPRESS OR IMPLIED WITH RESPECT TO THE USE OF THE
// SOFTWARE OR DOCUMENTATION. Under no circumstances shall ASC be liable for
// incidental, special, indirect, direct or consequential damages or loss of
// profits, interruption of business, or related expenses which may arise from
// use of software or documentation, including but not limited to those
// resulting from defects in software and/or documentation, or loss inaccuracy
// of data of any kind.
// 
#endregion  

using System;


namespace OrderPlacement
{
    
    public class Service : IOrderPlacementService
    {
        public Service()
        {
            
        }


        public PlaceOrderResponse PlaceOrder(int ClientID, int OfficeID, string FileNumber, OrderPlacementServicePropertyAddress PropertyAddress, int ClientsClientID, int TransactionTypeID, int ProductID, int UnderwriterID, int PrimaryContactID, DateTime? EstimatedSettlementDate, decimal SalesPrice, decimal LoanAmount, string LoanNumber, decimal CashOut, string[] PayoffMortgagees, int[] OptionalActionGroupIDs, OrderPlacementServicePartner Lender, bool IsLender, OrderPlacementServiceBuyerSeller[] Buyers, OrderPlacementServiceBuyerSeller[] Sellers, OrderPlacementServicePartner[] AdditionalPartners, OrderPlacementServicePartner ClientsClient, string Notes, bool RequestAQUADecision, decimal? OriginalDebtAmount, bool IsWholesaleOrder, string CPLCompany, string CPLDivision, string CPLStreet1, string CPLStreet2, string CPLCity, string CPLState, string CPLZip, string AssetNumber, OrderPlacementServicePriorPolicy PriorLenderPolicy, OrderPlacementServicePriorPolicy PriorOwnerPolicy, OrderPlacementServiceBuyerPayoff[] BuyerPayoffs, OrderPlacementServiceSellerPayoff[] SellerPayoffs)
        {
            // TODO - Determine reader (LSI, etc.) based on ClientID

            // var order = ReswareOrderFactory.ResolveReader(ClientID).ParseInput(ClientID, OfficeID, FileNumber, PropertyAddress, ClientsClientID, TransactionTypeID, ProductID, UnderwriterID, PrimaryContactID, EstimatedSettlementDate, SalesPrice, LoanAmount, LoanNumber, CashOut, PayoffMortgagees, OptionalActionGroupIDs, Lender, IsLender, Buyers, Sellers, AdditionalPartners, ClientsClient, Notes, RequestAQUADecision, OriginalDebtAmount, IsWholesaleOrder, CPLCompany, CPLDivision, CPLStreet1, CPLStreet2, CPLCity, CPLState, CPLZip, AssetNumber, PriorLenderPolicy, PriorOwnerPolicy, BuyerPayoffs, SellerPayoffs)



            // Reader parses and maps
            // Parses product, action events, and doc types
            // Assigns Customer Name and Customer Contact

            //throw new NotImplementedException();

            return new PlaceOrderResponse()
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
