using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.Bluetooth
{
    public abstract class BluetoothDevice
    {
        private string _Name;
        public string Name {
            get { return _Name; }
            protected set
            {
                if(value == null || value.Length == 0)
                {
                    _Name = "BT Device";
                }
                else
                {
                    _Name = value;
                }
            }
        }
        public string Address { get; protected set; }

        protected const string MYUUID = "00001101-0000-1000-8000-00805f9b34fb";

        private BluetoothDeviceState state;
        public BluetoothDeviceState State {
            get
            {
                return state;
            }
            protected set
            {
                state = value; OnStateChanged(new BluetoothDeviceEventArgs(value));
            }
        } 

        public event EventHandler<BluetoothDeviceEventArgs> Connected;
        public event EventHandler<BluetoothDeviceEventArgs> Disconnected;
        public event EventHandler<BluetoothDeviceEventArgs> Connecting;
        public event EventHandler<BluetoothDeviceEventArgs> DataReceived;
        public event EventHandler<BluetoothDeviceEventArgs> StateChanged;

        protected virtual void OnConnected(BluetoothDeviceEventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        protected virtual void OnDisconnected(BluetoothDeviceEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        protected virtual void OnConnecting(BluetoothDeviceEventArgs e)
        {
            Connecting?.Invoke(this, e);
        }

        protected virtual void OnDataReceived(BluetoothDeviceEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        protected virtual void OnStateChanged(BluetoothDeviceEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        public abstract void Connect();

        public abstract void Disconnect();
    }
}
