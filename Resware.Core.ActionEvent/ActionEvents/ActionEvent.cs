using Resware.Entities.Orders;

namespace Resware.Core.ActionEvent.ActionEvents
{
    internal abstract class ActionEvent
    {
        internal abstract bool PerformAction(Order order);
    }
}
