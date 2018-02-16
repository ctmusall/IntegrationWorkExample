using System;

namespace OrderPlacement.Managers
{
    public interface IOrderPlacementManager
    {
        int PlaceOrder(
            int clientId,
            int officeId,
            string fileNumber,
            OrderPlacementServicePropertyAddress propertyAddress,
            int clientsClientId,
            int transactionTypeId,
            int productId,
            int underwriterId,
            int primaryContactId,
            DateTime? estimatedSettlementDate,
            decimal salesPrice,
            decimal loanAmount,
            string loanNumber,
            decimal cashOut,
            string[] payoffMortgagees,
            int[] optionalActionGroupIDs,
            OrderPlacementServicePartner lender,
            bool isLender,
            OrderPlacementServiceBuyerSeller[] buyers,
            OrderPlacementServiceBuyerSeller[] sellers,
            OrderPlacementServicePartner[] additionalPartners,
            OrderPlacementServicePartner clientsClient,
            string notes,
            bool requestAquaDecision,
            decimal? originalDebtAmount,
            bool isWholesaleOrder,
            string cplCompany,
            string cplDivision,
            string cplStreet1,
            string cplStreet2,
            string cplCity,
            string cplState,
            string cplZip,
            string assetNumber,
            OrderPlacementServicePriorPolicy priorLenderPolicy,
            OrderPlacementServicePriorPolicy priorOwnerPolicy,
            OrderPlacementServiceBuyerPayoff[] buyerPayoffs,
            OrderPlacementServiceSellerPayoff[] sellerPayoffs);
    }
}