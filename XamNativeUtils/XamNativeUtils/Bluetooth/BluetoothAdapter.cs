using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamNativeUtils.Bluetooth
{
    public interface BluetoothAdapter
    {
        event EventHandler<BluetoothAdapterEventArgs> DeviceDiscovered;
        event EventHandler<BluetoothAdapterEventArgs> DiscoveryFinished;
        event EventHandler<BluetoothAdapterEventArgs> StateChanged;
        event EventHandler<BluetoothAdapterEventArgs> DeviceConnected;
        event EventHandler<BluetoothAdapterEventArgs> DeviceDisconnected;

        BluetoothAdapterState State { get; }

        BluetoothDevice Device { get; }

        void TurnOn(int resultCode);

        void StartDiscovery();

        void StopDiscovery();

    }
}
