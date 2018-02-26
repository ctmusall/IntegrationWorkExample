using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal class ActionEventFactoryParser : IActionEventFactoryParser
    {
        public ActionEventFactory ParseActionEventFactory(string customerId)
        {
            // TODO - 
            switch (customerId)
            {
               default:
                   return new LinearActionEventFactory("19", "22", "214", "234", "240");
            }
        }
    }
}
