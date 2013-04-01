using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace trovebox
{

    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        /// <summary>
        /// Once the page is loaded, Login a existing user, otherwise do nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Make sure that the file which contains the user credentials exists before opening it.
                //Todo: Test the credentials in the file to make sure they have not been revoked from the website by the user.
                if (myIsolatedStorage.FileExists("ServiceCred.xml"))
                {
                    using (IsolatedStorageFileStream fs = myIsolatedStorage.OpenFile("ServiceCred.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        XmlSerializer xmlDeserializer = new XmlSerializer(typeof(trovebox.Model.Credentials));
                        trovebox.Model.Credentials result = (trovebox.Model.Credentials)xmlDeserializer.Deserialize(fs);
                        (App.Current as App).client = new troveboxClient(result);
                    }
                    this.NavigationService.Navigate(new Uri("/Overview", UriKind.Relative));
                }
            }
        }

        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/Login/Default.xaml?domain=" + troveBoxDomain.Text, UriKind.Relative));
        }
    }
}