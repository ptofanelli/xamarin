using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using XamNativeUtils.Droid.ExternalApps;
using XamNativeUtils.ExternalApps;

[assembly: Xamarin.Forms.Dependency(typeof(ExternalAppsAndroid))]
namespace XamNativeUtils.Droid.ExternalApps
{
    public class ExternalAppsAndroid : IExternalApps
    {
        public void OpenDownloads()
        {
            Xamarin.Forms.Forms.Context.StartActivity(new Intent(DownloadManager.ActionViewDownloads));
        }
    }
}
