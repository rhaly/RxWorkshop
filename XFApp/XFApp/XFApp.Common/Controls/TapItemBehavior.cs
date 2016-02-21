using System.Windows.Input;
using Xamarin.Forms;

namespace XFApp.Common.Controls
{
    public class TapItemBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty TappedItemCommandProperty =
            BindableProperty.Create<TapItemBehavior, ICommand>(behavior => behavior.TappedItemCommand, null, BindingMode.TwoWay);

        public ICommand TappedItemCommand
        {
            get { return (ICommand)GetValue(TappedItemCommandProperty); }
            set { SetValue(TappedItemCommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemTapped += Bindable_ItemTapped;

            base.OnAttachedTo(bindable);
        }

        private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TappedItemCommand?.Execute(e.Item);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemTapped -= Bindable_ItemTapped;

            base.OnDetachingFrom(bindable);
        }
    }

}
