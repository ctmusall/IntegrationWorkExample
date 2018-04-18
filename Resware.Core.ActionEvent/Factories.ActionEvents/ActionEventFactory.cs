using Resware.Core.Services.Factories.ServiceUtilities;

namespace Resware.Core.ActionEvent.Factories.ActionEvents
{
    public abstract class ActionEventFactory
    {
        internal readonly ServiceUtilityFactory ServiceUtilityFactory;

        internal ActionEventFactory() { }

        internal ActionEventFactory(ServiceUtilityFactory serviceUtilityFactory)
        {
            ServiceUtilityFactory = serviceUtilityFactory;
        }

        internal abstract ActionEvent.ActionEvents.ActionEvent ResolveActionEvent(string actionEventCode);
    }
}
