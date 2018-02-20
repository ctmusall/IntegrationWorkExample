using System;
using Adeptive.ResWare.Services;
using SigningService.Factories;
using SigningService.Models;
using SigningService.Readers;
using SigningService.Repositories;

namespace SigningService.Managers
{
    internal class SigningManager : ISigningManager
    {
        private readonly SigningReader _signingReader;
        private readonly IReswareSigningRepository _reswareSigningRepository;

        public SigningManager() : this(SigningDependencyFactory.Resolve<SigningReader>(), SigningDependencyFactory.Resolve<IReswareSigningRepository>())
        {
            
        }

        public SigningManager(SigningReader signingReader, IReswareSigningRepository reswareSigningRepository)
        {
            _signingReader = signingReader;
            _reswareSigningRepository = reswareSigningRepository;
        }

        public SigningResult PlaceSigning(ReceiveSigningData receiveSigningData)
        {
            try
            {
                var signingReaderResult = _signingReader.ParseInput(receiveSigningData);
                return new SigningResult
                {
                    Result = _reswareSigningRepository.SaveReaderResult(signingReaderResult) 
                };
            }
            catch (Exception ex)
            {
                return new SigningResult
                {
                    Result = -1,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }

        }
    }
}