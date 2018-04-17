using Resware.Entities.Orders;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.StatusSenders
{
    internal class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentBuilder _statusDocumentBuilder;
        private readonly EClosingOrder _eClosingOrder;
        private readonly SolidifiUpdateOrderStatus _solidifiUpdateOrderStatus;

        internal SolidifiStatusSender(EClosingOrder eClosingOrder, IStatusDocumentBuilder statusDocumentBuilder, SolidifiUpdateOrderStatus solidifiUpdateOrderStatus)
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
