using ActionEventService.Models;
using Adeptive.ResWare.Services;

namespace ActionEventService.Managers
{
    public interface IActionEventManager
    {
        ActionEventResult PlaceActionEvent(ReceiveActionEventData receiveActionEventData);
    }
}
