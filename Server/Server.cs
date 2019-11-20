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
using MessageUtilities;
namespace Server
{
    public class Server : IServer
    {
        private static Dictionary<Guid,IClient> clients;
        private Queue<IChatLog> MessageQueue;
        private TcpListener server;
        private string UserList;
        public Server()
        {
            UserList = "";
            MessageQueue = new Queue<IChatLog>();
            clients = new Dictionary<Guid, IClient>();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Join(IClient client)
        {
            clients.Add(((Client)client).UserId, client);
            UpdateUserList();
            MessageModel message = new MessageModel
            {
                Message = $"{((Client)client).UserName} has joined."
            };
            message.UserList = UserList;
            Console.Write(((Client)client).UserName);
            Notify(message);
        }
        public void Leave(IClient client)
        {
            clients.Remove(((Client)client).UserId);
            UpdateUserList();
            MessageModel message = new MessageModel
            {
                Message = $"{((Client)client).UserName} has left",
            };
            message.UserList = UserList;
            Notify(message);
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
            MessageModel message = ((MessageModel)(client.Recieve().Result));
            client.UserName = message.Message;
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
        private void UpdateUserList()
        {
            UserList = UpdateUserListOutput();
        }
    }
}
