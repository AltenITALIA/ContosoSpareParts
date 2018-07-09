using Foundation;
using SpareParts.Mobile.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(DarkSearchBarRenderer))]
namespace SpareParts.Mobile.iOS.Renderers
{
    // https://forums.xamarin.com/discussion/67506/is-it-possible-to-hide-suggestions-in-android-keyboard
    public class DarkSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
        {
            base.OnElementChanged(args);

            var resources = Xamarin.Forms.Application.Current.Resources.MergedDictionaries.First();
            var contentBackgroundColor = ((Color)resources["MainContentBackgroundColor"]).ToUIColor();
            var textColor = ((Color)resources["TextColorWithDarkBackground"]).ToUIColor();
            var placeholderColor = ((Color)resources["PlaceholderWithDarkBackground"]).ToUIColor();

            //Control.AutocapitalizationType = UITextAutocapitalizationType.None;
            //Control.AutocorrectionType = UITextAutocorrectionType.No;
            //Control.KeyboardType = UIKeyboardType.ASCIICapable;
            Control.BarStyle = UIBarStyle.Black;
            Control.BarTintColor = contentBackgroundColor;

            var textFieldInsideSearchBar = Control.ValueForKey(new NSString("searchField")) as UITextField;
            textFieldInsideSearchBar.TextColor = textColor;

            var textFieldInsideSearchBarLabel = textFieldInsideSearchBar.ValueForKey(new NSString("placeholderLabel")) as UILabel;
            textFieldInsideSearchBarLabel.TextColor = placeholderColor;

            var glassIconView = textFieldInsideSearchBar.LeftView as UIImageView;
            glassIconView.Image = glassIconView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            glassIconView.TintColor = textColor;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == SearchBar.TextProperty.PropertyName)
            {
                Control.ShowsCancelButton = false;
            }
        }
    }
}
