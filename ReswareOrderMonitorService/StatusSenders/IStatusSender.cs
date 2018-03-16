using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders
{
    internal interface IStatusSender
    {
        void SendStatusUpdate(OrderResult order);
    }
}
