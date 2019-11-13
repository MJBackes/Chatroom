using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Client : IClient
    {
        NetworkStream stream;
        TcpClient client;
        public Guid UserId;
        public string UserName;
        public Client(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = Guid.NewGuid();
        }
        public void Send(IChatLog log)
        {
            ByteMessage message = Serializer.Serialize(log);
            stream.Write(message.Message,0,message.Message.Length);
        }
        public async Task<IChatLog> Recieve()
        {
            ByteMessage recievedMessage = new ByteMessage();
            await stream.ReadAsync(recievedMessage.Message, 0, recievedMessage.Message.Length);
            MessageModel recievedModel = (MessageModel)Serializer.Deserialize(recievedMessage);
            return recievedModel;
        }
    }
}
