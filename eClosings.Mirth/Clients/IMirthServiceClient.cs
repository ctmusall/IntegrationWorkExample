namespace eClosings.Mirth.Clients
{
    public interface IMirthServiceClient
    {
        bool SendMessageToMirth(string message, int port);
    }
}
