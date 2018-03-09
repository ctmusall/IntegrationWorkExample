using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedClosingAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }
        public override void SendStatusUpdate(OrderResult order)
        {
        }
    }
}
