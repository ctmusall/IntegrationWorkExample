namespace ReswareOrderMonitorService.StatusSenders
{
    internal abstract class AssignedAttorney : IStatusSender
    {
        public abstract bool SendStatusUpdate();
    }
}
