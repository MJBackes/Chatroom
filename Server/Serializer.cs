using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
namespace Server
{
    public static class Serializer
    {
        public static ByteMessage Serialize(object message)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, message);
                return new ByteMessage { Message = memoryStream.ToArray() };
            }
        }
        public static object Deserialize(ByteMessage message)
        {
            using (var memoryStream = new MemoryStream(message.Message))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }
    }
}
