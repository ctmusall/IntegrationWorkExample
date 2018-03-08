namespace ReswareOrderMonitorService.OrderStatusSenders
{
    internal abstract class ClosingCompleted : IStatusSender
    {
        public abstract bool SendStatusUpdate();
    }
}
