using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SigningService.Data;
using SigningService.Factories;
using SigningService.Models;

namespace SigningService.Repositories
{
    internal class ReswareSigningRepository : IReswareSigningRepository
    {
        private readonly ReswareSigningContext _reswareSigningContext;

        public ReswareSigningRepository() : this(SigningDependencyFactory.Resolve<ReswareSigningContext>())
        {
            
        }

        public ReswareSigningRepository(ReswareSigningContext reswareSigningContext)
        {
            _reswareSigningContext = reswareSigningContext;
        }

        public int SaveReaderResult(SigningReaderResult signingReaderResult)
        {
            if (signingReaderResult.Signing == null || signingReaderResult.SigningParties == null) return -1;

            _reswareSigningContext.Signings.Add(signingReaderResult.Signing);

            _reswareSigningContext.SigningParties.AddRange(signingReaderResult.SigningParties);

            return _reswareSigningContext.SaveChanges();
        }

        public List<Signing> GetAllSignings()
        {
            return _reswareSigningContext.Signings
                .Include(s => s.SigningParties).ToList();
        }

        public Signing GetSigningById(Guid id)
        {
            return _reswareSigningContext.Signings
                .Include(s => s.SigningParties)
                .FirstOrDefault(s => s.Id == id);
        }

        public int DeleteSigningById(Guid id)
        {
            var signing = _reswareSigningContext.Signings.FirstOrDefault(o => o.Id == id);

            if (signing == null) return 0;

            _reswareSigningContext.Signings.Remove(signing);

            return _reswareSigningContext.SaveChanges();
        }

        public int UpdateSigning(Signing updatedSigning)
        {
            var signing = _reswareSigningContext.Signings.FirstOrDefault(o => o.Id == updatedSigning.Id);

            if (signing == null) return 0;

            _reswareSigningContext.Entry(signing).CurrentValues.SetValues(updatedSigning);

            return _reswareSigningContext.SaveChanges();
        }
    }
}