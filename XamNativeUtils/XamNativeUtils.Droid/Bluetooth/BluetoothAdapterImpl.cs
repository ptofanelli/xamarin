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
using XamNativeUtils.Bluetooth;
using XamNativeUtils.Droid.Bluetooth;
using Android.Util;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothAdapterImpl))]
namespace XamNativeUtils.Droid.Bluetooth
{
    public class BluetoothAdapterImpl : AbstractBluetoothAdapter
    {        
        private List<BluetoothDevice> Devices;
        private Android.Bluetooth.BluetoothAdapter adapter;

        public BluetoothAdapterImpl()
        {
            Devices = new List<BluetoothDevice>();

            // Register for broadcasts when a device is discovered
            Receiver receiver = new Receiver();

            receiver.DeviceDiscovered += Receiver_DeviceDiscovered;
            receiver.DiscoveryFinished += Receiver_DiscoveryFinished;
            receiver.AdapterStateChanged += Receiver_AdapterStateChanged;

            var filter = new IntentFilter();
            filter.AddAction(Android.Bluetooth.BluetoothDevice.ActionFound);
            filter.AddAction(Android.Bluetooth.BluetoothAdapter.ActionDiscoveryFinished);
            filter.AddAction(Android.Bluetooth.BluetoothAdapter.ActionStateChanged);

            Android.App.Application.Context.RegisterReceiver(receiver, filter);
            adapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;

            State = adapter.IsEnabled ? BluetoothAdapterState.On : BluetoothAdapterState.Off;
        }

        private void Receiver_AdapterStateChanged(object sender, BluetoothAdapterEventArgs e)
        {
            State = e.State;
        }

        private void Receiver_DiscoveryFinished(object sender, BluetoothAdapterEventArgs e)
        {
            OnDiscoveryFinished(new BluetoothAdapterEventArgs(Devices));
        }

        private void Receiver_DeviceDiscovered(object sender, BluetoothAdapterEventArgs e)
        {
            Devices.Add(e.Device);
            OnDeviceDiscovered(e);
        }

        public override void StartDiscovery()
        {
            Devices.Clear();

            //If BT is not on, do not start discovering
            if (State == BluetoothAdapterState.Off)
            {
                OnDiscoveryFinished(new BluetoothAdapterEventArgs(Devices));
                return;
            }

            if (adapter.IsDiscovering)
            {
                adapter.CancelDiscovery();
            }

            adapter.StartDiscovery();

            System.Diagnostics.Debug.WriteLine("StartDiscover");
        }

        public override void StopDiscovery()
        {
            adapter.CancelDiscovery();
            System.Diagnostics.Debug.WriteLine("StopDiscover");
        }

        public override void TurnOn(int resultCode)
        {
            //If BT is not on, request that it be enabled.
            if (State == BluetoothAdapterState.Off)
            {
                Intent enableIntent = new Intent(Android.Bluetooth.BluetoothAdapter.ActionRequestEnable);

                Activity activity = (Activity)Xamarin.Forms.Forms.Context;
                activity.StartActivityForResult(enableIntent, resultCode);
            }
        }

    }

    public class Receiver : BroadcastReceiver
    {
        public event EventHandler<BluetoothAdapterEventArgs> DeviceDiscovered;
        public event EventHandler<BluetoothAdapterEventArgs> DiscoveryFinished;
        public event EventHandler<BluetoothAdapterEventArgs> AdapterStateChanged;

        public Receiver() { }

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            // When discovery finds a device
            if (action == Android.Bluetooth.BluetoothDevice.ActionFound)
            {
                // Get the BluetoothDevice object from the Intent
                Android.Bluetooth.BluetoothDevice droidDevice = (Android.Bluetooth.BluetoothDevice)intent.GetParcelableExtra(Android.Bluetooth.BluetoothDevice.ExtraDevice);
                // If it's already paired, skip it, because it's been listed already
                //if (device.BondState != Bond.Bonded)
                //{
                // Notify bluetooth event handler that a device was found
                BluetoothDevice device = new BluetoothDeviceImpl(droidDevice.Name, droidDevice.Address);
                DeviceDiscovered?.Invoke(this, new BluetoothAdapterEventArgs(device));
                
                Log.Debug("BL-SAMPLE", device.Name + "-" + device.Address);
                //}
                // When discovery is finished, change the Activity title
            }
            else if (action == Android.Bluetooth.BluetoothAdapter.ActionDiscoveryFinished)
            {
                // Notify bluetooth event handler that the discover finished
                DiscoveryFinished?.Invoke(this, new BluetoothAdapterEventArgs());

                Log.Debug("BL-SAMPLE", "discovery finished");
            }
            else if (action == Android.Bluetooth.BluetoothAdapter.ActionStateChanged)
            {
                int state = intent.GetIntExtra(Android.Bluetooth.BluetoothAdapter.ExtraState, Android.Bluetooth.BluetoothAdapter.Error);

                Android.Bluetooth.State AdapterState = (Android.Bluetooth.State) Enum.ToObject(Android.Bluetooth.State.On.GetType(), state);

                switch (AdapterState)
                {
                    case Android.Bluetooth.State.Off:
                        AdapterStateChanged?.Invoke(this, new BluetoothAdapterEventArgs(BluetoothAdapterState.Off));
                        break;
                    case Android.Bluetooth.State.TurningOff:
                        break;
                    case Android.Bluetooth.State.On:
                        AdapterStateChanged?.Invoke(this, new BluetoothAdapterEventArgs(BluetoothAdapterState.On));
                        break;
                    case Android.Bluetooth.State.TurningOn:
                        break;
                }
            }
        }
    }
}