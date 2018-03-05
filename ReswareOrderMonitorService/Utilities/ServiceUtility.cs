using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class ServiceUtility : IOrderServiceUtility
    {
        public abstract void AssignServices(RequestMessage requestClosingMessage);
    }
}
