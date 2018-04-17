using Resware.Core.Services.Factories.ServiceUtilities;

namespace Resware.Core.Services.Factories.ParentServiceUtilities
{
    public class ParentServiceUtilityFactory 
    {
        public ServiceUtilityFactory ResolveServiceUtilityFactory(int clientId)
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
