using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BluetoothSample.Bluetooth;
using Android.Bluetooth;
using Android.Util;
using System.Threading.Tasks;
using Java.Util;
using System.Threading;

namespace BluetoothSample.Droid
{
    public class MyBluetooth : IBluetoothAdapter
    {

        // Intent request codes
        // TODO: Make into Enums
        private const int REQUEST_CONNECT_DEVICE = 1;
        private const int REQUEST_ENABLE_BT = 2;

        private List<BtDevice> devices;
        private IBluetoothEventHandler eventHandler;
        private BluetoothAdapter adapter;
        private BluetoothSocket btsocket;
        private Activity activity;
        //private static UUID MY_UUID = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");
        private static UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"); // Bluetooth Serial Port Profile
        

        List<BtDevice> IBluetoothAdapter.devices
        {
            get
            {
                return this.devices;
            }
        }

        IBluetoothEventHandler IBluetoothAdapter.eventHandler
        {
            set
            {
                this.eventHandler = value;
            }
        }

        private DeviceState _deviceState;
        public DeviceState deviceState
        {
            get
            {
                return _deviceState;
            }
            private set
            {
                _deviceState = value;
                if(eventHandler != null)
                {
                    eventHandler.StateChanged(deviceState);
                }
            }
        }

        public MyBluetooth(Activity activity)
        {
            // Register for broadcasts when a device is discovered
            Receiver receiver = new Receiver(this);
            var filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionFound);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);

            Android.App.Application.Context.RegisterReceiver(receiver, filter);
            this.activity = activity;
            adapter = BluetoothAdapter.DefaultAdapter;

            devices = new List<BtDevice>();

            this.deviceState = DeviceState.Disconnected;

            var pairedDevices = adapter.BondedDevices;
            foreach (var device in pairedDevices)
            {
                Log.Debug("BL-SAMPLE", device.Name + "\n" + device.Address);
            }
        }

        public void scan()
        {
            // If BT is not on, request that it be enabled.
            if (!adapter.IsEnabled)
            {
                Intent enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                activity.StartActivityForResult(enableIntent, REQUEST_ENABLE_BT);
                eventHandler.DiscoverFinished();
                return;
            }

            if (adapter.IsDiscovering)
            {
                adapter.CancelDiscovery();
            }
            adapter.StartDiscovery();
            Log.Debug("BL-SAMPLE", "START DISCOVERY");
        }

        public void connect(BtDevice device)
        {
            if (device != null)
            {
                deviceState = DeviceState.Connecting;

                BluetoothDevice deviceDroid = adapter.GetRemoteDevice(device.Address);

                btsocket = deviceDroid.CreateInsecureRfcommSocketToServiceRecord(MY_UUID);

                btsocket.Connect();

                Thread oThread = new Thread(new ThreadStart(readBtSocket));
                oThread.Start();

                Log.Debug("BL-SAMPLE", "BluetoothDevice " + deviceDroid.Address);

                deviceState = DeviceState.Connected;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private void readBtSocket()
        {
            byte[] buffer = new byte[1024];
            int bytes;

            while (true)
            {
                bytes = btsocket.InputStream.Read(buffer, 0, buffer.Length);
                if (bytes > 0)
                {
                    eventHandler.BytesRecieved(bytes, buffer);
                    var str = System.Text.Encoding.Default.GetString(buffer);
                    Log.Debug("BL-SAMPLE", "bytes " + bytes);
                    Log.Debug("BL-SAMPLE", "input " + str);
                }
                Task.Delay(500).Wait();
            }
        }


        public class Receiver : BroadcastReceiver
        {
            MyBluetooth myBluetooth;

            public Receiver(MyBluetooth myBluetooth)
            {
                this.myBluetooth = myBluetooth;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                // When discovery finds a device
                if (action == BluetoothDevice.ActionFound)
                {
                    // Get the BluetoothDevice object from the Intent
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    // If it's already paired, skip it, because it's been listed already
                    //if (device.BondState != Bond.Bonded)
                    //{
                        // Notify bluetooth event handler that a device was found
                        myBluetooth.eventHandler.DeviceFound(new BtDevice {
                            Name = device.Name,
                            Address = device.Address
                        });

                        // Adds found device to list
                        myBluetooth.devices.Add(new BtDevice
                        {
                            Name = device.Name,
                            Address = device.Address
                        });
                        Log.Debug("BL-SAMPLE", device.Name + "-" + device.Address);
                    //}
                    // When discovery is finished, change the Activity title
                }
                else if (action == BluetoothAdapter.ActionDiscoveryFinished)
                {
                    // Notify bluetooth event handler that the discover finished
                    myBluetooth.eventHandler.DiscoverFinished();

                    Log.Debug("BL-SAMPLE", "discovery finished");
                }
            }
        }
    }
}