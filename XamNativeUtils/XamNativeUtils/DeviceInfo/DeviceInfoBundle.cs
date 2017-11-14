using System;
using System.Collections.Generic;
using System.Text;

namespace XamNativeUtils.DeviceInfo
{
    public class DeviceInfoBundle
    {
        public String MacAddress { get; set; }

        public String SerialNumber { get; set; }

        public String Name { get; set; }

        public String Brand { get; set; }

        public String Manufacturer { get; set; }

        public String Model { get; set; }

        public String Product { get; set; }

        public List<String> IMEI { get; set; }

        public String OsVersion { get; set; }
    }
}
