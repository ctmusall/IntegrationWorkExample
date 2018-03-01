using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class ClosingServiceUtility : IOrderServiceUtility
    {
        public abstract void AssignServices(RequestClosingMessage requestClosingMessage);
    }
}
