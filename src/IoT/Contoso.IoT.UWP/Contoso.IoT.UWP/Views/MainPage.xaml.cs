using System;

using Contoso.IoT.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Contoso.IoT.UWP.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
