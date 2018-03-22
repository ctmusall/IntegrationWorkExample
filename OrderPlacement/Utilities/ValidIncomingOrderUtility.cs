using OrderPlacement.Models;
using ReswareCommon;

namespace OrderPlacement.Utilities
{
    internal class ValidIncomingOrderUtility
    {
        internal ValidIncomingOrderResult IsIncomingOrderDataValid(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, OrderPlacementServicePartner lender)
        {
            if (string.IsNullOrWhiteSpace(fileNumber)) return new ValidIncomingOrderResult { Valid = false, Message = ValidationMessages.FileNumberIsNull };

            if (propertyAddress == null) return new ValidIncomingOrderResult { Valid = false, Message = ValidationMessages.PropertyAddressIsNull };

            return lender == null ? 
                new ValidIncomingOrderResult { Valid = false, Message = ValidationMessages.LenderIsNull } : 
                new ValidIncomingOrderResult { Valid = true };
        }
    }
}