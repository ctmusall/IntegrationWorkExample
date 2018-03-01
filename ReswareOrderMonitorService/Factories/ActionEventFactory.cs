using ReswareOrderMonitorService.ActionEvents;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class ActionEventFactory
    {
        protected internal IServiceUtilityFactory ServiceUtilityFactory;

        internal ActionEventFactory() : this(ReswareOrderDependencyFactory.Resolve<IServiceUtilityFactory>()) { }

        internal ActionEventFactory(IServiceUtilityFactory serviceUtilityFactory)
        {
            ServiceUtilityFactory = serviceUtilityFactory;
        }


        internal abstract ActionEvent ResolveActionEvent(string actionEventCode);
    }
}
