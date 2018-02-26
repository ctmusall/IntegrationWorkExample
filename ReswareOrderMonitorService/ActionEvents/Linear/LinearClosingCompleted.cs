using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearClosingCompleted : ClosingCompleted
    {
        internal override bool PerformAction(OrderResult order)
        {
            throw new System.NotImplementedException();
        }
    }
}
