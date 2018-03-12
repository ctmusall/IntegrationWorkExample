using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiStatusSender : IStatusSender
    {
        private readonly IStatusDocumentUtility _statusDocumentUtility;
        protected internal readonly GetOrderResult EClosingOrder;
        protected internal readonly  OrderPlacementServiceClient OrderPlacementServiceClient;


        internal SolidifiStatusSender(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : this(new OrderPlacementServiceClient())
        {
            EClosingOrder = eClosingOrder;
            _statusDocumentUtility = statusDocumentUtility;
        }

        internal SolidifiStatusSender(OrderPlacementServiceClient orderPlacementServiceClient) { OrderPlacementServiceClient = orderPlacementServiceClient; }

        public void SendStatusUpdate(OrderResult reswareOrder)
        {
            var document = _statusDocumentUtility.BuildDocument(reswareOrder, EClosingOrder);
            if (document == null) return;
            var sendResult = SendDocumentToResware();
            if (sendResult) UpdateReswareOrderStatus(reswareOrder);
        }

        protected internal abstract void UpdateReswareOrderStatus(OrderResult reswareOrder);

        protected internal abstract bool SendDocumentToResware();
    }
}
