using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal interface IOrderServiceUtility
    {
        void AssignServices(RequestClosingMessage requestClosingMessage);
    }
}
