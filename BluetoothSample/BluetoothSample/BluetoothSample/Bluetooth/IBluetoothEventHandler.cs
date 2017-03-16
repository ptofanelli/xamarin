using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSample.Bluetooth
{
    public interface IBluetoothEventHandler
    {
        void DeviceFound(BtDevice device);

        void DiscoverFinished();

        void BytesRecieved(int count, byte[] bytes);

        void StateChanged(DeviceState state);
    }
}
