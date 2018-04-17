using Resware.Core.Builders.StatusDocument;
using Resware.Core.Status.StatusSenders.Solidifi;
using Resware.Entities.Orders;

namespace Resware.Core.Status.StatusSenders
{
    internal class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentBuilder _statusDocumentBuilder;
        private readonly eClosings.Entities.Orders.Order _eClosingOrder;
        private readonly SolidifiUpdateOrderStatus _solidifiUpdateOrderStatus;

        internal SolidifiStatusSender(eClosings.Entities.Orders.Order eClosingOrder, IStatusDocumentBuilder statusDocumentBuilder, SolidifiUpdateOrderStatus solidifiUpdateOrderStatus)
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
