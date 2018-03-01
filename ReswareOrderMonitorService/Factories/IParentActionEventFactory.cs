namespace ReswareOrderMonitorService.Factories
{
    internal interface IParentActionEventFactory
    {
        ActionEventFactory ParseActionEventFactory(int clientId);
    }
}
