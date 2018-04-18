using Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents;

namespace Resware.Core.ActionEvent.Factories.ParentClientCompletedActionEvents
{
    public interface IParentClientCompletedActionEventFactory
    {
        IClientCompletedActionEventFactory ResolveClientCompletedActionEventFactory(int clientId);
    }
}
