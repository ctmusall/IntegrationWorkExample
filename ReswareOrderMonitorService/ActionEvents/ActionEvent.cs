using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class ActionEvent
    {
        internal abstract bool PerformAction(Order order);
    }
}
