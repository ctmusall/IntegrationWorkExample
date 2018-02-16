using System;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    internal abstract class ReswareReader : IReswareReader
    {
        public abstract ReaderResult ParseInput(int clientId, int officeId, string fileNumber,
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
            OrderPlacementServiceSellerPayoff[] sellerPayoffs);
    }
}