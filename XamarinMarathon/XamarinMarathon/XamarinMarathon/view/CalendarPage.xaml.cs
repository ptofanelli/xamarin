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
    public partial class CalendarPage : ContentPage
    {
        //RadCalendar calendar;
        public CalendarPage()
        {
            InitializeComponent();

            //calendar = new RadCalendar();
            calendar.SelectedDate = DateTime.Today;
            calendar.SelectionChanged += Calendar_SelectionChanged;

            calendar.AppointmentsSource = new List<Appointment>() {
                new Appointment() {
                    StartDate = DateTime.Today.AddDays(1),
                    EndDate = DateTime.Today.AddDays(2).AddTicks(-1),
                    Title = "Mom's Birthday",
                    Color = Color.Red },
                new Appointment() {
                    StartDate = DateTime.Today.AddDays(3).AddHours(17),
                    EndDate = DateTime.Today.AddDays(3).AddHours(22),
                    Title = "Big Game",
                    Color = Color.Green },
                new Appointment() {
                    StartDate = DateTime.Today.AddDays(11).AddHours(20),
                    EndDate = DateTime.Today.AddDays(12).AddHours(4),
                    Title = "Progress Party",
                    Color = Color.Red }
            };

            //layout.Children.Add(calendar);

            selectedDate.Text = "Date: ";
        }

        private void Calendar_SelectionChanged(object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<object> e)
        {
            selectedDate.Text = "Date: " + calendar.SelectedDate.ToString();
        }
    }
}
