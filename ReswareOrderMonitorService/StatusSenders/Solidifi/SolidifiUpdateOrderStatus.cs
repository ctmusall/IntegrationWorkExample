using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiUpdateOrderStatus : IStatusSender
    {
        internal readonly IOrderPlacementRepository OrderPlacementRepository;
        internal readonly string NewStatus;

        internal SolidifiUpdateOrderStatus(string newStatus, IOrderPlacementRepository orderPlacementRepository) : this(new OrderPlacementRepository()) { NewStatus = newStatus; }

        internal SolidifiUpdateOrderStatus(IOrderPlacementRepository orderPlacementRepository)
        {
            OrderPlacementRepository = orderPlacementRepository;
        }

        public abstract void SendStatusUpdate(OrderResult order);
    }
}
