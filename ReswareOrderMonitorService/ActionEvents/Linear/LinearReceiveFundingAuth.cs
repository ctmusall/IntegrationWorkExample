using System;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearReceiveFundingAuth : ReceiveFundingAuth
    {
        internal override bool PerformAction(OrderResult order)
        {
            throw new NotImplementedException();
        }

        internal override bool SendUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
