namespace ReswareOrderMonitorService.Factories
{
    internal class ParentActionEventFactory : IParentActionEventFactory
    {
        private readonly IParentServiceUtilityFactory _parentServiceUtilityFactory;

        internal ParentActionEventFactory() : this(ReswareOrderDependencyFactory.Resolve<IParentServiceUtilityFactory>()) { }

        internal ParentActionEventFactory(IParentServiceUtilityFactory parentServiceUtilityFactory)
        {
            _parentServiceUtilityFactory = parentServiceUtilityFactory;
        }

        public ActionEventFactory ParseActionEventFactory(int clientId)
        {
            switch (clientId)
            {
                default:
                   return new LinearActionEventFactory(_parentServiceUtilityFactory.ResolveServiceUtilityFactory(clientId));
            }
        }
    }
}
