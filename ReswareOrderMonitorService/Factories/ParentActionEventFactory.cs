namespace ReswareOrderMonitorService.Factories
{
    internal class ParentActionEventFactory : IParentActionEventFactory
    {
        public ActionEventFactory ParseActionEventFactory(int clientId)
        {
            switch (clientId)
            {

                default:
                   return new LinearActionEventFactory();
            }
        }
    }
}
