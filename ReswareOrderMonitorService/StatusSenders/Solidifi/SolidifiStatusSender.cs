using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiStatusSender : IStatusSender
    {
        protected internal readonly GetOrderResult EClosingOrder;
        protected internal readonly  OrderPlacementServiceClient OrderPlacementServiceClient;

        internal SolidifiStatusSender(GetOrderResult eClosingOrder) : this(new OrderPlacementServiceClient()) { EClosingOrder = eClosingOrder; }

        internal SolidifiStatusSender(OrderPlacementServiceClient orderPlacementServiceClient) { OrderPlacementServiceClient = orderPlacementServiceClient; }

        public void SendStatusUpdate(OrderResult reswareOrder)
        {
            BuildStatusUpdateDocument(reswareOrder);
            SendDocumentToResware();
            UpdateReswareOrderStatus(reswareOrder);
        }

        protected internal abstract void UpdateReswareOrderStatus(OrderResult reswareOrder);

        protected internal abstract bool SendDocumentToResware();

        protected internal abstract void BuildStatusUpdateDocument(OrderResult reswareOrder);
    }
}
