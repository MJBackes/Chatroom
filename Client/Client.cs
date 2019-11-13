using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Server;
namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public string UserName;
        private string IP;
        private int Port;
        public Client(string IP, int port,string userName)
        {
            UserName = userName;
            this.IP = IP;
            Port = port;
            clientSocket = new TcpClient();
        }
        public void Send(string input)
        {
            ByteMessage message = new ByteMessage {
                Message = Encoding.ASCII.GetBytes(input)
            };
            stream.Write(message.Message, 0, message.Message.Length);
        }
        public async Task<IChatLog> Recieve()
        {
            ByteMessage recievedMessage = new ByteMessage();
            await stream.ReadAsync(recievedMessage.Message, 0, recievedMessage.Message.Length);
            MessageModel recievedModel = (MessageModel)Serializer.Deserialize(recievedMessage);
            return recievedModel;
        }
        public async Task AttemptConnection()
        {
            await Task.Run(() => clientSocket.Connect(IPAddress.Parse(IP), Port));
            stream = clientSocket.GetStream();
            Send(UserName);
        }
    }
}
