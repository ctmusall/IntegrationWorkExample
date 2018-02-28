using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal interface IActionEventFactoryParser
    {
        ActionEventFactory ParseActionEventFactory(int clientId);
    }
}
