using System;
using Adeptive.ResWare.Services;
using SigningService.Factories;
using SigningService.Models;
using SigningService.Readers;
using SigningService.Repositories;
using SigningService.Utilities;

namespace SigningService.Managers
{
    internal class SigningManager : ISigningManager
    {
        private readonly SigningReader _signingReader;
        private readonly IReswareSigningRepository _reswareSigningRepository;
        private readonly ValidIncomingSigningUtility _validIncomingSigningUtility;

        public SigningManager() : this(SigningDependencyFactory.Resolve<SigningReader>(), SigningDependencyFactory.Resolve<IReswareSigningRepository>(), SigningDependencyFactory.Resolve<ValidIncomingSigningUtility>())
        {
            
        }

        public SigningManager(SigningReader signingReader, IReswareSigningRepository reswareSigningRepository, ValidIncomingSigningUtility validIncomingSigningUtility)
        {
            _signingReader = signingReader;
            _reswareSigningRepository = reswareSigningRepository;
            _validIncomingSigningUtility = validIncomingSigningUtility;
        }

        public SigningManagerResult PlaceSigning(ReceiveSigningData receiveSigningData)
        {
            try
            {
                var validIncomingSigningUtility = _validIncomingSigningUtility.IsIncomingSigningDataValid(receiveSigningData);
                if (!validIncomingSigningUtility.Valid) return new SigningManagerResult {Result = 0, Message = validIncomingSigningUtility.Message};

                var signingReaderResult = _signingReader.ParseInput(receiveSigningData);
                var saveReaderResult = _reswareSigningRepository.SaveReaderResult(signingReaderResult); 
                return new SigningManagerResult
                {
                    Result = saveReaderResult.Result,
                    Message = saveReaderResult.Message
                };
            }
            catch (Exception ex)
            {
                return new SigningManagerResult
                {
                    Result = -1,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }

        }
    }
}