using OrderPlacement.Readers;

namespace OrderPlacement.Factories
{
    public class ReswareReaderFactory
    {
        internal IReswareReader ResolveReader(int clientId)
        {
            switch (clientId)
            {
                // TODO - switch on client id
                default:
                    return new SolidifiReswareReader();
            }
        }
    }
}