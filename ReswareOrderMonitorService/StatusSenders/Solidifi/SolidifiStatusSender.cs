using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentUtility _statusDocumentUtility;
        private readonly GetOrderResult _eClosingOrder;
        private readonly SolidifiUpdateOrderStatus _solidifiUpdateOrderStatus;

        internal SolidifiStatusSender(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, SolidifiUpdateOrderStatus solidifiUpdateOrderStatus)
        {
            _statusDocumentUtility = statusDocumentUtility;
            _eClosingOrder = eClosingOrder;
            _solidifiUpdateOrderStatus = solidifiUpdateOrderStatus;
        }

        public void SendStatusUpdate(OrderResult reswareOrder)
        {
            var document = _statusDocumentUtility.BuildDocument(reswareOrder, _eClosingOrder);
            if (document == null) return;
            // TODO - Send to resware utility -- send document received
            _solidifiUpdateOrderStatus.SendStatusUpdate(reswareOrder);
        }
    }
}
