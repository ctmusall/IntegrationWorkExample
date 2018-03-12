using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiClosingCompleted : SolidifiStatusSender
    {
        internal SolidifiClosingCompleted(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : base(eClosingOrder, statusDocumentUtility) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.ClosingStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            // TODO - Send document to resware
            return true;
        }
    }
}
