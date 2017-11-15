using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace stgen_stfarm_xamarin.iOS.Dependencies.Bluetooth
{
    public interface IBluetooth
    {
        void ReceiveBluetoothData(int dataReceivedSize, byte[] dataReceived);
        void Disconnected();
    }
}