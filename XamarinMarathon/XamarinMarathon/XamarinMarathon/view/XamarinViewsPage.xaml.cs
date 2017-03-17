using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using XamarinMarathon.model;

namespace XamarinMarathon.view
{
    public partial class XamarinViewsPage : ContentPage
    {
        public string MainText { get; set; }
        

        public XamarinViewsPage()
        {
            MainText = "Welcome to Xamarin Forms!";
            BindingContext = this;

            InitializeComponent();

            picker.Items.Add("item1");
            picker.Items.Add("item2");
            picker.SelectedIndex = 0;

            progressBar.Progress = 0.2;

        }

        public void ButtonClick(Object sender, EventArgs e)
        {
            // animate the progression to 80%, in 250ms
            progressBar.ProgressTo(.8, 500, Easing.Linear);
        }

        public void StepperChanged(Object sender, ValueChangedEventArgs e)
        {
            stepperValue.Text = e.NewValue.ToString();
        }
    }
}
