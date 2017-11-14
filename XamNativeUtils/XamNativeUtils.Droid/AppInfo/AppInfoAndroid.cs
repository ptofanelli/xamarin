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
using XamNativeUtils.AppInfo;
using Xamarin.Forms;
using XamNativeUtils.Droid.AppInfo;

[assembly: Dependency(typeof(AppInfoAndroid))]
namespace XamNativeUtils.Droid.AppInfo
{
    public class AppInfoAndroid : IAppInfo
    {

        public int GetVersionCode()
        {
            var context = Forms.Context;
            var code = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode;
            return code;
        }

        public string GetVersionName()
        {
            var context = Forms.Context;
            var name = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            return name;
        }
    }
}