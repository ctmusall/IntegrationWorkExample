using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.Readers
{
    internal interface IActionEventReader
    {
        void CompleteActions(Order order);
    }
}
