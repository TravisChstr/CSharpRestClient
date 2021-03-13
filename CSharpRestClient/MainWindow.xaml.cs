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

namespace CSharpRestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient();
            restClient.endPoint = restURI.Text;

            if ((bool)rollOwn.IsChecked)
            {
                restClient.authTech = authenticationTechnique.RollYourOwn;
                debugOutput("AuthTechnique: Roll Your Own");
                debugOutput("AuthType: Basic");
            }
            else
            {
                restClient.authTech = authenticationTechnique.NetworkCredential;
                debugOutput("AuthTechnique: NetworkCredential");
                debugOutput("AuthType: ??? - NetCred decides");
            }

            restClient.userName = userName.Text;
            restClient.userPassword = password.Password;

            debugOutput("Rest Client Created");

            string strResponse = string.Empty;

            strResponse = restClient.makeRequest();

            debugOutput(strResponse);
        }

        private void debugOutput(string strDebugTxt)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(strDebugTxt);
                response.Text = response.Text + strDebugTxt + Environment.NewLine;
                response.SelectionStart = response.SelectionLength;
                response.ScrollToEnd();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, ToString());
            }
        }
    }
}
