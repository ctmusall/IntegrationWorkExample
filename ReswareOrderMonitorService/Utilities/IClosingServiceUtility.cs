using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IClosingServiceUtility
    {
        void AssignServices(RequestClosingMessage requestClosingMessage);
    }
}
