using System;
using OrderPlacement.Factory;
using OrderPlacement.Models;
using Resware.Data.Order.Repository;
using ReswareCommon.Messages;

namespace OrderPlacement.Managers
{
    internal class OrderPlacementManager : IOrderPlacementManager
    {
        private readonly ReswareReaderFactory _reswareReaderFactory;
        private readonly OrderRepository _reswareOrderRepository;
        public OrderPlacementManager():this(DependencyFactory.Resolve<ReswareReaderFactory>(), DependencyFactory.Resolve<OrderRepository>()) { }

        public OrderPlacementManager(ReswareReaderFactory reswareReaderFactory, OrderRepository reswareOrderRepository)
        {
            _reswareReaderFactory = reswareReaderFactory;
            _reswareOrderRepository = reswareOrderRepository;
        }

        public PlaceOrderResult PlaceOrder(int clientId, string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate,
            OrderPlacementServicePartner lender, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers, string notes, int transactionTypeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileNumber)) return new PlaceOrderResult { Result = 0, Message = ValidationMessages.FileNumberIsNull };

                if (propertyAddress == null) return new PlaceOrderResult {Result = 0, Message = ValidationMessages.PropertyAddressIsNull};

                var readerResult = _reswareReaderFactory?.ResolveReader(clientId)?.ParseInput(fileNumber, propertyAddress, productId, estimatedSettlementDate, lender, buyers, sellers, notes, clientId, transactionTypeId);

                return new PlaceOrderResult
                {
                    Result = _reswareOrderRepository.SaveNewOrder(readerResult?.Order, readerResult?.PropertyAddress, readerResult?.BuyerSellersReaderResult.BuyerSellers, readerResult?.BuyerSellersReaderResult.BuyerSellerAddresses)
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