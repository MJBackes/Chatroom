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
        TcpListener server;
        public Server()
        {
            clients = new Dictionary<Guid, IClient>();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Join(IClient client)
        {
            Notify("User has joined.");
            clients.Add(((Client)client).UserId,client);
        }
        public void Leave(IClient client)
        {
            Notify("User has left");
            clients.Remove(((Client)client).UserId);
        }
        public void Notify(string message)
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
                foreach(KeyValuePair<Guid,IClient> pair in clients)
                {
                    string message = pair.Value.Recieve().Result;
                    Notify(message);
                }
                await AcceptClient();
            }
        }
        private async Task AcceptClient()
        {
            TcpClient clientSocket = await Task.Run(() => server.AcceptTcpClient());
            NetworkStream stream = clientSocket.GetStream();
            Client client = new Client(stream, clientSocket);
            Join(client);
        }
        private void Respond(IChatClient client,string body)
        {
             client.Send(body);
        }
    }
}
