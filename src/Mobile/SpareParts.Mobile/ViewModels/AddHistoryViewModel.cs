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
using CustomVisionClient.Models;
using Microsoft.AppCenter.Analytics;

namespace SpareParts.Mobile.ViewModels
{
    public class AddHistoryViewModel : ViewModelBase
    {
        private readonly IContosoService contosoService;
        private readonly IRecognitionService recognitionService;

        private GetVehicle vehicle;
        public GetVehicle Vehicle
        {
            get => vehicle;
            set => Set(ref vehicle, value);
        }

        private Recognition recognition;
        public Recognition Recognition
        {
            get => recognition;
            set => Set(ref recognition, value, broadcast: true);
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => Set(ref imagePath, value);
        }

        public AutoRelayCommand UploadCommand { get; private set; }

        public AddHistoryViewModel(IContosoService contosoService, IRecognitionService recognitionService)
        {
            this.contosoService = contosoService;
            this.recognitionService = recognitionService;

            CreateCommands();
        }

        private void CreateCommands()
        {
            UploadCommand = new AutoRelayCommand(async () => await UploadAsync(), () => !IsBusy && Recognition != null)
                .DependsOn(nameof(IsBusy)).DependsOn(nameof(Recognition));
        }

        public override async void Activate(object parameter)
        {
            var data = parameter as HistoryData;

            Recognition = null;
            ImagePath = data.File.Path;
            Vehicle = data.Vehicle;

            await RecognizeAsync(data.File);

            base.Activate(parameter);
        }

        private async Task RecognizeAsync(MediaFile file)
        {
            IsBusy = true;

            try
            {
                Recognition = await recognitionService.RecognizeAsync(file.GetStream());
                if (recognition == null)
                {
                    await DialogService.AlertAsync("Impossibile riconoscere l'immagine. Scatta un'altra foto e riprova.", "Nessun riconoscimento");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message, ex);
            }
            finally
            {
                file.Dispose();
                IsBusy = false;
            }
        }

        private async Task UploadAsync()
        {
            Analytics.TrackEvent("Add History", new Dictionary<string, string>
            {
                ["VehicleId"] = vehicle.Id,
                ["Part"] = recognition.Tag,
            });

            DialogService.ShowLoading("Caricamento in corso, attendere...");

            try
            {
                await contosoService.AddHistoryAsync(vehicle, recognition.Tag, imagePath);

                DialogService.HideLoading();
                await DialogService.AlertAsync("Caricamento completato con successo.", "Operazione completata");
                NavigationService.GoBack();
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
    }
}
