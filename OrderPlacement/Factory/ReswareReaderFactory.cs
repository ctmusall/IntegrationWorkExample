using OrderPlacement.Readers;
using OrderPlacement.Readers.Solidifi;

namespace OrderPlacement.Factory
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