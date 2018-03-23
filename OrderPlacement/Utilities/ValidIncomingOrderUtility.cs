using OrderPlacement.Models;
using ReswareCommon;

namespace OrderPlacement.Utilities
{
    internal class ValidIncomingOrderUtility
    {
        internal ValidIncomingOrderResult IsIncomingOrderDataValid(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, OrderPlacementServicePartner lender)
        {
            if (string.IsNullOrWhiteSpace(fileNumber)) return new ValidIncomingOrderResult { Valid = false, Message = ValidationMessages.FileNumberIsNull };

            return propertyAddress == null ? 
                new ValidIncomingOrderResult { Valid = false, Message = ValidationMessages.PropertyAddressIsNull } : 
                new ValidIncomingOrderResult { Valid = true };
        }
    }
}