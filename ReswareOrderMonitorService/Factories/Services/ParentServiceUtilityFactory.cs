namespace ReswareOrderMonitorService.Factories
{
    internal class ParentServiceUtilityFactory : IParentServiceUtilityFactory
    {
        public IServiceUtilityFactory ResolveServiceUtilityFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - Switch on client Id
                default:
                    return new SolidifiServiceUtilityFactory();
            }
        }
    }
}
