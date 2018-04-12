namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal class ParentActionEventFactory : IParentActionEventFactory
    {
        private readonly IParentServiceUtilityFactory _parentServiceUtilityFactory;

        public ParentActionEventFactory() : this(DependencyFactory.Resolve<IParentServiceUtilityFactory>()) { }

        internal ParentActionEventFactory(IParentServiceUtilityFactory parentServiceUtilityFactory)
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
