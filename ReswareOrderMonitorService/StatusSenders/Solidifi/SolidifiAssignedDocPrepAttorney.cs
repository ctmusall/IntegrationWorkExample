using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedDocPrepAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedDocPrepAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }
        public override void SendStatusUpdate(OrderResult order)
        {
            throw new NotImplementedException();
        }
    }
}
