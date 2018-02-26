using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal interface IActionEventReader
    {
        void CompleteActions(OrderResult order);
    }
}
