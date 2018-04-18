using System.IO;
using System.Xml.Serialization;

namespace ReswareCommon
{
    public static class ModelSerializer
    {
        public static string SerializeXml<T>(T objectToSerialize)
        {
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(stringWriter, objectToSerialize);
            return stringWriter.ToString();
        }
    }
}
