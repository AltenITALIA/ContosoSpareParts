using System;
using Android.Widget;
using Android.Text;
using G = Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SpareParts.Mobile.Droid.Renderers;
using Android.Content;

[assembly: ExportRenderer(typeof(SearchBar), typeof(DarkSearchBarRenderer))]
namespace SpareParts.Mobile.Droid.Renderers
{
    // https://forums.xamarin.com/discussion/67506/is-it-possible-to-hide-suggestions-in-android-keyboard
    public class DarkSearchBarRenderer : SearchBarRenderer
    {
        public DarkSearchBarRenderer(Context context): base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
        {
            base.OnElementChanged(args);

            // Get native control (background set in shared code, but can use SetBackgroundColor here)
            var searchView = Control as SearchView;
            searchView.SetInputType(InputTypes.TextFlagNoSuggestions);

            // Access search textview within control
            var textViewId = searchView.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
            var textView = searchView.FindViewById(textViewId) as EditText;

            // Set custom colors
            //textView.SetBackgroundColor(G.Color.Rgb(225, 225, 225));
            //textView.SetTextColor(G.Color.Rgb(32, 32, 32));
            //textView.SetHintTextColor(G.Color.Rgb(128, 128, 128));

            // Customize frame color
            var frameId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
            var frameView = searchView.FindViewById(frameId) as Android.Views.View;
            //frameView.SetBackgroundColor(G.Color.Rgb(96, 96, 96));

            var searchIconId = searchView.Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            var searchIcon = (ImageView)searchView.FindViewById(searchIconId);
            searchIcon.SetImageResource(Resource.Drawable.ic_search);
        }
    }
}
