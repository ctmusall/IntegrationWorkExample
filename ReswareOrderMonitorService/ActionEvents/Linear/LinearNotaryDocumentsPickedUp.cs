using System;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearNotaryDocumentsPickedUp : NotaryDocumentsPickedUp
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
