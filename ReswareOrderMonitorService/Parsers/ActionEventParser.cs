using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.Parsers
{
    internal class ActionEventParser : IActionEventParser
    {
        public ActionEventFactory ParseActionEventFactory(string customerId)
        {
            switch (customerId)
            {
               default:
                   return new LinearActionEventFactory("19", "22", "214", "234", "240");
            }
        }
    }
}
