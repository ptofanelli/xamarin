using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamNativeUtils.DeviceInfo;
using XamNativeUtils.Droid.DeviceInfo;
using Java.Net;
using Android.Net.Wifi;
using Android.Telephony;

[assembly: Dependency(typeof(DeviceInfoAndroid))]
namespace XamNativeUtils.Droid.DeviceInfo
{
    public class DeviceInfoAndroid : IDeviceInfo
    {

        public string GetMacAddress()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {

                try
                {
                    List<NetworkInterface> all = new List<NetworkInterface>();

                    Java.Util.IEnumeration networkInterfaces = NetworkInterface.NetworkInterfaces;
                    while (networkInterfaces.HasMoreElements)
                    {
                        Java.Net.NetworkInterface netInterface = (Java.Net.NetworkInterface)networkInterfaces.NextElement();
                        all.Add(netInterface);
                    }

                    foreach (NetworkInterface nif in all)
                    {
                        if (!nif.Name.Equals("wlan0")) continue;

                        byte[] macBytes = nif.GetHardwareAddress();
                        if (macBytes == null)
                        {
                            return "";
                        }

                        string mac = string.Join(":", (from z in macBytes select z.ToString("X2")).ToArray()).ToUpper();

                        return mac;
                    }
                }
                catch (Exception ex)
                {

                }
                return "02:00:00:00:00:00";

            }
            else
            {
                WifiManager manager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
                WifiInfo info = manager.ConnectionInfo;
                return info.MacAddress;
            }
        }

        public string GetSerialNumber()
        {
            return Build.Serial;
        }

        public string GetName()
        {
            return Build.Device;
        }

        public string GetBrand()
        {
            return Build.Brand;
        }

        public string GetManufacturer()
        {
            return Build.Manufacturer;
        }

        public string GetModel()
        {
            return Build.Model;
        }

        public string GetProduct()
        {
            return Build.Product;
        }

        public string GetIMEI()
        {
            TelephonyManager telephonyManager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Context.TelephonyService);
            return telephonyManager.GetDeviceId(0);
        }

        public string GetOsVersion()
        {
            return Build.VERSION.Sdk;
        }
    }
}