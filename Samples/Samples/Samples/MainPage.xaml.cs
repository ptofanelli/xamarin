using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ActivityIndicator.IsEnabled = false;
            ActivityIndicator.IsVisible = false;
            ActivityIndicator.IsRunning = false;
            StackBusy.IsVisible = false;
            StackToast.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ActivityIndicator.IsEnabled = true;
            ActivityIndicator.IsVisible = true;
            ActivityIndicator.IsRunning = true;
            StackBusy.IsVisible = true;

            await Task.Delay(3000);

            ActivityIndicator.IsEnabled = false;
            ActivityIndicator.IsVisible = false;
            ActivityIndicator.IsRunning = false;
            StackBusy.IsVisible = false;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            StackToast.Opacity = 0;
            StackToast.IsVisible = true;

            await StackToast.FadeTo(0.8, 500);

            await Task.Delay(3000);

            await StackToast.FadeTo(0, 500);

            StackToast.IsVisible = false;
        }
    }
}
