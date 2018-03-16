using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IReceiveActionEventRepository
    {
        ActionEventServiceResult[] GetAllActionEvents();
        int UpdateActionEvent(ActionEventServiceResult actionEvent);
    }
}
