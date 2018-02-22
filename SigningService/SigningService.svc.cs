using System;
using System.Collections.Generic;
using Adeptive.ResWare.Services;
using SigningService.Factories;
using SigningService.Managers;
using SigningService.Models;

namespace SigningService
{
    
    public class Service : IReceiveSigningService
    {
        private readonly ISigningManager _signingManager;
        private readonly ISigningServiceResultManager _signingServiceResultManager;

        public Service() : this(SigningDependencyFactory.Resolve<ISigningManager>(), SigningDependencyFactory.Resolve<ISigningServiceResultManager>())
        {
            
        }

        public Service(ISigningManager signingManager, ISigningServiceResultManager signingServiceResultManager)
        {
            _signingManager = signingManager;
            _signingServiceResultManager = signingServiceResultManager;
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

        public ICollection<SigningPartyServiceResult> GetAllSignings()
        {
            throw new NotImplementedException();
        }

        public SigningPartyServiceResult GetSigningById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteSigningById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int UpdateSigning(SigningPartyServiceResult signingPartyResult)
        {
            throw new NotImplementedException();
        }
    }
}
