using BluetoothLE.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothSample.Bluetooth
{
    public interface IBluetoothAdapter
    { 
        List<BtDevice> devices { get; }

        DeviceState deviceState { get; }

        IBluetoothEventHandler eventHandler { set;}

        void scan();

        void connect(BtDevice device);
    }
}
