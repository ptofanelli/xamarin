using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace XamarinMarathon.model
{
    public class Appointment : IAppointment
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public bool IsAllDay { get; set; }

        public Color Color { get; set; }
    }
}
