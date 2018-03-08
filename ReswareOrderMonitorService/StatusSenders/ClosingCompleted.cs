namespace ReswareOrderMonitorService.StatusSenders
{
    internal abstract class ClosingCompleted : IStatusSender
    {
        public abstract bool SendStatusUpdate();
    }
}
