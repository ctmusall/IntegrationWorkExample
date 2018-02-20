using SigningService.Models;

namespace SigningService.Repositories
{
    internal interface IReswareSigningRepository
    {
        int SaveReaderResult(SigningReaderResult signingReaderResult);
    }
}
