using Adeptive.ResWare.Services;
using SigningService.Models;

namespace SigningService.Managers
{
    public class SigningManager : ISigningManager
    {
        public SigningResult PlaceSigning(ReceiveSigningData receiveSigningData)
        {
            return new SigningResult();
        }
    }
}