using SpareParts.Mobile.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(DarkEntryRenderer))]
namespace SpareParts.Mobile.iOS.Renderers
{
    public class DarkEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
            {
                return;
            }

            var resources = Xamarin.Forms.Application.Current.Resources.MergedDictionaries.First();
            var textColor = ((Color)resources["TextColorWithDarkBackground"]).ToUIColor();

            Control.Layer.BorderColor = textColor.CGColor;
            Control.Layer.BorderWidth = 1;
        }
    }
}
