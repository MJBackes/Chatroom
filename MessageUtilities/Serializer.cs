using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace MessageUtilities
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
            using (MemoryStream memoryStream = new MemoryStream(message.Message))
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = memoryStream;
                try
                {
                    object output = bf.Deserialize(ms);
                    return output;
                }
                catch (System.Runtime.Serialization.SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
            }
        }
    }
}
