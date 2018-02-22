using System.Collections.Generic;
using System.Linq;
using SigningService.Models;

namespace SigningService.Parsers
{
    internal class SigningParser
    {
        internal Signing ParseSigning(SigningServiceResult signingServiceResult)
        {
            return new Signing
            {
                Id = signingServiceResult.Id,
                CreatedDateTime = signingServiceResult.CreatedDateTime,
                FileNumber = signingServiceResult.FileNumber,
                ClosingDateTime = signingServiceResult.ClosingDateTime,
                MobilePhone = signingServiceResult.MobilePhone,
                HomePhone = signingServiceResult.HomePhone,
                WorkPhone = signingServiceResult.WorkPhone,
                EmailAddress = signingServiceResult.EmailAddress,
                ClosingLocation = signingServiceResult.ClosingLocation,
                ClosingAddress = signingServiceResult.ClosingAddress,
                ClosingCity = signingServiceResult.ClosingCity,
                ClosingState = signingServiceResult.ClosingState,
                ClosingZip = signingServiceResult.ClosingZip,
                ClosingCounty = signingServiceResult.ClosingCounty,
                SigningParties = ParseSigningParties(signingServiceResult.SigningParties)
            };
        }

        private static ICollection<SigningParty> ParseSigningParties(ICollection<SigningPartyServiceResult> signingPartyServiceResults)
        {
            return signingPartyServiceResults.Select(signingParty => new SigningParty
            {
                Id = signingParty.Id,
                Name = signingParty.Name,
                Phone = signingParty.Phone,
                SigningId = signingParty.SigningId
            }).ToList();
        }
    }
}