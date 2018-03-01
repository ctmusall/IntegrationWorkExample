namespace ReswareOrderMonitorService.Factories
{
    internal interface IParentServiceUtilityFactory
    {
        IServiceUtilityFactory ResolveServiceUtilityFactory(int clientId);
    }
}
