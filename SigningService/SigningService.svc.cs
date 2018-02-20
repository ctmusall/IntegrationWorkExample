using System;
using Adeptive.ResWare.Services;
using SigningService.Factories;
using SigningService.Managers;

namespace SigningService
{
    
    public class Service : IReceiveSigningService
    {
        private readonly ISigningManager _signingManager;

        public Service() : this(SigningDependencyFactory.Resolve<ISigningManager>())
        {
            
        }

        public Service(ISigningManager signingManager)
        {
            _signingManager = signingManager;
        }

        public ReceiveSigningResponse ReceiveSigning(ReceiveSigningData SigningData)
        {
            try
            {
                var signingResult = _signingManager.PlaceSigning(SigningData);

                if (signingResult.Result > 0)
                {
                    return new ReceiveSigningResponse
                    {
                        ResponseCode = 0,
                        Message = $"Filenumber {SigningData.FileNumber}: Signing Received"
                    };
                }

                return new ReceiveSigningResponse
                {
                    ResponseCode = 0,
                    Message = $"ERROR saving! Did not receive filenumber {SigningData.FileNumber}. {signingResult.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ReceiveSigningResponse
                {
                    ReceiverSigningNumber = SigningData.SenderSigningNumber,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}",
                    ResponseCode = 0
                };
            }
        }
    }
}
