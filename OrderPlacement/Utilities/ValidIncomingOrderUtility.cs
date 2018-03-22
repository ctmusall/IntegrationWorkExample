using OrderPlacement.Models;

namespace OrderPlacement.Utilities
{
    internal class ValidIncomingOrderUtility
    {
        internal ValidIncomingOrderResult IsIncomingOrderDataValid(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, OrderPlacementServicePartner lender)
        {
            if (string.IsNullOrWhiteSpace(fileNumber)) return new ValidIncomingOrderResult { Valid = false, Message = "File number is null or empty. PCN requires a file number." };

            if (propertyAddress == null) return new ValidIncomingOrderResult { Valid = false, Message = "Property address is null or empty. PCN requires a property address." };

            return lender == null ? 
                new ValidIncomingOrderResult { Valid = false, Message = "Lender is null or empty. PCN requires a lender." } : 
                new ValidIncomingOrderResult { Valid = true };
        }
    }
}