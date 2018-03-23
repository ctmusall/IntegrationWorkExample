using System;
using System.Collections.Generic;
using System.Linq;
using Adeptive.ResWare.Services;
using SigningService.Models;

namespace SigningService.Readers
{
    internal class SigningReader
    {
        internal SigningReaderResult ParseInput(ReceiveSigningData receiveSigningData)
        {
            var result = new SigningReaderResult {Signing = MapSigning(receiveSigningData)};
            result.SigningParties = MapSigningParties(result.Signing, receiveSigningData);

            return result;
        }

        private static Signing MapSigning(ReceiveSigningData receiveSigningData)
        {
            return new Signing
            {
                FileNumber = receiveSigningData?.FileNumber,
                CreatedDateTime = DateTime.Now,
                ClosingDateTime = receiveSigningData?.SigningDateTime,
                MobilePhone = receiveSigningData?.TransacteeMobilePhone,
                HomePhone = receiveSigningData?.TransacteePhone,
                WorkPhone = receiveSigningData?.TransacteeWorkPhone,
                EmailAddress = receiveSigningData?.TransacteeEmailAddress,
                ClosingLocation = receiveSigningData?.Location,
                ClosingAddress = $"{receiveSigningData?.LocationStreet1} {receiveSigningData?.LocationStreet2}",
                ClosingCity = receiveSigningData?.LocationCity,
                ClosingState = receiveSigningData?.LocationState,
                ClosingZip = receiveSigningData?.LocationZIP,
                ClosingCounty = receiveSigningData?.LocationCounty
            };
        }

        private static ICollection<SigningParty> MapSigningParties(Signing signing, ReceiveSigningData receiveSigningData)
        {
            return receiveSigningData?.SigningParties?.Select(signingParty => new SigningParty
            {
                Signing = signing,
                Name = signingParty?.Name,
                Phone = signingParty?.Phone
            }).ToList();
        }
    }
}