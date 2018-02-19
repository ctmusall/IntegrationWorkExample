using System;
using OrderPlacement.Factories;
using OrderPlacement.Repositories;

namespace OrderPlacement.Managers
{
    public class OrderPlacementManager : IOrderPlacementManager
    {
        private readonly ReswareReaderFactory _reswareReaderFactory;
        private readonly IReswareOrderRepository _reswareOrderRepository;
        public OrderPlacementManager():this(DependencyFactory.Resolve<ReswareReaderFactory>(), DependencyFactory.Resolve<IReswareOrderRepository>())
        {
            
        }

        public OrderPlacementManager(ReswareReaderFactory reswareReaderFactory, IReswareOrderRepository reswareOrderRepository)
        {
            _reswareReaderFactory = reswareReaderFactory;
            _reswareOrderRepository = reswareOrderRepository;
        }

        // TODO - Remove data not needed from method
        public int PlaceOrder(int clientId, int officeId, string fileNumber,
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
            var readerResult = _reswareReaderFactory.ResolveReader(clientId).ParseInput(clientId, officeId, fileNumber,propertyAddress, clientsClientId,
                transactionTypeId, productId, underwriterId, primaryContactId, estimatedSettlementDate, salesPrice,loanAmount, loanNumber, cashOut,
                payoffMortgagees, optionalActionGroupIDs, lender, isLender, buyers, sellers, additionalPartners,clientsClient, notes, requestAquaDecision,
                originalDebtAmount, isWholesaleOrder, cplCompany, cplDivision, cplStreet1, cplStreet2, cplCity,cplState, cplZip, assetNumber,
                priorLenderPolicy, priorOwnerPolicy, buyerPayoffs, sellerPayoffs);

            return _reswareOrderRepository.SaveReaderResult(readerResult);
        }
    }
}