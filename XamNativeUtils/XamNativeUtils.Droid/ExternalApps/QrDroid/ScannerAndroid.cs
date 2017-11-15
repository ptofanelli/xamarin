using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamNativeUtils.ExternalApps.QrDroid;
using XamNativeUtils.Droid.ExternalApps.QrDroid;

[assembly: Xamarin.Forms.Dependency(typeof(ScannerAndroid))]
namespace XamNativeUtils.Droid.ExternalApps.QrDroid
{
    public class ScannerAndroid : IScanner
    {
        public void ReadCode(int requestCode)
        {
            Activity activity = (Activity)Xamarin.Forms.Forms.Context;
            Intent qrDroid = new Intent("la.droid.qr.scan");
            qrDroid.PutExtra("la.droid.qr.complete", true);
            
            if (qrDroid.ResolveActivity(Xamarin.Forms.Forms.Context.PackageManager) != null)
            {
                activity.StartActivityForResult(qrDroid, requestCode);
            }
            else
            {
                activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=la.droid.qr.priva")));
            }
            
        }
    }
}