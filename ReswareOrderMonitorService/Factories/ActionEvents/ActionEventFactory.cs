using ReswareOrderMonitorService.ActionEvents;

namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal abstract class ActionEventFactory
    {
        internal readonly IServiceUtilityFactory ServiceUtilityFactory;

        internal ActionEventFactory(IServiceUtilityFactory serviceUtilityFactory)
        {
            ServiceUtilityFactory = serviceUtilityFactory;
        }

        internal abstract ActionEvent ResolveActionEvent(string actionEventCode);
    }
}
