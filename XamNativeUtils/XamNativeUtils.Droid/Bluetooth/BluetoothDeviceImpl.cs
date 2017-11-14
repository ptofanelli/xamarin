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
using Android.Bluetooth;
using System.Threading.Tasks;
using System.Threading;
using Java.Util;
using System.ComponentModel;

namespace XamNativeUtils.Droid.Bluetooth
{
    public class BluetoothDeviceImpl : XamNativeUtils.Bluetooth.BluetoothDevice
    {
        private BluetoothSocket socket;
        private Thread readSocketThread;
        private Boolean keepReadingSocket;

        public BluetoothDeviceImpl(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public async override void Connect()
        {
            State = BluetoothDeviceState.Connecting;
            OnConnecting(new BluetoothDeviceEventArgs(this));
            BluetoothSocket tmp = null;

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                try
                {
                    System.Diagnostics.Debug.WriteLine("Creating Socket...");
                    b.ReportProgress(0);
                    Android.Bluetooth.BluetoothDevice deviceDroid = Android.Bluetooth.BluetoothAdapter.DefaultAdapter.GetRemoteDevice(Address);

                    Java.Lang.Reflect.Method createInsecureRfcommSocket = deviceDroid.Class.GetMethod("createRfcommSocket", new Java.Lang.Class[] { Java.Lang.Integer.Type });
                    tmp = (BluetoothSocket)createInsecureRfcommSocket.Invoke(deviceDroid, 1);
                    //socket = deviceDroid.CreateInsecureRfcommSocketToServiceRecord(UUID.FromString(MYUUID));
                    System.Diagnostics.Debug.WriteLine("Socket Created...");
                    b.ReportProgress(25);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Socket: ERROR " + ex.Message);
                    tmp?.Close();
                    tmp?.Dispose();
                    tmp = null;
                }

                socket = tmp;

                try
                {
                    b.ReportProgress(50);
                    System.Diagnostics.Debug.WriteLine("Socket Connecting...");
                    socket.Connect();
                    System.Diagnostics.Debug.WriteLine("Socket Connected...");
                    b.ReportProgress(75);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Connect: ERROR " + e.Message);
                    socket?.Close();
                    socket?.Dispose();
                    socket = null;
                }

                b.ReportProgress(100);

            });

            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate (object o, ProgressChangedEventArgs args)
            {
                State = BluetoothDeviceState.Connecting;
                OnConnecting(new BluetoothDeviceEventArgs(this));
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate (object o, RunWorkerCompletedEventArgs args)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Starting reading Socket...");
                    keepReadingSocket = true;
                    readSocketThread = new Thread(new ThreadStart(readBtSocket));
                    readSocketThread.Start();
                    System.Diagnostics.Debug.WriteLine("Started reading Socket...");

                    State = BluetoothDeviceState.Connected;
                    OnConnected(new BluetoothDeviceEventArgs(this));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error Stard Reading Socket >> " + e.Message);
                    State = BluetoothDeviceState.Disconnected;
                    OnDisconnected(new BluetoothDeviceEventArgs(this));
                }
            });

            bw.RunWorkerAsync();
           
        }

        public override void Disconnect()
        {
            keepReadingSocket = false;

            try
            {
                if (socket != null)
                {
                    socket?.Close();
                    socket?.Dispose();
                    socket = null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Disconnect: ERROR " + e.Message);
            }


            State = BluetoothDeviceState.Disconnected;
            OnDisconnected(new BluetoothDeviceEventArgs(this));
        }

        private void readBtSocket()
        {
            byte[] buffer = new byte[1024];
            int bytes;

            Task.Delay(1000).Wait();

            while (keepReadingSocket)
            {
                try
                {
                    if (socket != null)
                    {
                        bytes = socket.InputStream.Read(buffer, 0, buffer.Length);
                        if (bytes > 0)
                        {
                            OnDataReceived(new BluetoothDeviceEventArgs(bytes, buffer));
                        }
                        Task.Delay(200).Wait();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Socket null ...");
                    }
                    
                }
                catch (Exception e)
                {
                    Disconnect();
                    System.Diagnostics.Debug.WriteLine("readBtSocket: ERROR " + e.Message);
                    keepReadingSocket = false;
                }

            }
        }
    }
}