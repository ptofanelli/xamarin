using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.Bluetooth
{
    public class BluetoothDeviceEventArgs : EventArgs
    {
        public byte[] DataReceived { get; private set; }

        public int DataReceivedSize { get; private set; }

        public BluetoothDeviceState State { get; private set; }

        public BluetoothDevice Device { get; private set; }

        public BluetoothDeviceEventArgs() { }

        public BluetoothDeviceEventArgs(BluetoothDevice Device)
        {
            this.Device = Device;
        }

        public BluetoothDeviceEventArgs(int dataReceivedSize, byte[] dataReceived)
        {
            this.DataReceived = dataReceived;
            this.DataReceivedSize = dataReceivedSize;
        }

        public BluetoothDeviceEventArgs(BluetoothDeviceState state)
        {
            State = state;
        }

    }
}
