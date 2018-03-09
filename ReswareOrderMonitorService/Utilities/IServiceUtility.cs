using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IServiceUtility
    {
        void AssignServices(RequestMessage requestClosingMessage);
    }
}
