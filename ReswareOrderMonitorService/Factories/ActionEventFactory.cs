using ReswareOrderMonitorService.ActionEvents;

namespace ReswareOrderMonitorService.Factories
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
