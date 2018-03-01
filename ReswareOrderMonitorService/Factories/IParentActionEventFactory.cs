namespace ReswareOrderMonitorService.Factories
{
    internal interface IParentActionEventFactory
    {
        ActionEventFactory ResolveActionEventFactory(int clientId);
    }
}
