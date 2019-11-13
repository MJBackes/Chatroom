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
        public void Send(IChatLog Message)
        {
            
        }
        public async Task<string> Recieve()
        {
            //byte[] recievedMessage = new byte[256];
            //stream.ReadAsync(recievedMessage, 0, recievedMessage.Length);
            StreamReader streamReader = new StreamReader(stream);
            string inboundMessage = await Task.Run(() => streamReader.ReadToEnd());
            //string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            //Console.WriteLine(recievedMessageString);
            //return recievedMessageString;
            return inboundMessage;
        }
    }
}
