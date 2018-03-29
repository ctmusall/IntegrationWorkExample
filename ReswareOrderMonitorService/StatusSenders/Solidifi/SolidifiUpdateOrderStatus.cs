using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Factories;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiUpdateOrderStatus : IStatusSender
    {
        internal readonly OrderRepository OrderPlacementRepository;
        internal readonly string NewStatus;

        internal SolidifiUpdateOrderStatus(string newStatus, OrderRepository orderPlacementRepository) : this(DependencyFactory.Resolve<OrderRepository>()) { NewStatus = newStatus; OrderPlacementRepository = orderPlacementRepository; }

        internal SolidifiUpdateOrderStatus(OrderRepository orderPlacementRepository)
        {
            OrderPlacementRepository = orderPlacementRepository;
        }

        public abstract void SendStatusUpdate(Order order);
    }
}
