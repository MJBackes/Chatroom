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
        public void Send()
        {
            string messageString = UI.GetInput();
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public void Send(string input)
        {
            string messageString = input;
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public async Task Recieve()
        {
            //byte[] recievedMessage = new byte[256];
            //stream.Read(recievedMessage, 0, recievedMessage.Length);
            StreamReader streamReader = new StreamReader(stream);
            string inboundMessage = await Task.Run(() => streamReader.ReadToEnd());
        }
        public async Task AttemptConnection()
        {
            await Task.Run(() => clientSocket.Connect(IPAddress.Parse(IP), Port));
            stream = clientSocket.GetStream();
            Send(UserName);
        }
    }
}
