using System;
using System.IO;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using ButceTakip.Droid.Helper;
using ButceTakip.Helper;
using Java.IO;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(SaveAndroid))]
namespace ButceTakip.Droid.Helper
{
    public class SaveAndroid : ISave
    {
        public void Save(string filename, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;


            if (ContextCompat.CheckSelfPermission(Forms.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Android.App.Activity)Forms.Context, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
            }

            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();
            Java.IO.File file = new Java.IO.File(myDir, filename);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());
                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (file.Exists() && contentType != "application/html")
            {
                //Android.Net.Uri path = Android.Net.Uri.FromFile(file);
                //string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                //string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                //Intent intent = new Intent(Intent.ActionView);
                //intent.SetDataAndType(path, mimeType);
                //Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }
        }
    }
}