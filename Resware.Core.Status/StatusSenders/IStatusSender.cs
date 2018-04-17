namespace Resware.Core.Status.StatusSenders
{
    public interface IStatusSender
    {
        void SendStatusUpdate(Entities.Orders.Order order);
    }
}
