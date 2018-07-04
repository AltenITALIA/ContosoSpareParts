using System;

using CommonServiceLocator;

using Contoso.IoT.UWP.Services;
using Contoso.IoT.UWP.Views;

using GalaSoft.MvvmLight.Ioc;

namespace Contoso.IoT.UWP.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<MapViewModel, MapPage>();
            Register<TripDataViewModel, TripDataPage>();
            Register<HistoricTripViewModel, HistoricTripPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public HistoricTripViewModel HistoricTripViewModel => ServiceLocator.Current.GetInstance<HistoricTripViewModel>();

        public TripDataViewModel TripDataViewModel => ServiceLocator.Current.GetInstance<TripDataViewModel>();

        public MapViewModel MapViewModel => ServiceLocator.Current.GetInstance<MapViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
