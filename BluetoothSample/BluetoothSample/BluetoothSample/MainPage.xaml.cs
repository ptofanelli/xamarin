using BluetoothSample.Bluetooth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BluetoothSample
{
    public partial class MainPage : ContentPage, IBluetoothEventHandler
    {
        private IBluetoothAdapter bt;
        private ObservableCollection<BtDevice> devices;
        public MainPage(IBluetoothAdapter bt)
        {
            InitializeComponent();

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;

            this.bt = bt;

            bt.eventHandler = this;
            deviceState.Text = "Disconnected";
        }

        public void StartScan(Object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;

            devices = new ObservableCollection<BtDevice>();
            devicesListView.ItemsSource = devices;

            bt.scan();
        }

        public void DeviceFound(BtDevice device)
        {
            //Device found
            Device.BeginInvokeOnMainThread(() =>
            {
                devices.Add(device);
                devicesListView.ItemsSource = devices;
            });
        }

        public void DiscoverFinished()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;

                devicesListView.ItemsSource = bt.devices;
            });
        }

        // Device listview item selected
        void OnSelection(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            bt.connect((BtDevice)e.Item);
            //DisplayAlert("Item Selected", e.Item.ToString(), "Ok");
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

        public void BytesRecieved(int count, byte[] bytes)
        {
            if (count >= 16)
            {
                var str = System.Text.Encoding.UTF8.GetString(bytes, 0, count);
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Bluetooth", str, "OK");
                });
            }

        }

        public void StateChanged(DeviceState state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                
                switch(state)
                {
                    case DeviceState.Connected:
                        deviceState.Text = "Connected";
                        //DisplayAlert("State changed", "Connected", "OK");
                        break;

                    case DeviceState.Connecting:
                        deviceState.Text = "Connecting...";
                        //DisplayAlert("State changed", "Connecting...", "OK");
                        break;

                    case DeviceState.Disconnected:
                        deviceState.Text = "Disconnected";
                        break;
                }
            });
        }
    }
}
