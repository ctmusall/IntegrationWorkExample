using System;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        public override bool SendStatusUpdate(OrderResult order)
        {
            throw new NotImplementedException();
        }
    }
}
