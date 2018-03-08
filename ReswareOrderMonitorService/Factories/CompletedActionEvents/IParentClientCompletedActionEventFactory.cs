namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal interface IParentClientCompletedActionEventFactory
    {
        IClientCompletedActionEventFactory ResolveClientCompletedActionEventFactory(int clientId);
    }
}
