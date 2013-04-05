using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace trovebox.Views.Picture
{
    public partial class imageDisplay : PhoneApplicationPage
    {
        private ProgressIndicator _progressIndicator;

        public imageDisplay()
        {
            InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (null == _progressIndicator)
            {
                _progressIndicator = new ProgressIndicator();
                _progressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator = _progressIndicator;
            }

            _progressIndicator.IsIndeterminate = true;

            Model.ResponseEnvelope<trovebox.Model.Photo> result = await(App.Current as App).client.Photos.Get(this.NavigationContext.QueryString["id"]);
            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
            bi.UriSource = new Uri(result.Result.Path480x800);
            FullscreenImage.Source = bi;

            _progressIndicator.IsIndeterminate = false;
        }
    }
}