using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinMarathon.view
{
    public partial class RestWsPage : ContentPage
    {
        public RestWsPage()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateNetworkInfo(this, null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossConnectivity.Current.ConnectivityChanged -= UpdateNetworkInfo;
        }

        private void UpdateNetworkInfo(object sender, ConnectivityChangedEventArgs e)
        {

            if (CrossConnectivity.Current != null)
            {
                lblConn.Text = CrossConnectivity.Current.IsConnected ? "Is connected" : "No Connection";
            }

            if (CrossConnectivity.Current != null && CrossConnectivity.Current.ConnectionTypes != null)
            {
                var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                lblConnType.Text = connectionType.ToString();
            }
        }
    }
}
