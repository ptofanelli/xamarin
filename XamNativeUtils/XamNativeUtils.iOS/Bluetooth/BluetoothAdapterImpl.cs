using System.Collections.Generic;

using stgen_stfarm_xamarin.Bluetooth;
using stgen_stfarm_xamarin.Dependencies.Bluetooth;
using stgen_stfarm_xamarin.iOS.Dependencies.Bluetooth;
using ExternalAccessory;
using UIKit;
using Foundation;
using CoreBluetooth;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothAdapterImpl))]
namespace stgen_stfarm_xamarin.iOS.Dependencies.Bluetooth
{
    public class BluetoothAdapterImpl : AbstractBluetoothAdapter
    {

        private List<BluetoothDevice> Devices;
        private BluetoothUtils utils;


        public BluetoothAdapterImpl()
        {
            Devices = new List<BluetoothDevice>();
            utils = BluetoothUtils.getInstance();
            utils.iBluetooth = null;
        }

        public override void StartDiscovery()
        {
            Devices.Clear();
            System.Diagnostics.Debug.WriteLine("StartDiscover");

            if (utils.isBluetoothOn())
            {
                // Does not go directly to bluetooth on every OS version though, but opens the Settings on most
                State = BluetoothAdapterState.Off;
                UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=Bluetooth"));
            }
            else
            {
                State = BluetoothAdapterState.On;
                foreach (var a in utils.findDevices())
                {
                    BluetoothDevice device = new BluetoothDeviceImpl(a.Name, a.ConnectionID.ToString(), a);
                    BluetoothAdapterEventArgs e = new BluetoothAdapterEventArgs(device);
                    Devices.Add(e.Device);
                    OnDeviceDiscovered(e);
                }
            }

            OnDiscoveryFinished(new BluetoothAdapterEventArgs(Devices));
            
        }

        public override void StopDiscovery()
        {
            System.Diagnostics.Debug.WriteLine("StopDiscover");
        }

        public override void TurnOn()
        {
            System.Diagnostics.Debug.WriteLine("TurnOn");
        }
    }
}