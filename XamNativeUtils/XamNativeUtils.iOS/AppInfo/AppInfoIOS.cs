using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using XamNativeUtils.AppInfo;
using System.Diagnostics;
using Xamarin.Forms;
using XamNativeUtils.iOS.AppInfo;

[assembly: Dependency(typeof(AppInfoIOS))]
namespace XamNativeUtils.iOS.AppInfo
{
    public class AppInfoIOS : IAppInfo
    {
        public int GetVersionCode()
        {
            var code = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];
            int intCode = 0;
            try
            {
                intCode = Convert.ToInt32(Convert.ToString(code));

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return intCode;
        }

        public string GetVersionName()
        {
            var code = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"];
            return Convert.ToString(code);
        }
    }
}