using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class ServiceUtility : IServiceUtility
    {
        public abstract void AssignServices(RequestMessage requestClosingMessage);
    }
}
