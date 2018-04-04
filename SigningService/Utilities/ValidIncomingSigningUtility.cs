using Adeptive.ResWare.Services;
using ReswareCommon.Messages;
using SigningService.Models;

namespace SigningService.Utilities
{
    internal class ValidIncomingSigningUtility
    {
        internal ValidIncomingSigningResult IsIncomingSigningDataValid(ReceiveSigningData receiveSigningData)
        {
            if (string.IsNullOrWhiteSpace(receiveSigningData.FileNumber)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.FileNumberIsNull};

            if (string.IsNullOrWhiteSpace(receiveSigningData.Location)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationIsNull};

            if (string.IsNullOrWhiteSpace(receiveSigningData.LocationStreet1)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationStreet1IsNull};

            if (string.IsNullOrWhiteSpace(receiveSigningData.LocationCity)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationCityIsNull};

            if (string.IsNullOrWhiteSpace(receiveSigningData.LocationZIP)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationZipIsNull};

            if (string.IsNullOrWhiteSpace(receiveSigningData.LocationState)) return new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationStateIsNull};

            return string.IsNullOrWhiteSpace(receiveSigningData.LocationCounty) ? 
                new ValidIncomingSigningResult {Valid = false, Message = ValidationMessages.LocationCountyIsNull} : 
                new ValidIncomingSigningResult {Valid = true};
        }
    }
}