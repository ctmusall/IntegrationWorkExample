using System;
using OrderPlacement.Models;

namespace OrderPlacement.Managers
{
    public interface IOrderPlacementManager
    {
        PlaceOrderResult PlaceOrder(int clientId, string fileNumber, OrderPlacementServicePropertyAddress propertyAddress, int productId, DateTime? estimatedSettlementDate,
            OrderPlacementServicePartner lender, OrderPlacementServiceBuyerSeller[] buyers, OrderPlacementServiceBuyerSeller[] sellers, string notes, int transactionTypeId);
    }
}