using OrderPlacement.Readers;

namespace OrderPlacement.Factories
{
    public class ReswareReaderFactory
    {
        internal IReswareReader ResolveReader(int clientId)
        {
            switch (clientId)
            {
                default:
                    return new LinearReswareReader();
            }
        }
    }
}