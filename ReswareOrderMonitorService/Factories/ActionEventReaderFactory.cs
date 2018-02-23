using ReswareOrderMonitorService.Readers;

namespace ReswareOrderMonitorService.Factories
{
    internal class ActionEventReaderFactory
    {
        internal IActionEventReader ResolveActionReader(string customerId)
        {
            // TODO - Resolve monitor based on resware client ID sent (ex. 1 = Linear, etc.)
            return ReswareOrderDependencyFactory.Resolve<LinearActionEventReader>();
        }
    }
}
