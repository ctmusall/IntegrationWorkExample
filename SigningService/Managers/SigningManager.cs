using System;
using Adeptive.ResWare.Services;
using Resware.Data.Signing.Repository;
using SigningService.Factories;
using SigningService.Models;
using SigningService.Readers;
using SigningService.Utilities;

namespace SigningService.Managers
{
    internal class SigningManager : ISigningManager
    {
        private readonly SigningReader _signingReader;
        private readonly SigningRepository _reswareSigningRepository;
        private readonly ValidIncomingSigningUtility _validIncomingSigningUtility;

        public SigningManager() : this(DependencyFactory.Resolve<SigningReader>(), DependencyFactory.Resolve<SigningRepository>(), DependencyFactory.Resolve<ValidIncomingSigningUtility>()) { }

        public SigningManager(SigningReader signingReader, SigningRepository reswareSigningRepository, ValidIncomingSigningUtility validIncomingSigningUtility)
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
                return new SigningManagerResult
                {
                    Result = _reswareSigningRepository.SaveNewSigning(signingReaderResult.Signing, signingReaderResult.SigningParties)
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