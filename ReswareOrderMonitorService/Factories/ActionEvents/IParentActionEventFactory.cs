namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal interface IParentActionEventFactory
    {
        ActionEventFactory ResolveActionEventFactory(int clientId);
    }
}
