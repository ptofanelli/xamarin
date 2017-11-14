using System;
using System.Collections.Generic;
using System.Text;

namespace XamNativeUtils.DeviceInfo
{
    public interface IDeviceInfo
    {

        String GetMacAddress();

        String GetSerialNumber();

        String GetName();

        String GetBrand();

        String GetManufacturer();

        String GetModel();

        String GetProduct();

        String GetIMEI();

        String GetOsVersion();
    }
}
