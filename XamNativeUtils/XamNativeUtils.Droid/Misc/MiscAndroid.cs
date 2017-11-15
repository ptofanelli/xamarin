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
using Xamarin.Forms;
using XamNativeUtils.Droid.Misc;
using XamNativeUtils.Misc;
using Android.Media;

[assembly: Dependency(typeof(MiscAndroid))]
namespace XamNativeUtils.Droid.Misc
{
    public class MiscAndroid : IMisc
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}