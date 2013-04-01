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
using System.Windows.Controls.Primitives;
using System.Collections;
using System.Collections.ObjectModel;
using trovebox.Utility;
using Microsoft.Phone.Shell;

namespace trovebox.views.Overview
{
    public partial class Default : PhoneApplicationPage
    {

        private ScrollViewer sv = null;
        private bool alreadyHookedScrollEvents = false;
        private int page = 1;
        private ProgressIndicator _progressIndicator;
        private bool ScrollingCompleted = false;
        private bool BottomCompression = false;

        private RangeObservableCollection<trovebox.Model.Photo> Source = new RangeObservableCollection<trovebox.Model.Photo>();

        public Default()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Default_Loaded);
        }

        async void Default_Loaded(object sender, RoutedEventArgs e)
        {
            Model.ResponseEnvelope<List<trovebox.Model.Photo>> result = await (App.Current as App).client.Photos.GetList(page);
            Source.AddRange(result.Result);
            pics.ItemsSource = Source;

            if (alreadyHookedScrollEvents)
                return;

            alreadyHookedScrollEvents = true;
            sv = (ScrollViewer)FindElementRecursive(pics, typeof(ScrollViewer));
            if (sv != null)
            {
                // Visual States are always on the first child of the control template 
                FrameworkElement element = VisualTreeHelper.GetChild(sv, 0) as FrameworkElement;
                if (element != null)
                {
                    VisualStateGroup group = FindVisualState(element, "ScrollStates");
                    if (group != null)
                    {
                        group.CurrentStateChanging += new EventHandler<VisualStateChangedEventArgs>(group_CurrentStateChanging);
                    }

                    VisualStateGroup vgroup = FindVisualState(element, "VerticalCompression");
                    if (vgroup != null)
                    {
                        vgroup.CurrentStateChanging += new EventHandler<VisualStateChangedEventArgs>(vgroup_CurrentStateChanging);
                    }
                }
            }           

        }

        private void group_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name != "Scrolling")
            {
                ScrollingCompleted = true;
                getMorePictures();
            }
        }

        private void vgroup_CurrentStateChanging(object sender, VisualStateChangedEventArgs e)
        {
            if (e.NewState.Name == "CompressionBottom")
            {
                BottomCompression = true;
                getMorePictures();
            }
        }

        async void getMorePictures()
        {
            if (BottomCompression && ScrollingCompleted)
            {
                if (null == _progressIndicator)
                {
                    _progressIndicator = new ProgressIndicator();
                    _progressIndicator.IsVisible = true;
                    SystemTray.ProgressIndicator = _progressIndicator;
                }

                _progressIndicator.IsIndeterminate = true;
                
                ScrollingCompleted = BottomCompression = false;
                
                page += 1;
                Model.ResponseEnvelope<List<trovebox.Model.Photo>> result = await (App.Current as App).client.Photos.GetList(page);
                Source.AddRange(result.Result);
                pics.ItemsSource = Source;

                if (sv != null)
                {
                    sv.Dispatcher.BeginInvoke(() => { sv.ScrollToVerticalOffset(sv.ScrollableHeight); });
                }

                _progressIndicator.IsIndeterminate = false;
            }
        }

        private UIElement FindElementRecursive(FrameworkElement parent, Type targetType)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            UIElement returnElement = null;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Object element = VisualTreeHelper.GetChild(parent, i);
                    if (element.GetType() == targetType)
                    {
                        return element as UIElement;
                    }
                    else
                    {
                        returnElement = FindElementRecursive(VisualTreeHelper.GetChild(parent, i) as FrameworkElement, targetType);
                    }
                }
            }
            return returnElement;
        }

        private VisualStateGroup FindVisualState(FrameworkElement element, string name)
        {
            if (element == null)
                return null;

            IList groups = VisualStateManager.GetVisualStateGroups(element);
            foreach (VisualStateGroup group in groups)
                if (group.Name == name)
                    return group;

            return null;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();

            base.OnNavigatedTo(e);
        }

        private void pics_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}