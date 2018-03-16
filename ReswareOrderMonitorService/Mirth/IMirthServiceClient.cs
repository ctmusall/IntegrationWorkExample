namespace ReswareOrderMonitorService.Mirth
{
    internal interface IMirthServiceClient
    {
        bool SendMessageToMirth(string message, int port, string ip);
    }
}
