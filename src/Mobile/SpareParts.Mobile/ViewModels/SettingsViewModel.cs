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

namespace SpareParts.Mobile.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public string PredictionKey
        {
            get => SettingsService.PredictionKey;
            set => SettingsService.PredictionKey = value;
        }

        public string ProjectId
        {
            get => SettingsService.ProjectId;
            set => SettingsService.ProjectId = value;
        }

        public AutoRelayCommand OpenCustomVisionWebSiteCommand { get; set; }

        public SettingsViewModel()
        {
            CreateCommands();
        }

        private void CreateCommands()
        {
            OpenCustomVisionWebSiteCommand = new AutoRelayCommand(() => Device.OpenUri(Constants.CustomVisionPortal));
        }
    }
}
