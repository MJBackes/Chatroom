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
namespace Chatroom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private Client.Client client;
        public MainWindow()
        {
            viewModel = new ViewModel();
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.UserName.Length != 0)
            {
                client = new Client.Client("127.0.0.1", 9999, viewModel.UserName);
                client.AttemptConnection();
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
