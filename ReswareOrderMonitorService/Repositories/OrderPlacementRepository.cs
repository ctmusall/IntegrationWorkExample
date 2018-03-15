using System;
using System.Diagnostics;
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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return -1;
            }
        }
    }
}
