using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client;
using MessageUtilities;
namespace Chatroom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ViewModel viewModel;
        private Client.Client client;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            this.DataContext = viewModel;
            viewModel.DisconnectButtonVisibility = "Hidden";
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.UserName.Length != 0)
            {
                viewModel.ConnectButtonVisibility = "Hidden";
                viewModel.DisconnectButtonVisibility = "Visible";
                viewModel.UserNameInputVisibility = "Hidden";
                client = new Client.Client("127.0.0.1", 9999, viewModel.UserName,viewModel);
                await client.AttemptConnection();
            }
            else
            {
                string message = "You did not enter a user name.";
                string caption = "Incomplete user input.";
                MessageBox.Show(message, caption);
            }
        }
    }
}
