using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinMarathon.view
{
    public partial class MainMasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MainMasterPage()
        {
            InitializeComponent();

            // Fills the master page side menu
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Xamarin Views",
                IconSource = "icon.png",
                TargetType = typeof(XamarinViewsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "SQLite Sample",
                IconSource = "icon.png",
                TargetType = typeof(PeoplePage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Chart Sample",
                IconSource = "icon.png",
                TargetType = typeof(ChartPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Calendar Sample",
                IconSource = "icon.png",
                TargetType = typeof(CalendarPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "AutoComplete Sample",
                IconSource = "icon.png",
                TargetType = typeof(AutoCompletePage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "DataForm Sample",
                IconSource = "icon.png",
                TargetType = typeof(DataFormPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "SOAP WS Client Sample",
                IconSource = "icon.png",
                TargetType = typeof(SoapWebServiceClient)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Rest WS Client Sample",
                IconSource = "icon.png",
                TargetType = typeof(RestWebServiceClient)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Connectivity Sample",
                IconSource = "icon.png",
                TargetType = typeof(RestWsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Barcode Sample",
                IconSource = "icon.png",
                TargetType = typeof(BarcodePage)
            });

            listView.ItemsSource = masterPageItems;

        }
    }
}
