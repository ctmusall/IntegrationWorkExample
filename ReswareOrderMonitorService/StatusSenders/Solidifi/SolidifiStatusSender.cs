using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentUtility _statusDocumentUtility;
        private readonly IOrderPlacementRepository _orderPlacementRepository;

        protected internal readonly GetOrderResult EClosingOrder;

        internal SolidifiStatusSender(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : this(new OrderPlacementRepository())
        {
            EClosingOrder = eClosingOrder;
            _statusDocumentUtility = statusDocumentUtility;
        }

        internal SolidifiStatusSender(IOrderPlacementRepository orderPlacementRepository) { _orderPlacementRepository = orderPlacementRepository; }

        public void SendStatusUpdate(OrderResult reswareOrder)
        {
            var document = _statusDocumentUtility.BuildDocument(reswareOrder, EClosingOrder);
            if (document == null) return;
            var sendResult = SendDocumentToResware();
            if (!sendResult) return;
            UpdateReswareOrderStatus(reswareOrder);
            _orderPlacementRepository.UpdateOrder(reswareOrder);
        }

        protected internal abstract void UpdateReswareOrderStatus(OrderResult reswareOrder);

        protected internal abstract bool SendDocumentToResware();
    }
}
