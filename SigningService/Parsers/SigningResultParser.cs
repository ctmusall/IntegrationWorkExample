using System.Collections.Generic;
using System.Linq;
using SigningService.Models;

namespace SigningService.Parsers
{
    internal class SigningResultParser
    {
        internal ICollection<SigningServiceResult> ParseAllSigningResults(ICollection<Signing> signings)
        {
            return signings.Select(signing => new SigningServiceResult
            {
                Id = signing.Id,
                CreatedDateTime = signing.CreatedDateTime,
                FileNumber = signing.FileNumber,
                ClosingDateTime = signing.ClosingDateTime,
                MobilePhone = signing.MobilePhone,
                HomePhone = signing.HomePhone,
                WorkPhone = signing.WorkPhone,
                EmailAddress = signing.EmailAddress,
                ClosingLocation = signing.ClosingLocation,
                ClosingAddress = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZip = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty,
                SigningParties = ParseSigningParties(signing.SigningParties)
            }).ToList();
        }

        internal SigningServiceResult ParseSigningResult(Signing signing)
        {
            return new SigningServiceResult
            {
                Id = signing.Id,
                CreatedDateTime = signing.CreatedDateTime,
                FileNumber = signing.FileNumber,
                ClosingDateTime = signing.ClosingDateTime,
                MobilePhone = signing.MobilePhone,
                HomePhone = signing.HomePhone,
                WorkPhone = signing.WorkPhone,
                EmailAddress = signing.EmailAddress,
                ClosingLocation = signing.ClosingLocation,
                ClosingAddress = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZip = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty,
                SigningParties = ParseSigningParties(signing.SigningParties)
            };
        }

        private static ICollection<SigningPartyServiceResult> ParseSigningParties(IEnumerable<SigningParty> signingParties)
        {
            return signingParties.Select(signingParty => new SigningPartyServiceResult
            {
                Id = signingParty.Id,
                Name = signingParty.Name,
                Phone = signingParty.Phone,
                SigningId = signingParty.SigningId
            }).ToList();
        }
    }
}