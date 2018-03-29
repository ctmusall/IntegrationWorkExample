using Resware.Entities.Orders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusDocumentBuilders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentBuilder _statusDocumentBuilder;
        private readonly GetOrderResult _eClosingOrder;
        private readonly SolidifiUpdateOrderStatus _solidifiUpdateOrderStatus;

        internal SolidifiStatusSender(GetOrderResult eClosingOrder, IStatusDocumentBuilder statusDocumentBuilder, SolidifiUpdateOrderStatus solidifiUpdateOrderStatus)
        {
            _statusDocumentBuilder = statusDocumentBuilder;
            _eClosingOrder = eClosingOrder;
            _solidifiUpdateOrderStatus = solidifiUpdateOrderStatus;
        }

        public void SendStatusUpdate(Order reswareOrder)
        {
            var document = _statusDocumentBuilder.BuildDocument(reswareOrder, _eClosingOrder);
            if (document == null) return;
            // TODO - Send to resware utility -- send document received
            _solidifiUpdateOrderStatus.SendStatusUpdate(reswareOrder);
        }
    }
}
