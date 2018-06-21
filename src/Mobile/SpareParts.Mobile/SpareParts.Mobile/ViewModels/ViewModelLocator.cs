using Acr.UserDialogs;
using SpareParts.Mobile.Common;
using SpareParts.Mobile.Services;
using SpareParts.Mobile.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Globalization;
using Xamarin.Forms;
using SpareParts.ApiClient;
using Refit;

namespace SpareParts.Mobile.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(Constants.MainPage, typeof(MainPage));
            navigationService.Configure(Constants.SettingsPage, typeof(SettingsPage));
            navigationService.Configure(Constants.HistoryPage, typeof(HistoryPage));

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IUserDialogs>(() => UserDialogs.Instance);
            SimpleIoc.Default.Register<IPermissionService, PermissionService>();
            SimpleIoc.Default.Register<IMediaService, MediaService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();

            var hostUrl = new ApiOptions().BaseUri.ToString();
            SimpleIoc.Default.Register<IHistoryClient>(() => RestService.For<IHistoryClient>(hostUrl));
            SimpleIoc.Default.Register<IVehicleClient>(() => RestService.For<IVehicleClient>(hostUrl));

            SimpleIoc.Default.Register<IContosoService, ContosoService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<HistoryViewModel>();
        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public HistoryViewModel HistoryViewModel => SimpleIoc.Default.GetInstance<HistoryViewModel>();
    }
}
