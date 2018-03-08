namespace ReswareOrderMonitorService.OrderStatusSenders
{
    internal abstract class AssignedAttorney : IStatusSender
    {
        public abstract bool SendStatusUpdate();
    }
}
