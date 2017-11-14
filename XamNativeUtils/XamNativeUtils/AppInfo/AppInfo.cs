using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.AppInfo
{
    public class AppInfo : IAppInfo
    {
        private IAppInfo native;

        public AppInfo()
        {
            native = DependencyService.Get<IAppInfo>();
            if (native == null)
            {
                throw new NotImplementedException();
            }
        }

        public int GetVersionCode()
        {
            return native.GetVersionCode();
        }

        public string GetVersionName()
        {
            return native.GetVersionName();
        }
    }
}
