using System;
using OrderPlacement.Factories;
using OrderPlacement.Models;
using OrderPlacement.Repositories;

namespace OrderPlacement.Managers
{
    public class OrderPlacementManager : IOrderPlacementManager
    {
        private readonly ReswareReaderFactory _reswareReaderFactory;
        private readonly IReswareOrderRepository _reswareOrderRepository;
        public OrderPlacementManager():this(OrderDependencyFactory.Resolve<ReswareReaderFactory>(), OrderDependencyFactory.Resolve<IReswareOrderRepository>())
        {
            
        }

        public OrderPlacementManager(ReswareReaderFactory reswareReaderFactory, IReswareOrderRepository reswareOrderRepository)
        {
            _reswareReaderFactory = reswareReaderFactory;
            _reswareOrderRepository = reswareOrderRepository;
        }

        public PlaceOrderResult PlaceOrder(int clientId, string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate,
            OrderPlacementServicePartner lender, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers)
        {
            try
            {
                var readerResult = _reswareReaderFactory.ResolveReader(clientId).ParseInput(fileNumber, propertyAddress, productId, estimatedSettlementDate, lender, buyers, sellers);

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