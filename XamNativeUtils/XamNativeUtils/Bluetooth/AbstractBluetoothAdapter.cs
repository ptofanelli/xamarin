using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.Bluetooth
{
    public abstract class AbstractBluetoothAdapter : BluetoothAdapter
    {
        protected const string MYUUID = "00001101-0000-1000-8000-00805f9b34fb";

        private BluetoothAdapterState _State;
        public BluetoothAdapterState State
        {
            get
            {
                return _State;
            }
            protected set
            {
                _State = value; OnStateChanged(new BluetoothAdapterEventArgs(_State));
            }
        }

        private BluetoothDevice _Device;
        public BluetoothDevice Device
        {
            get
            {
                return _Device;
            }
            protected set
            {
                _Device = value;
            }

        }

        public event EventHandler<BluetoothAdapterEventArgs> DeviceDiscovered;
        public event EventHandler<BluetoothAdapterEventArgs> DiscoveryFinished;
        public event EventHandler<BluetoothAdapterEventArgs> StateChanged;
        public event EventHandler<BluetoothAdapterEventArgs> DeviceConnected;
        public event EventHandler<BluetoothAdapterEventArgs> DeviceDisconnected;

        protected virtual void OnStateChanged(BluetoothAdapterEventArgs e)
        {
            if(e.State == BluetoothAdapterState.Off)
            {
                Device?.Disconnect();
                Device = null;
            }

            StateChanged?.Invoke(this, e);
        }

        protected void Device_Disconnected(object sender, BluetoothDeviceEventArgs e)
        {
            this.Device = null;
            DeviceDisconnected?.Invoke(this, new BluetoothAdapterEventArgs(e.Device));
        }

        protected void Device_Connected(object sender, BluetoothDeviceEventArgs e)
        {
            this.Device = e.Device;
            DeviceConnected?.Invoke(this, new BluetoothAdapterEventArgs(e.Device));
        }

        protected virtual void OnDeviceDiscovered(BluetoothAdapterEventArgs e)
        {
            e.Device.Connected += Device_Connected;
            e.Device.Disconnected += Device_Disconnected;

            DeviceDiscovered?.Invoke(this, e);
        }

        protected virtual void OnDiscoveryFinished(BluetoothAdapterEventArgs e)
        {
            DiscoveryFinished?.Invoke(this, e);
        }

        public abstract void StartDiscovery();

        public abstract void StopDiscovery();

        public abstract void TurnOn(int resultCode);
    }
}
