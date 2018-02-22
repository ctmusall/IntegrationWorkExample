using System;
using System.Collections.Generic;
using SigningService.Factories;
using SigningService.Models;
using SigningService.Parsers;
using SigningService.Repositories;

namespace SigningService.Managers
{
    internal class SigningServiceResultManager : ISigningServiceResultManager
    {
        private readonly IReswareSigningRepository _reswareSigningRepository;
        private readonly SigningResultParser _signingResultParser;
        private readonly SigningParser _signingParser;

        public SigningServiceResultManager() : this(SigningDependencyFactory.Resolve<IReswareSigningRepository>(),SigningDependencyFactory.Resolve<SigningResultParser>(),SigningDependencyFactory.Resolve<SigningParser>())
        {
        }

        internal SigningServiceResultManager(IReswareSigningRepository reswareSigningRepository, SigningResultParser signingResultParser, SigningParser signingParser)
        {
            _reswareSigningRepository = reswareSigningRepository;
            _signingResultParser = signingResultParser;
            _signingParser = signingParser;
        }

        public ICollection<SigningServiceResult> GetAllSignings()
        {
            try
            {
                var signings = _reswareSigningRepository.GetAllSignings();
                return _signingResultParser.ParseAllSigningResults(signings);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SigningServiceResult GetSigningById(Guid id)
        {
            try
            {
                var signing = _reswareSigningRepository.GetSigningById(id);
                return _signingResultParser.ParseSigningResult(signing);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteSigningById(Guid id)
        {
            try
            {
                return _reswareSigningRepository.DeleteSigningById(id);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateSigning(SigningServiceResult signingServiceResult)
        {
            try
            {
                var signing = _signingParser.ParseSigning(signingServiceResult);
                return _reswareSigningRepository.UpdateSigning(signing);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}