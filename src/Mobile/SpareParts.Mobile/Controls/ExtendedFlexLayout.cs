using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpareParts.Mobile.Controls
{
    public class ExtendedFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ExtendedFlexLayout), propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ExtendedFlexLayout));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var layout = (ExtendedFlexLayout)bindable;

            layout.Children.Clear();
            if (newVal is IEnumerable newValue)
            {
                foreach (var item in newValue)
                {
                    layout.Children.Add(layout.CreateChildView(item));
                }
            }
        }

        private View CreateChildView(object item)
        {
            if (ItemTemplate is DataTemplateSelector)
            {
                var dts = ItemTemplate as DataTemplateSelector;
                var itemTemplate = dts.SelectTemplate(item, null);
                itemTemplate.SetValue(BindableObject.BindingContextProperty, item);
                return (View)itemTemplate.CreateContent();
            }
            else
            {
                ItemTemplate.SetValue(BindableObject.BindingContextProperty, item);
                return (View)ItemTemplate.CreateContent();
            }
        }
    }

}
