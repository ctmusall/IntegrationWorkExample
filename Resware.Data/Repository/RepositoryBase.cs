using Resware.Data.Context;
using Resware.Data.Factory;

namespace Resware.Data.Repository
{
    public abstract class RepositoryBase
    {
        protected readonly ReswareDbContext ReswareDbContext;
        protected RepositoryBase() : this(DependencyFactory.Resolve<ReswareDbContext>()) { }
        protected RepositoryBase(ReswareDbContext reswareDbContext)
        {
            ReswareDbContext = reswareDbContext;
        }
    }
}
