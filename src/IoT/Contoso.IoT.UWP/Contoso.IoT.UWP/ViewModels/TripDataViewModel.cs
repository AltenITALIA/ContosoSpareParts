using System;
using System.Collections.ObjectModel;

using Contoso.IoT.UWP.Models;
using Contoso.IoT.UWP.Services;

using GalaSoft.MvvmLight;

namespace Contoso.IoT.UWP.ViewModels
{
    public class TripDataViewModel : ViewModelBase
    {
        public ObservableCollection<CarMetric> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleMetricsService.GetGridMetricsData();
            }
        }
    }
}
