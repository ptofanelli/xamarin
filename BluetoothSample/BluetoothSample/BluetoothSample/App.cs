using BluetoothSample.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BluetoothSample
{
    public class App : Application
    {
        private IBluetoothAdapter bluetooth;

        public App(IBluetoothAdapter bluetooth)
        {
            this.bluetooth = bluetooth;
            MainPage = new NavigationPage(new MainPage(bluetooth));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
