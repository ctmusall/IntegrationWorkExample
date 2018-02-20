using Adeptive.ResWare.Services;
using SigningService.Models;

namespace SigningService.Managers
{
    public interface ISigningManager
    {
        SigningResult PlaceSigning(ReceiveSigningData receiveSigningData);
    }
}
