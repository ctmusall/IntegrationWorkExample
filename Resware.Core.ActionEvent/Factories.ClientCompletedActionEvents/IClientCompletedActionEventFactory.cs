using Resware.Core.Status.Factories.StatusSender;

namespace Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents
{
    public interface IClientCompletedActionEventFactory
    {
        StatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
