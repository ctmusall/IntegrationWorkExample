using System;
using OrderPlacement.Models;

namespace OrderPlacement.Readers
{
    public interface IReswareReader
    {
        ReaderResult ParseInput(string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate,
            OrderPlacementServicePartner lender, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers, string notes);
    }
}