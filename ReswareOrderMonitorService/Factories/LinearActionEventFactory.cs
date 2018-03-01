using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;
using ReswareOrderMonitorService.Common;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearActionEventFactory : ActionEventFactory
    {
        private const string RequestClosingActionEventCode = "234";
        private const string RescheduleActionEventCode = "240";

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case RescheduleActionEventCode:
                    return new LinearSchedulingReschedule();
                case RequestClosingActionEventCode:
                    return new LinearRequestClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                 default:
                    return null;   
            }
        }
    }
}
