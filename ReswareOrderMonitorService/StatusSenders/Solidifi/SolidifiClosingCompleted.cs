using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiClosingCompleted : SolidifiStatusSender
    {
        internal SolidifiClosingCompleted(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.ClosingStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            throw new System.NotImplementedException();
        }

        protected internal override void BuildStatusUpdateDocument(OrderResult reswareOrder)
        {
            throw new System.NotImplementedException();
        }
    }
}
