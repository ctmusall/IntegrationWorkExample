using Resware.Data.Context;
using Resware.Data.Factory;

namespace Resware.Data.Repository
{
    public abstract class RepositoryBase
    {
        internal ReswareDbContext ReswareDbContext;

        internal RepositoryBase() : this(DependencyFactory.Resolve<ReswareDbContext>()) { }

        internal RepositoryBase(ReswareDbContext reswareDbContext)
        {
            ReswareDbContext = reswareDbContext;
        }
    }
}
