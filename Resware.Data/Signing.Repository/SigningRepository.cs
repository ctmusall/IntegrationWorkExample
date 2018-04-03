using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Resware.Data.Context;
using Resware.Data.Repository;
using Resware.Entities.Signings.SigningParties;

namespace Resware.Data.Signing.Repository
{
    public class SigningRepository : RepositoryBase
    {
        internal SigningRepository(ReswareDbContext reswareDbContext) : base(reswareDbContext) { }

        public int SaveNewSigning(Entities.Signings.Signing signing, ICollection<SigningParty> signingParties)
        {
            if (signing == null) return -1;

            ReswareDbContext.Signings.Add(signing);
            if (signingParties != null) ReswareDbContext.SigningParties.AddRange(signingParties);
            return ReswareDbContext.SaveChanges();
        }

        public List<Entities.Signings.Signing> GetAllSignings()
        {
            return ReswareDbContext.Signings.Include(s => s.SigningParties).ToList();
        }

        public int UpdateSigning(Entities.Signings.Signing updatedSigning)
        {
            var signing = ReswareDbContext.Signings.FirstOrDefault(o => o.Id == updatedSigning.Id);

            if (signing == null) return 0;

            ReswareDbContext.Entry(signing).CurrentValues.SetValues(updatedSigning);

            return ReswareDbContext.SaveChanges();
        }
    }
}