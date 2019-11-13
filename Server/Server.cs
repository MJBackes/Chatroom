using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server : IServer
    {
        private static Dictionary<Guid,IClient> clients;
        private Queue<IChatLog> MessageQueue;
        TcpListener server;
        public Server()
        {
            MessageQueue = new Queue<IChatLog>();
            clients = new Dictionary<Guid, IClient>();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Join(IClient client)
        {
            MessageModel message = new MessageModel();
            message.Message = $"{((Client)client).UserName} has joined.";
            Notify(message);
            clients.Add(((Client)client).UserId,client);
        }
        public void Leave(IClient client)
        {
            MessageModel message = new MessageModel();
            message.Message = $"{((Client)client).UserName} has left";
            Notify(message);
            clients.Remove(((Client)client).UserId);
        }
        public void Notify(IChatLog message)
        {
            foreach(KeyValuePair<Guid,IClient> pair in clients)
            {
                Respond(pair.Value, message);
            }
        }
        public async Task Run()
        {
            while (true)
            {
                //foreach(KeyValuePair<Guid,IClient> pair in clients)
                //{
                //    string message = pair.Value.Recieve().Result;
                //    Notify(message);
                //}
                await AcceptClient();
            }
        }
        private async Task AcceptClient()
        {
            TcpClient clientSocket = await Task.Run(() => server.AcceptTcpClient());
            NetworkStream stream = clientSocket.GetStream();
            Client client = new Client(stream, clientSocket);
            client.UserName = ((MessageModel)(client.Recieve().Result)).Message;
            Join(client);
        }
        private void Respond(IClient client,IChatLog body)
        {
             client.Send(body);
        }
        private string UpdateUserListOutput()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            foreach(KeyValuePair<Guid,IClient> pair in clients)
            {
                stringBuilder.Append($"< ListBoxItem >{((Client)pair.Value).UserName}</ ListBoxItem >");
            }
            return stringBuilder.ToString();
        }
    }
}
