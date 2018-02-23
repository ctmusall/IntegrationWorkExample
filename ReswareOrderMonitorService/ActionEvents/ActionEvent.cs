using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class ActionEvent
    {
        internal abstract bool PerformAction(OrderResult order);
    }
}
