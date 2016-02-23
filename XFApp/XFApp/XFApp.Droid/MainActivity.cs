using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XFApp.Common;

namespace XFApp.Droid
{
    [Activity(Label = "XFApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) => {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
        }
    }
}

