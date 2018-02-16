using OrderPlacement.Readers;

namespace OrderPlacement.Factories
{
    public class ReswareReaderFactory
    {
        internal IReswareReader ResolveReader(int clientId)
        {
            // TODO - Resolve reader based on resware client ID sent (ex. 1 = Linear, etc.)
            return new LinearReswareReader();    
        }
    }
}