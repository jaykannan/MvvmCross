﻿using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace NativeToXF.Pages
{
    public class HelloPage
        : ContentPage
    {
        public HelloPage()
        {
            Padding = new Thickness(10);

            // see https://forums.xamarin.com/discussion/45111/has-anybody-managed-to-get-a-toolbar-working-on-winrt-windows-using-xf
            if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
                Padding = new Xamarin.Forms.Thickness(Padding.Left, this.Padding.Top, this.Padding.Right, 95);

            ForceLayout();

            Title = " Hello Page";

            var entryBox = new Entry
            {
                Placeholder = "Who are you?",
                TextColor = Color.Aqua,
                WidthRequest = 30
            };

            var helloResponse = new Label
            {
                Text = string.Empty,
                FontSize = 24
            };

            var saveNickNameButton = new Button
            {
                Text = "Save",
                FontSize = 24
            };

            var image = new MvxImageView
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    Margin = new Thickness(20),
                                    HeightRequest = 100,
                                    ImageUri = "https://www.mvvmcross.com/img/MvvmCross-logo.png",
                                };

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                {
                    image.DefaultImagePath = "res:fallback";
                    image.ErrorImagePath = "res:error";
                    break;
                }
                case Device.iOS:
                {
                    image.DefaultImagePath = "res:Fallback.png";
                    image.ErrorImagePath = "res:Error.png";
                    break;
                }
            }

            Content = new StackLayout
            {
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    image,
                    new Label
                    {
                        Text = "Enter your nickname in the box below",
                        FontSize = 24
                    },
                    entryBox,
                    helloResponse,
                    saveNickNameButton
                }
            };

            entryBox.SetBinding(Entry.TextProperty, new Binding("YourNickname"));
            helloResponse.SetBinding(Label.TextProperty, new Binding("Hello"));
            saveNickNameButton.SetBinding(Button.CommandProperty, new Binding("SaveNickNameCommand"));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // Fixed in next version of Xamarin.Forms. BindingContext is not properly set on ToolbarItem.
            var aboutItem = new ToolbarItem { Text = "About", ClassId = "About", Order = ToolbarItemOrder.Primary, BindingContext = BindingContext };
            aboutItem.SetBinding(MenuItem.CommandProperty, new Binding("ShowAboutPageCommand"));


            ToolbarItems.Add(aboutItem);
        }
    }
}
