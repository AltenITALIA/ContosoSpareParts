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
    public class MainViewModel : ViewModelBase
    {
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (Set(ref searchText, value))
                {
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        //    // Filtra gli incarichi in base ai criteri di ricerca.
                        //    PrestazioniFiltrate = prestazioniService.Prestazioni
                        //        .Where(n => n.Descrizione.ContainsIgnoreCase(searchText) || n.CodiceAusiliario.ContainsIgnoreCase(searchText) || n.CodiceRegionale.ContainsIgnoreCase(searchText)
                        //        || (n.Sinonimi?.Any(s => s.ContainsIgnoreCase(searchText)) ?? false) || (n.Discipline?.Any(d => d.Descrizione.ContainsIgnoreCase(searchText)) ?? false)).ToList();
                    }
                    else
                    {
                        //Incarichi = incarichiService.Incarichi;
                    }
                }
            }
        }

        public AutoRelayCommand SettingsCommand { get; private set; }

        public MainViewModel(IMediaService mediaService)
        {
            CreateCommands();
        }

        private void CreateCommands()
        {
            SettingsCommand = new AutoRelayCommand(() => NavigationService.NavigateTo(Constants.SettingsPage));
        }
    }
}
