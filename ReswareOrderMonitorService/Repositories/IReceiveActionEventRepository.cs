using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IReceiveActionEventRepository
    {
        ActionEventServiceResult[] GetAllActionEvents();
    }
}
