using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.Bluetooth
{
    public class BluetoothAdapterEventArgs : EventArgs
    {
        public List<BluetoothDevice> Devices { get; private set; }

        public BluetoothDevice Device { get; private set; }

        public BluetoothAdapterState State { get; private set; }

        public BluetoothAdapterEventArgs() { }

        public BluetoothAdapterEventArgs(BluetoothDevice Device)
        {
            this.Device = Device;
        }

        public BluetoothAdapterEventArgs(List<BluetoothDevice> devices)
        {
            this.Devices = devices;
        }

        public BluetoothAdapterEventArgs(BluetoothAdapterState state)
        {
            this.State = state;
        }

    }
}
