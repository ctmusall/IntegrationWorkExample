namespace ReswareOrderMonitorService.Factories
{
    internal class ParentServiceUtilityFactory : IParentServiceUtilityFactory
    {
        public IServiceUtilityFactory ResolveServiceUtilityFactory(int clientId)
        {
            switch (clientId)
            {
                case 1:
                    return new SolidifiServiceUtilityFactory();
                default:
                    return null;
            }
        }
    }
}
