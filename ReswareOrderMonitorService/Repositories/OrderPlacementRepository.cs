using System;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Repositories
{
    internal class OrderPlacementRepository : IOrderPlacementRepository
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;

        public OrderPlacementRepository() : this(new OrderPlacementServiceClient()) { }

        internal OrderPlacementRepository(OrderPlacementServiceClient orderPlacementServiceClient) { _orderPlacementServiceClient = orderPlacementServiceClient; }

        public OrderResult[] GetAllOrders()
        {
            try
            {
                return _orderPlacementServiceClient.GetAllOrders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public int UpdateOrder(OrderResult reswareOrder)
        {
            try
            {
                return _orderPlacementServiceClient.UpdateOrder(reswareOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return -1;
            }
        }
    }
}
