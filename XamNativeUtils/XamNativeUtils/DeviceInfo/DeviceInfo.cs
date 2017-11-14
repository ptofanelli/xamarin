using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNativeUtils.DeviceInfo
{
    public class DeviceInfo : IDeviceInfo
    {
        private IDeviceInfo native;

        public DeviceInfo()
        {
            native = DependencyService.Get<IDeviceInfo>();
            if(native == null)
            {
                throw new NotImplementedException();
            }
        }

        public DeviceInfoBundle GetBundle()
        {
            DeviceInfoBundle bundle = new DeviceInfoBundle();

            bundle.Brand = GetBrand();

            bundle.IMEI = new List<string>();
            bundle.IMEI.Add(GetIMEI());

            bundle.MacAddress = GetMacAddress();
            bundle.Manufacturer = GetManufacturer();
            bundle.Model = GetModel();
            bundle.Name = GetName();
            bundle.OsVersion = GetOsVersion();
            bundle.Product = GetProduct();
            bundle.SerialNumber = GetSerialNumber();

            return bundle;
        }

        public string GetMacAddress()
        {
            string value = native.GetMacAddress();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            value = value.ToUpper();
            return value;
        }

        public string GetSerialNumber()
        {
            string value = native.GetSerialNumber();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetName()
        {
            string value = native.GetName();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetBrand()
        {
            string value = native.GetBrand();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetManufacturer()
        {
            string value = native.GetManufacturer();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetModel()
        {
            string value = native.GetModel();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetProduct()
        {
            string value = native.GetProduct();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetIMEI()
        {
            string value = native.GetIMEI();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }

        public string GetOsVersion()
        {
            string value = native.GetOsVersion();
            value = !string.IsNullOrWhiteSpace(value) ? value : "unknown";
            return value;
        }
    }
}
