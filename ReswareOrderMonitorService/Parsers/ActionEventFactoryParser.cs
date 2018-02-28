using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal class ActionEventFactoryParser : IActionEventFactoryParser
    {
        public ActionEventFactory ParseActionEventFactory(int clientId)
        {
            // TODO - Switch based on client id
            switch (clientId)
            {
                default:
                   return new LinearClosingActionEventFactory();
            }
        }
    }
}
