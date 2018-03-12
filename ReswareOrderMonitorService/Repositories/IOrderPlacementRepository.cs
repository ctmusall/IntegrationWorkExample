using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IOrderPlacementRepository
    {
        OrderResult[] GetAllOrders();
        int UpdateOrder(OrderResult reswareOrder);
    }
}
