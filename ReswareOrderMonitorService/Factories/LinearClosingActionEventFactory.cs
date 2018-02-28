using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearClosingActionEventFactory : ActionEventFactory
    {
        private readonly string _requestClosingActionEventCode;

        internal LinearClosingActionEventFactory(string requestClosingActionEventCode = null)
        {
            _requestClosingActionEventCode = requestClosingActionEventCode ?? "234";
        }

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            return new LinearRequestClosing();
        }
    }
}
