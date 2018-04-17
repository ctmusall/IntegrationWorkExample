using Resware.Core.ActionEvent.Factories.ActionEvents;

namespace Resware.Core.ActionEvent.Factories.ParentActionEvents
{
    public interface IParentActionEventFactory
    {
        ActionEventFactory ResolveActionEventFactory(int clientId);
    }
}
