using OrderPlacement.Models;

namespace OrderPlacement.Utilities
{
    internal class ValidIncomingOrderUtility
    {
        internal ValidIncomingOrderResult IsIncomingOrderDataValid(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, OrderPlacementServicePartner lender)
        {
            if (string.IsNullOrWhiteSpace(fileNumber)) return new ValidIncomingOrderResult { Valid = false, Message = "File number is null or empty" };

            if (propertyAddress == null) return new ValidIncomingOrderResult { Valid = false, Message = "Property address is null or empty" };

            return lender == null ? 
                new ValidIncomingOrderResult { Valid = false, Message = "Lender is null or empty" } : 
                new ValidIncomingOrderResult { Valid = true };
        }
    }
}