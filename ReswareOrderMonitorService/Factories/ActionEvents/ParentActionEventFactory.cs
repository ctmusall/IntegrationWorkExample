namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal class ParentActionEventFactory : IParentActionEventFactory
    {
        private readonly IParentServiceUtilityFactory _parentServiceUtilityFactory;

        public ParentActionEventFactory() : this(ReswareOrderDependencyFactory.Resolve<IParentServiceUtilityFactory>()) { }

        internal ParentActionEventFactory(IParentServiceUtilityFactory parentServiceUtilityFactory)
        {
            _parentServiceUtilityFactory = parentServiceUtilityFactory;
        }

        public ActionEventFactory ResolveActionEventFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - switch on client id
                default:
                   return new SolidifiActionEventFactory(_parentServiceUtilityFactory.ResolveServiceUtilityFactory(clientId));
            }
        }
    }
}
