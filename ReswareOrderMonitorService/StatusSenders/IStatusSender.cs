namespace ReswareOrderMonitorService.OrderStatusSenders
{
    internal interface IStatusSender
    {
        bool SendStatusUpdate();
    }
}
