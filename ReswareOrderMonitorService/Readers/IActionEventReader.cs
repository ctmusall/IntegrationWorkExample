using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal interface IActionEventReader
    {
        bool CompleteActions(OrderResult order);
    }
}
