using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SpareParts.Mobile.Behaviors
{
	[Preserve(AllMembers = true)]
	public sealed class ListViewItemTappedBehavior : BehaviorBase<ListView>
	{
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ListViewItemTappedBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
		{
			base.OnAttachedTo(bindable);
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemTapped += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
		{
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemTapped -= OnListViewItemTapped;
            base.OnDetachingFrom(bindable);
		}

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Command?.CanExecute(e.Item) ?? false)
            {
                Command.Execute(e.Item);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}

