using SigningService.Data;
using SigningService.Factories;
using SigningService.Models;

namespace SigningService.Repositories
{
    internal class ReswareSigningRepository : IReswareSigningRepository
    {
        private readonly ReswareSigningContext _reswareSigningContext;

        public ReswareSigningRepository() : this(SigningDependencyFactory.Resolve<ReswareSigningContext>())
        {
            
        }

        public ReswareSigningRepository(ReswareSigningContext reswareSigningContext)
        {
            _reswareSigningContext = reswareSigningContext;
        }

        public int SaveReaderResult(SigningReaderResult signingReaderResult)
        {
            if (signingReaderResult.Signing == null || signingReaderResult.SigningParties == null) return -1;

            _reswareSigningContext.Signings.Add(signingReaderResult.Signing);

            _reswareSigningContext.SigningParties.AddRange(signingReaderResult.SigningParties);

            return _reswareSigningContext.SaveChanges();
        }
    }
}