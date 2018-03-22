using System;
using OrderPlacement.Factories;
using OrderPlacement.Models;
using OrderPlacement.Repositories;
using OrderPlacement.Utilities;

namespace OrderPlacement.Managers
{
    internal class OrderPlacementManager : IOrderPlacementManager
    {
        private readonly ReswareReaderFactory _reswareReaderFactory;
        private readonly IReswareOrderRepository _reswareOrderRepository;
        private readonly ValidIncomingOrderUtility _validIncomingOrderUtility;
        public OrderPlacementManager():this(OrderDependencyFactory.Resolve<ReswareReaderFactory>(), OrderDependencyFactory.Resolve<IReswareOrderRepository>(), OrderDependencyFactory.Resolve<ValidIncomingOrderUtility>())
        {
            
        }

        public OrderPlacementManager(ReswareReaderFactory reswareReaderFactory, IReswareOrderRepository reswareOrderRepository, ValidIncomingOrderUtility validIncomingOrderUtility)
        {
            _reswareReaderFactory = reswareReaderFactory;
            _reswareOrderRepository = reswareOrderRepository;
            _validIncomingOrderUtility = validIncomingOrderUtility;
        }

        public PlaceOrderResult PlaceOrder(int clientId, string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate,
            OrderPlacementServicePartner lender, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers, string notes, int transactionTypeId)
        {
            try
            {
                var validateOrderDataResult = _validIncomingOrderUtility.IsIncomingOrderDataValid(fileNumber, propertyAddress, lender);

                if (!validateOrderDataResult.Valid) return new PlaceOrderResult { Result = 0, Message = validateOrderDataResult.Message};

                var readerResult = _reswareReaderFactory.ResolveReader(clientId).ParseInput(fileNumber, propertyAddress, productId, estimatedSettlementDate, lender, buyers, sellers, notes, clientId, transactionTypeId);

                return new PlaceOrderResult
                {
                    Result = _reswareOrderRepository.SaveReaderResult(readerResult)
                };
            }
            catch (Exception ex)
            {
                return new PlaceOrderResult
                {
                    Result = -1,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }
        }

    }
}