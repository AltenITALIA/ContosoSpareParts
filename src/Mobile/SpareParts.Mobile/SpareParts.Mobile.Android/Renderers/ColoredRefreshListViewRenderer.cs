using Android.Content;
using Android.Support.V4.Widget;
using SpareParts.Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(ColoredRefreshListViewRenderer))]
namespace SpareParts.Mobile.Droid.Renderers
{
    public class ColoredRefreshListViewRenderer : ListViewRenderer
    {
        private readonly Context context;

        public ColoredRefreshListViewRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (Control.Parent is SwipeRefreshLayout swipe)
                {
                    swipe.SetColorSchemeColors(context.GetColor(Resource.Color.colorPrimaryDark));
                }
            }
            catch
            {
            }
        }
    }
}
