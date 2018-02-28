using ReswareOrderMonitorService.ActionEvents;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class ActionEventFactory
    {
        internal abstract ActionEvent ResolveActionEvent(string actionEventCode);
    }
}
