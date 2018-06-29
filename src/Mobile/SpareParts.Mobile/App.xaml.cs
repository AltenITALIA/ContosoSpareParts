using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SpareParts.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpareParts.Mobile
{
	public partial class App : Application
	{
        public static bool IsPausing { get; set; }

        public App ()
		{
			InitializeComponent();

            AppCenter.Start("ios=1e033e83-c07d-419f-971a-7188c70cc99b;" +
                  "android=20dfb19c-ce80-40e9-a80d-03c1462f3919",
                  typeof(Analytics), typeof(Crashes));

            var start = new MainPage();
            MainPage = new NavigationPage(start);
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
