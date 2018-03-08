using ReswareOrderMonitorService.OrderStatusSenders;

namespace ReswareOrderMonitorService.StatusSenders.Linear
{
    internal class LinearClosingCompleted : ClosingCompleted
    {
        public override bool SendStatusUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
