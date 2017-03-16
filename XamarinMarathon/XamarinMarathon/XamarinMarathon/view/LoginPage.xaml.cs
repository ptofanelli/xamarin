using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinMarathon.model;

namespace XamarinMarathon.view
{
    public partial class LoginPage : ContentPage
    {
        public string User { get; set; }
        public string Password { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;

            loading.IsRunning = false;
            loading.IsVisible = false;
        }

        public void Login(Object sender, EventArgs e)
        {
            loading.IsVisible = true;
            loading.IsRunning = true;

            Task.Run(() => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    loading.IsRunning = false;
                    loading.IsVisible = false;

                    App.Current.MainPage = new MainPage();
                });
            });
        }

        void loginCompletedCallback()
        {
            
        }
    }
}
