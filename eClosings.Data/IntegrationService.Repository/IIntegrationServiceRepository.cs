using eClosings.Entities.Orders;

namespace eClosings.Data.IntegrationService.Repository
{
    public interface IIntegrationServiceRepository
    {
        Order GetOrder(string customerId, string fileNumber);
    }
}
