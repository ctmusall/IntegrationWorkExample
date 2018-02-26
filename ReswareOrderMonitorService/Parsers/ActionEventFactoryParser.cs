using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal class ActionEventFactoryParser : IActionEventFactoryParser
    {
        public ActionEventFactory ParseActionEventFactory(string customerId)
        {
            // TODO - Switch based on customer id
            switch (customerId)
            {
               default:
                   return new LinearClosingActionEventFactory("19", "22", "214", "234", "240");
            }
        }
    }
}
