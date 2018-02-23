using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal interface IActionEventParser
    {
        ActionEventFactory ParseActionEvent(string customerId);
    }
}
