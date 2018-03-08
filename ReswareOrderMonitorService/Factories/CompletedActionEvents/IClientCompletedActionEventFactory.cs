using ReswareOrderMonitorService.Factories.StatusSenders;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal interface IClientCompletedActionEventFactory
    {
        IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
