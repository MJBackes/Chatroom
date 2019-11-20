using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtilities
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string outputTextBoxText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string OutputTextBoxText
        {
            get => outputTextBoxText;
            set
            {
                outputTextBoxText += ("\n" + value);
                NotifyPropertyChanged();
            }
        }
        private string inputTextBoxText;
        public string InputTextBoxText
        {
            get => inputTextBoxText;
            set
            {
                if (value != inputTextBoxText)
                {
                    inputTextBoxText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                if (value != userName)
                {
                    userName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string userNameInputVisibility;
        public string UserNameInputVisibility
        {
            get => userNameInputVisibility; set
            {
                if (value != userNameInputVisibility)
                {
                    userNameInputVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string users;
        public string UsersList
        {
            get => users; set
            {
                if (value != users)
                {
                    users = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string connectButtonVisible;
        public string ConnectButtonVisibility
        {
            get => connectButtonVisible; set
            {
                if (value != connectButtonVisible)
                {
                    connectButtonVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string disconnectButtonVisible;
        public string DisconnectButtonVisibility
        {
            get => disconnectButtonVisible; set
            {
                if (value != disconnectButtonVisible)
                {
                    disconnectButtonVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ViewModel()
        {
            outputTextBoxText = "";
            inputTextBoxText = "";
            userName = "";
            userNameInputVisibility = "Visible";
            connectButtonVisible = "Visible";
            disconnectButtonVisible = "Hidden";
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
