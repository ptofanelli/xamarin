using BluetoothSample.Bluetooth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace BluetoothSample.UWP
{
    public class BluetoothAdapterWindows : IBluetoothAdapter
    {

        private List<BtDevice> _devices;
        public List<BtDevice> devices
        {
            get
            {
                return _devices;
            }
        }

        private IBluetoothEventHandler _eventHandler;
        public IBluetoothEventHandler eventHandler
        {
            set
            {
                _eventHandler = value;
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
                if (_eventHandler != null)
                {
                    _eventHandler.StateChanged(deviceState);
                }
            }
        }

        private WindowsBluetooth.BluetoothManager manager;
        private DeviceWatcher deviceWatcher;
        private BluetoothDevice btdevice;
        private RfcommDeviceService rfcommservice;
        private TypedEventHandler<DeviceWatcher, DeviceInformation> handlerAdded = null;
        private TypedEventHandler<DeviceWatcher, DeviceInformationUpdate> handlerUpdated = null;
        private TypedEventHandler<DeviceWatcher, DeviceInformationUpdate> handlerRemoved = null;
        private TypedEventHandler<DeviceWatcher, Object> handlerEnumCompleted = null;
        private TypedEventHandler<DeviceWatcher, Object> handlerStopped = null;

        public BluetoothAdapterWindows()
        {
            deviceState = DeviceState.Disconnected;
        }

        public void connect(BtDevice device)
        {
            getBluetoothDevice(device);
        }

        private async void getBluetoothDevice(BtDevice device)
        {
            deviceState = DeviceState.Connecting;

            btdevice = await BluetoothDevice.FromIdAsync(device.Address);

            RfcommDeviceServicesResult rfcommresult = await btdevice.GetRfcommServicesAsync();

            rfcommservice = rfcommresult.Services[0];

            // Create a socket and connect to the target
            StreamSocket _socket = new StreamSocket();
            await _socket.ConnectAsync(
                rfcommservice.ConnectionHostName,
                rfcommservice.ConnectionServiceName,
                SocketProtectionLevel.PlainSocket);

            DataReader reader = new DataReader(_socket.InputStream);
            reader.InputStreamOptions = InputStreamOptions.Partial;

            deviceState = DeviceState.Connected;

            while (true)
            {
                // Keep reading until we consume the complete stream.
                var receivedStrings = "";
                await reader.LoadAsync(20);
                while (reader.UnconsumedBufferLength > 0)
                {
                    // Note that the call to readString requires a length of "code units" 
                    // to read. This is the reason each string is preceded by its length 
                    // when "on the wire".
                    //uint bytesToRead = reader.ReadUInt32();
                    try
                    {
                        receivedStrings += reader.ReadString(reader.UnconsumedBufferLength);
                    } 
                    catch (Exception e)
                    {
                        reader.ReadBuffer(reader.UnconsumedBufferLength);
                        receivedStrings = "";
                        continue;
                    }
                    
                }

                if(receivedStrings.Length > 0)
                {
                    _eventHandler.BytesRecieved(receivedStrings.Length, Encoding.UTF8.GetBytes(receivedStrings));
                    //ConversationList.Items.Add("Received: " + chatReader.ReadString(stringLength));
                    receivedStrings = "";
                }
                

            }

        }

        public void scan()
        {
            _devices = new List<BtDevice>();
            //return new DeviceSelectorInfo() { DisplayName = "Bluetooth", Selector = "System.Devices.Aep.ProtocolId:=\"{e0cbf06c-cd8b-4647-bb8a-263b43f0f974}\"", Kind = DeviceInformationKind.AssociationEndpoint };
            string selector = "System.Devices.Aep.ProtocolId:=\"{e0cbf06c-cd8b-4647-bb8a-263b43f0f974}\"";
            //string selector = "System.Devices.Aep.ProtocolId:=\"{00001101-0000-1000-8000-00805f9b34fb}\""; // serial port bluetooth
            DeviceInformationKind kind = DeviceInformationKind.AssociationEndpoint;

           // Kind is specified in the selector info
           deviceWatcher = DeviceInformation.CreateWatcher(
                selector,
                null, // don't request additional properties for this sample
                kind);

            // Hook up handlers for the watcher events before starting the watcher
            // Device added to list
            handlerAdded = new TypedEventHandler<DeviceWatcher, DeviceInformation>((watcher, deviceInfo) =>
            {
                // Since we have the collection databound to a UI element, we need to update the collection on the UI thread.
                BtDevice device = new BtDevice
                {
                    Name = deviceInfo.Name,
                    Address = deviceInfo.Id
                };

                _devices.Add(device);
                _eventHandler.DeviceFound(device);
            });
            deviceWatcher.Added += handlerAdded;

            // Device updated
            handlerUpdated = new TypedEventHandler<DeviceWatcher, DeviceInformationUpdate>((watcher, deviceInfoUpdate) =>
            {
                //Device updated
            });
            deviceWatcher.Updated += handlerUpdated;

            // Device removed from list
            handlerRemoved = new TypedEventHandler<DeviceWatcher, DeviceInformationUpdate>((watcher, deviceInfoUpdate) =>
            {
                // Device removed
            });
            deviceWatcher.Removed += handlerRemoved;

            // Device Watcher listing completed
            handlerEnumCompleted = new TypedEventHandler<DeviceWatcher, Object>((watcher, obj) =>
            {
                _eventHandler.DiscoverFinished();
            });
            deviceWatcher.EnumerationCompleted += handlerEnumCompleted;

            // Device Watcher Stopped
            handlerStopped = new TypedEventHandler<DeviceWatcher, Object>((watcher, obj) =>
            {
                _eventHandler.DiscoverFinished();
            });
            deviceWatcher.Stopped += handlerStopped;

            // Starts devices discovering and listing
            deviceWatcher.Start();
        }
    }   
       
}
