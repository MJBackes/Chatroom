using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class ViewModel
    {
        private string outputTextBoxText;
        public string OutputTextBoxText { get => outputTextBoxText; set => outputTextBoxText += ("\n" + value); }
        public string InputTextBoxText { get; set; }
        public string UserName { get; set; }
        public string UserNameInputVisibility { get; set; }
        public string UsersList { get; set; }
        public string ConnectButtonVisibility { get; set; }
        public string DisconnectButtonVisibility { get; set; }

        public ViewModel()
        {
            outputTextBoxText = "";
            InputTextBoxText = "";
            UserName = "";
            UserNameInputVisibility = "Visible";
            ConnectButtonVisibility = "Visible";
            DisconnectButtonVisibility = "Hidden";
        }
    }
}
