using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiClosingCompleted : SolidifiStatusSender
    {
        internal SolidifiClosingCompleted(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        public override void SendStatusUpdate(OrderResult order)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
