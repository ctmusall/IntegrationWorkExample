using System;
using System.Collections.Generic;
using SigningService.Models;

namespace SigningService.Repositories
{
    internal interface IReswareSigningRepository
    {
        int SaveReaderResult(SigningReaderResult signingReaderResult);
        List<Signing> GetAllSignings();
        Signing GetSigningById(Guid id);
        int DeleteSigningById(Guid id);
        int UpdateSigning(Signing updatedSigning);
    }
}
