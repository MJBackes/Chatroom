using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    class ViewModel
    {
        private string outputTextBoxText;
        public string OutputTextBoxText { get => outputTextBoxText; set => outputTextBoxText += ("\n" + value); }
        public string InputTextBoxText { get; set; }
        public string UserName { get; set; }
        public bool IsUserNameInputVisible { get; set; }
        public List<string> UsersList { get; set; }
        public bool IsConnectButtonVisible { get; set; }
        public bool IsDisconnectButtonVisible { get; set; }

        public ViewModel()
        {
            outputTextBoxText = "";
            InputTextBoxText = "";
            UserName = "";
            IsUserNameInputVisible = false;
            UsersList = new List<string>();
            IsConnectButtonVisible = true;
            IsDisconnectButtonVisible = false;
        }
    }
}
