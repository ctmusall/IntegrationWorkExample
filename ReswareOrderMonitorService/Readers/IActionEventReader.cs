using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal interface IActionEventReader
    {
        bool CompleteAction(OrderResult order);
    }
}
