using System;
using stgen_stfarm_xamarin.Bluetooth;
using stgen_stfarm_xamarin.Dependencies.Bluetooth;
using System.Threading.Tasks;
using System.Threading;
using ExternalAccessory;
using UIKit;
using Foundation;
using ObjCRuntime;

namespace stgen_stfarm_xamarin.iOS.Dependencies.Bluetooth
{
    public class BluetoothDeviceImpl : BluetoothDevice, IBluetooth
    {
        private EAAccessory accessory;
        private BluetoothUtils utils;

        public BluetoothDeviceImpl(string name, string address, EAAccessory accessory)
        {
            this.Name = name;
            this.Address = address;
            this.accessory = accessory;
            utils = BluetoothUtils.getInstance();
            utils.iBluetooth = this;
        }

        public override void Connect()
        {
            State = BluetoothDeviceState.Connecting;

            utils.connect(accessory);

            State = BluetoothDeviceState.Connected;
            OnConnected(new BluetoothDeviceEventArgs(this));
        }

        public override void Disconnect()
        {
            utils.disconnect(accessory);
            State = BluetoothDeviceState.Disconnected;
            OnDisconnected(new BluetoothDeviceEventArgs(this));
        }

        public void ReceiveBluetoothData(int dataReceivedSize, byte[] dataReceived)
        {
            OnDataReceived(new BluetoothDeviceEventArgs(dataReceivedSize, dataReceived));
        }

        void IBluetooth.Disconnected()
        {
            State = BluetoothDeviceState.Disconnected;
            OnDisconnected(new BluetoothDeviceEventArgs(this));
        }
    }
}