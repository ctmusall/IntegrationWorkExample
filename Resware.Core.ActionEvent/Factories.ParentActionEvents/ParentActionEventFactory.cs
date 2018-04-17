using Resware.Core.ActionEvent.Factories.ActionEvents;
using Resware.Core.Services.Factories.ParentServiceUtilities;

namespace Resware.Core.ActionEvent.Factories.ParentActionEvents
{
    public class ParentActionEventFactory : IParentActionEventFactory
    {
        private readonly ParentServiceUtilityFactory _parentServiceUtilityFactory;

        public ParentActionEventFactory() : this(DependencyFactory.Resolve<ParentServiceUtilityFactory>()) { }

        internal ParentActionEventFactory(ParentServiceUtilityFactory parentServiceUtilityFactory)
        {
            _parentServiceUtilityFactory = parentServiceUtilityFactory;
        }

        public ActionEventFactory ResolveActionEventFactory(int clientId)
        {
            switch (clientId)
            {
                case 1:
                    return new SolidifiActionEventFactory(_parentServiceUtilityFactory.ResolveServiceUtilityFactory(clientId));
                default:
                    return null;
            }
        }
    }
}
