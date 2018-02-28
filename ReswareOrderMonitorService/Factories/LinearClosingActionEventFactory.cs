using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearClosingActionEventFactory : ActionEventFactory
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
                    return new LinearRequestClosing();
                 default:
                    return null;   
            }
        }
    }
}
