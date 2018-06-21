using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using SpareParts.Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(DarkEntryRenderer))]
namespace SpareParts.Mobile.Droid.Renderers
{
    public class DarkEntryRenderer : EntryRenderer
    {
        public DarkEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
            {
                return;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            }
            else
            {
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
            }

            //Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}
