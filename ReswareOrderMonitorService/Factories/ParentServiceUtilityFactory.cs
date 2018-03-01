﻿namespace ReswareOrderMonitorService.Factories
{
    internal class ParentServiceUtilityFactory : IParentServiceUtilityFactory
    {
        public IServiceUtilityFactory ResolveServiceUtilityFactory(int clientId)
        {
            switch (clientId)
            {
                default:
                    return new LinearServiceUtilityFactory();
            }
        }
    }
}
