using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;

// Telerik Calendar Renderer
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.iOS.CalendarRenderer))]
// Telerik Charts Renderers
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Chart.RadCartesianChart), typeof(Telerik.XamarinForms.ChartRenderer.iOS.CartesianChartRenderer))]
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Chart.RadPieChart), typeof(Telerik.XamarinForms.ChartRenderer.iOS.PieChartRenderer))]
// Telerik AutoComplete Renderer
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadAutoComplete), typeof(Telerik.XamarinForms.InputRenderer.iOS.AutoCompleteRenderer))]
// Telerik DataForm Renderer
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadDataForm), typeof(Telerik.XamarinForms.InputRenderer.iOS.DataFormRenderer))]


namespace XamarinMarathon.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new Telerik.XamarinForms.InputRenderer.iOS.CalendarRenderer();
            new Telerik.XamarinForms.ChartRenderer.iOS.PieChartRenderer();
            new Telerik.XamarinForms.ChartRenderer.iOS.CartesianChartRenderer();
            new Telerik.XamarinForms.InputRenderer.iOS.AutoCompleteRenderer();
            new Telerik.XamarinForms.InputRenderer.iOS.DataFormRenderer();

            //Initialize zxing
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            global::Xamarin.Forms.Forms.Init();
            Telerik.XamarinForms.Common.iOS.TelerikForms.Init();

            App.FileAcessHelper = new FileAccessHelper();
            App appPcl = new App();
            
            LoadApplication(appPcl);

            return base.FinishedLaunching(app, options);
        }
    }
}
