using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal interface IActionEventParser
    {
        ActionEventFactory ParseActionEventFactory(string customerId);
    }
}
