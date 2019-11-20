using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Server;
using MessageUtilities;
namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        ViewModel viewModel;
        private string IP;
        private int Port;
        public Client(string IP, int port,string userName,ViewModel view)
        {
            viewModel = view;
            viewModel.UserName = userName;
            this.IP = IP;
            Port = port;
            clientSocket = new TcpClient();
        }
        public void Send(MessageModel input)
        {
            ByteMessage message = Serializer.Serialize(input);
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
            try
            {
                await Task.Run(() => clientSocket.Connect(IPAddress.Parse(IP), Port));
                stream = clientSocket.GetStream();
                Send(new MessageModel { Message = viewModel.UserName });
                ListenForResponses();
            }
            catch(System.Net.Sockets.SocketException e)
            {
                return;
            }
        }
        private async Task ListenForResponses()
        {
            while (true)
            {
                MessageModel message = (MessageModel)await Task.Run(() => Recieve().Result);
                viewModel.OutputTextBoxText = message.Message;
                if(message.UserList != null)
                viewModel.UsersList = message.UserList;
            }
        }
    }
}
