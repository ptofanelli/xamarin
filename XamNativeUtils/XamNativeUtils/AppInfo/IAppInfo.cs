using System;
using System.Collections.Generic;
using System.Text;

namespace XamNativeUtils.AppInfo
{
    public interface IAppInfo
    {

        int GetVersionCode();

        string GetVersionName();
    }
}
