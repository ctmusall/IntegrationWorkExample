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
        private readonly GetOrderResult _eClosingOrder;

        internal SolidifiStatusSender(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, IOrderPlacementRepository orderPlacementRepository)
        {
            _statusDocumentUtility = statusDocumentUtility;
            _orderPlacementRepository = orderPlacementRepository;
            _eClosingOrder = eClosingOrder;
        }

        public void SendStatusUpdate(OrderResult reswareOrder)
        {
            var document = _statusDocumentUtility.BuildDocument(reswareOrder, _eClosingOrder);
            if (document == null) return;
            // TODO - Send to resware utility -- send document received
            UpdateReswareOrderStatus(reswareOrder, _eClosingOrder);
            _orderPlacementRepository.UpdateOrder(reswareOrder);
        }

        protected internal abstract void UpdateReswareOrderStatus(OrderResult reswareOrder, GetOrderResult eClosingOrder);
    }
}
