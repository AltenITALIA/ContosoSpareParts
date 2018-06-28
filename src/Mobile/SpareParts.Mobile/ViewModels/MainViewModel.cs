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
    public class MainViewModel : ViewModelBase
    {
        private readonly IContosoService contosoService;

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set => Set(ref searchText, value);
        }

        private IEnumerable<GetVehicle> vehicles;
        public IEnumerable<GetVehicle> Vehicles
        {
            get => vehicles;
            set => Set(ref vehicles, value);
        }

        public AutoRelayCommand<GetVehicle> ItemTappedCommand { get; private set; }

        public AutoRelayCommand SearchCommand { get; private set; }

        public AutoRelayCommand SettingsCommand { get; private set; }

        public MainViewModel(IContosoService contosoService)
        {
            this.contosoService = contosoService;

            CreateCommands();
        }

        private void CreateCommands()
        {
            SearchCommand = new AutoRelayCommand(async () => await SearchAsync(), () => !IsBusy).DependsOn(nameof(IsBusy));
            ItemTappedCommand = new AutoRelayCommand<GetVehicle>(async (vehicle) => await GotoVehicleHistoryAsync(vehicle));
            SettingsCommand = new AutoRelayCommand(() => NavigationService.NavigateTo(Constants.SettingsPage));
        }

        private async Task SearchAsync()
        {
            IsBusy = true;

            try
            {
                Vehicles = await contosoService.SearchVehiclesAsync(searchText);

                if (!Vehicles.Any())
                {
                    await DialogService.AlertAsync("Nessun veicolo trovato con la targa specificata.", "Ricerca veicoli");
                }
                //else if (Vehicles.Count() == 1)
                //{
                //    // E' stato trovato un solo veicolo, quindi passa direttamente alla pagina di dettaglio relativa.
                //    await Task.Delay(500);
                //    await GotoVehicleHistoryAsync(Vehicles.First());
                //}
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Si è verificato un errore durante la ricerca dei veicoli: {ex.Message}", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task GotoVehicleHistoryAsync(GetVehicle vehicle)
        {
            NavigationService.NavigateTo(Constants.HistoryPage, vehicle);
            return Task.CompletedTask;
        }
    }
}
