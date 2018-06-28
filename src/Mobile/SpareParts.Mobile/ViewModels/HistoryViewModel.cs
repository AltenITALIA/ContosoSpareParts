using SpareParts.Mobile.Common;
using SpareParts.Mobile.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using GalaSoft.MvvmLight.Command;
using SpareParts.ApiModel.Vehicles;
using CustomVisionClient;
using SpareParts.Mobile.Models;
using SpareParts.ApiModel.History;

namespace SpareParts.Mobile.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        private readonly IContosoService contosoService;
        private readonly IMediaService mediaService;

        private GetVehicle vehicle;
        public GetVehicle Vehicle
        {
            get => vehicle;
            set => Set(ref vehicle, value);
        }

        private IEnumerable<GetHistory> history;
        public IEnumerable<GetHistory> History
        {
            get => history;
            set => Set(ref history, value);
        }

        public AutoRelayCommand TakePhotoCommand { get; private set; }

        public AutoRelayCommand PickPhotoCommand { get; private set; }

        public AutoRelayCommand RefreshCommand { get; private set; }

        public HistoryViewModel(IContosoService contosoService, IMediaService mediaService)
        {
            this.contosoService = contosoService;
            this.mediaService = mediaService;

            CreateCommands();
        }

        private void CreateCommands()
        {
            TakePhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.TakePhotoAsync()));
            PickPhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.PickPhotoAsync()));
            RefreshCommand = new AutoRelayCommand(async () => await RefreshAsync(), () => !IsBusy).DependsOn(nameof(IsBusy));
        }

        public override async void Activate(object parameter)
        {
            Vehicle = parameter as GetVehicle;
            History = null;

            await RefreshAsync();

            base.Activate(parameter);
        }

        private async Task AnalyzePhotoAsync(Func<Task<MediaFile>> action)
        {
            IsBusy = true;

            try
            {
                var file = await action.Invoke();
                if (file != null)
                {
                    NavigationService.NavigateTo(Constants.AddHistoryPage, new HistoryData(vehicle, file));
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshAsync()
        {
            IsBusy = true;

            try
            {
                History = await contosoService.GetHistoryAsync(vehicle);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Si è verificato un errore durante la ricerca della storia: {ex.Message}", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
