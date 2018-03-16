using Adeptive.ResWare.Services;
using SigningService.Models;

namespace SigningService.Managers
{
    public interface ISigningManager
    {
        SigningManagerResult PlaceSigning(ReceiveSigningData receiveSigningData);
    }
}
