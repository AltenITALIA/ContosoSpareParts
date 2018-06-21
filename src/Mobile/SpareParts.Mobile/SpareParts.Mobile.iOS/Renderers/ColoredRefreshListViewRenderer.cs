using SpareParts.Mobile.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ColoredRefreshListViewRenderer))]
namespace SpareParts.Mobile.iOS.Renderers
{
    public class ColoredRefreshListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            try
            {
                var vc = ViewController as UITableViewController;
                if (vc?.RefreshControl != null)
                {
                    var resources = Xamarin.Forms.Application.Current.Resources.MergedDictionaries.First();
                    vc.RefreshControl.TintColor = ((Color)resources["StatusBarColor"]).ToUIColor();
                }
            }
            catch
            {
            }
        }
    }
}
