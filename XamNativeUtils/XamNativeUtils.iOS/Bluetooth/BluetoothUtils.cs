using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ExternalAccessory;
using System.Threading.Tasks;
using CoreBluetooth;

namespace stgen_stfarm_xamarin.iOS.Dependencies.Bluetooth
{
    class BluetoothUtils : NSObject, INSStreamDelegate
    {
        private EASession session;
        private static BluetoothUtils instance;

        private IBluetooth _iBluetooth;
        public IBluetooth iBluetooth {
            protected get
            {
                return _iBluetooth;
            }
            set
            {
                _iBluetooth = value;
            }
        }

        public static BluetoothUtils getInstance()
        {
            if (instance == null)
            {
                instance = new BluetoothUtils();
            }

            return instance;
        }

        private BluetoothUtils() { }

        public List<EAAccessory> findDevices()
        {
            List<EAAccessory> accessories = new List<EAAccessory>();

            var connectedAccessories = EAAccessoryManager.SharedAccessoryManager.ConnectedAccessories;

            foreach (var a in connectedAccessories)
            {
                foreach (var protocolString in a.ProtocolStrings)
                {
                    if (protocolString.Contains("com.allflex-europe.lpr"))
                    {
                        accessories.Add(a);
                        break;
                    }
                }
            }

            return accessories;
        }

        public void connect(EAAccessory accessory)
        {
            try
            {
                if (accessory != null)
                {
                    try
                    {
                        session = new EASession(accessory, "com.allflex-europe.lpr");
                        session.Accessory.Disconnected += delegate
                        {
                            System.Diagnostics.Debug.WriteLine(" session.Accessory.Disconnected");
                            if (iBluetooth != null)
                            {
                                iBluetooth.Disconnected();
                            }
                            //new UIAlertView("Attention", "Please reconnect the reader", null, "OK").Show();
                            //disconnect(accessory);
                        };

                        session.InputStream.Delegate = this;
                        session.InputStream.Schedule(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
                        session.InputStream.Open();

                        session.OutputStream.Delegate = this;
                        session.OutputStream.Schedule(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
                        session.OutputStream.Open();

                        System.Diagnostics.Debug.WriteLine("Connected");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Connect: ERROR " + ex.Message);
                    }
                }
                else
                {
                    new UIAlertView("Attention", "No Reader connected", null, "OK").Show();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void disconnect(EAAccessory accessory)
        {
            try
            {
                if (session == null)
                    return;

                session.InputStream.Close();
                session.InputStream.Unschedule(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
                session.InputStream.Delegate = null;
                session.InputStream.Dispose();

                session.OutputStream.Close();
                session.OutputStream.Unschedule(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
                session.OutputStream.Delegate = null;
                session.OutputStream.Dispose();

                if (accessory != null)
                {
                    accessory.Dispose();
                    accessory = null;
                }

                session.Dispose();
                session = null;
                System.Diagnostics.Debug.WriteLine("Disconnected");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Disconnect: ERROR " + e.Message);
            }
        }

        public Boolean isBluetoothOn()
        {
            var bluetoothManager = new CBCentralManager();

            return (bluetoothManager.State == CBCentralManagerState.PoweredOn);
        }


        private void ReadReceivedData()
        {
            nuint BUFFER_SIZE = 1024;
            byte[] buffer = new byte[BUFFER_SIZE];
            nint bytesread;

            if (session != null && session.InputStream.HasBytesAvailable())
            {
                bytesread = session.InputStream.Read(buffer, 0, (nuint)buffer.Length);

                try
                {
                    if (bytesread > 0)
                    {
                        //string str = System.Text.Encoding.UTF8.GetString(buffer, 0, (int)bytesread);
                        //Console.WriteLine("read: " + str);
                        if (iBluetooth != null)
                        {
                            iBluetooth.ReceiveBluetoothData((int)bytesread, buffer);
                        }
                    }

                    Task.Delay(200).Wait();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("readBtSocket: ERROR " + e.Message);
                }
            }
        }

        [Export("stream:handleEvent:")]
        public void HandleEvent(NSStream theStream, NSStreamEvent streamEvent)
        {
            switch (streamEvent)
            {
                case NSStreamEvent.None:
                case NSStreamEvent.OpenCompleted:
                    break;
                case NSStreamEvent.HasBytesAvailable:
                    ReadReceivedData();
                    break;
                case NSStreamEvent.HasSpaceAvailable:
                case NSStreamEvent.ErrorOccurred:
                case NSStreamEvent.EndEncountered:
                default:
                    break;
            }
        }
    }
}