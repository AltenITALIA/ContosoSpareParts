using System;

using Contoso.IoT.UWP.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Contoso.IoT.UWP.Views
{
    public sealed partial class HistoricTripPage : Page
    {
        private HistoricTripViewModel ViewModel
        {
            get { return DataContext as HistoricTripViewModel; }
        }

        public HistoricTripPage()
        {
            InitializeComponent();
            Loaded += HistoricTripPage_Loaded;
        }

        private async void HistoricTripPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }
    }
}
