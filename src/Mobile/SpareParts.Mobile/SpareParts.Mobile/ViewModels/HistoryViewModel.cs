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

        public AutoRelayCommand TakePhotoCommand { get; private set; }

        public AutoRelayCommand PickPhotoCommand { get; private set; }

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
        }

        public override void Activate(object parameter)
        {
            Vehicle = parameter as GetVehicle;
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
                    //// Clean up previous results.
                    //Predictions = null;
                    //ImagePath = file.Path;

                    //// Check whether to use the online or offline version of the prediction model.
                    //IEnumerable<Recognition> predictionsRecognized = null;
                    //if (isOffline)
                    //{
                    //    var classifier = CrossOfflineClassifier.Current;
                    //    predictionsRecognized = await classifier.RecognizeAsync(file.GetStream(), file.Path);
                    //}
                    //else
                    //{
                    //    var classifier = CrossOnlineClassifier.Current;
                    //    predictionsRecognized = await classifier.RecognizeAsync(SettingsService.PredictionKey, Guid.Parse(SettingsService.ProjectId), file.GetStream());
                    //}

                    //Predictions = predictionsRecognized.Select(p => $"{p.Tag}: {p.Probability:P1}");
                    //file.Dispose();
                }
            }
            catch (Exception ex)
            {
                await DialogService.AlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
