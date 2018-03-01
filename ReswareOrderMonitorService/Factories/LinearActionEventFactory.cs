using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;
using ReswareOrderMonitorService.Common;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearActionEventFactory : ActionEventFactory
    {
        private readonly IServiceUtilityFactory _serviceUtilityFactory;

        internal LinearActionEventFactory() : this(ReswareOrderDependencyFactory.Resolve<IServiceUtilityFactory>()) { }

        internal LinearActionEventFactory(IServiceUtilityFactory serviceUtilityFactory)
        {
            _serviceUtilityFactory = serviceUtilityFactory;
        }

        private const string RequestClosingActionEventCode = "234";
        private const string RescheduleActionEventCode = "240";

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case RescheduleActionEventCode:
                    return new LinearSchedulingReschedule();
                case RequestClosingActionEventCode:
                    return new LinearRequestClosing(_serviceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                 default:
                    return null;   
            }
        }
    }
}
