using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ButceTakip.Droid
{
    [Activity( Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        #region Runtime Access işlemi
        //Burdaki mesele yeni android veriyonlarında uygulamaya runtime da izin vermek gerekiyor. Gerekli eirişmleri için yoksa çalışmıyor.
        //Bu işlem syncfusion da dataexport yaparken filestream kullnaılması gerekiyordu. O yüzden izin gerekti 
        readonly string[] Permission =
        {
            Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.Internet
        };

        const int Request = 0;

        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Runtime Access işlemi için
            RequestPermissions(Permission, Request);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}