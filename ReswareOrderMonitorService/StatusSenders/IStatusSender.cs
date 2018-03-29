using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.StatusSenders
{
    internal interface IStatusSender
    {
        void SendStatusUpdate(Order order);
    }
}
