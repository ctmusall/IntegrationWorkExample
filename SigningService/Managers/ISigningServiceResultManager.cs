using System;
using System.Collections.Generic;
using SigningService.Models;

namespace SigningService.Managers
{
    public interface ISigningServiceResultManager
    {
        ICollection<SigningServiceResult> GetAllSignings();
        SigningServiceResult GetSigningById(Guid id);
        int DeleteSigningById(Guid id);
        int UpdateSigning(SigningServiceResult signingServiceResult);
    }
}
