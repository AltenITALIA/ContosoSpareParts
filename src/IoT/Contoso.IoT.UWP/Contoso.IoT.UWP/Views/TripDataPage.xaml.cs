using System;

using Contoso.IoT.UWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Contoso.IoT.UWP.Views
{
    public sealed partial class TripDataPage : Page
    {
        private TripDataViewModel ViewModel
        {
            get { return DataContext as TripDataViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public TripDataPage()
        {
            InitializeComponent();
        }
    }
}
