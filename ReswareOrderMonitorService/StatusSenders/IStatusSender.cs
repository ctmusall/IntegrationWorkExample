using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders
{
    internal interface IStatusSender
    {
        bool SendStatusUpdate(OrderResult order);
    }
}
