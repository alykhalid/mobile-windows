using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace trovebox.Views.Login
{
    public partial class Default : PhoneApplicationPage
    {
        public string UriHost { get; set; }
        public bool onlyOnce { get; set; }
        public Default()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                onlyOnce = false;
                wbLogin.LoadCompleted += OnWbLoginOnLoadCompleted;
                wbLogin.Navigating += WbLoginOnNavigating;
                wbLogin.Loaded += wbLogin_Loaded;
            };
        }

        private void WbLoginOnNavigating(object sender, NavigatingEventArgs e)
        {
            if ((e.Uri.Host == UriHost) && (onlyOnce == false))
            {
                wbLogin.Navigate(new Uri("https://" + UriHost + "/v1/oauth/flow"));
                onlyOnce = true;
            }
        }

        private void OnWbLoginOnLoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri.Query.StartsWith("?"))
            {
                trovebox.Utility.QueryString qs = new Utility.QueryString(e.Uri);
                
                if (qs["oauth_consumer_key"] != null && qs["oauth_consumer_secret"] != null && qs["oauth_token"] != null && qs["oauth_token_secret"] != null)
                {
                    trovebox.Model.Credentials result = new Model.Credentials("https://" + UriHost, qs["oauth_consumer_key"], qs["oauth_consumer_secret"], qs["oauth_token"], qs["oauth_token_secret"]);
                    using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (myIsolatedStorage.FileExists("ServiceCred.xml"))
                        {
                            myIsolatedStorage.DeleteFile("ServiceCred.xml");
                        }
                        else
                        {
                            using (IsolatedStorageFileStream fs = myIsolatedStorage.CreateFile("ServiceCred.xml"))
                            {
                                XmlSerializer xmlDeserializer = new XmlSerializer(typeof(trovebox.Model.Credentials));
                                xmlDeserializer.Serialize(fs, result);
                                (App.Current as App).client = new troveboxClient(result);
                            }
                        }
                    }
                    this.NavigationService.Navigate(new Uri("/Gallery", UriKind.Relative));
                }
            }
        }

        void wbLogin_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            if (parameters.ContainsKey("domain"))
            {
                UriHost = parameters["domain"];
                wbLogin.Navigate(new Uri("https://trovebox.com/signin"));
            }
        }
    }
}