using System;

using Contoso.IoT.UWP.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Contoso.IoT.UWP.Views
{
    public sealed partial class HistoricTripDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(HistoricTripDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public HistoricTripDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as HistoricTripDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
