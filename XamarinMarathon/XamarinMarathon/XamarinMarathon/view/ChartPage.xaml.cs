using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Chart;
using Xamarin.Forms;
using XamarinMarathon.model;

namespace XamarinMarathon.view
{
    public partial class ChartPage : ContentPage
    {
        private static string[] Categories = new string[] { "Greenings", "Perfecto", "NearBy", "Family", "Fresh" };

        public ChartPage()
        {
            InitializeComponent();

            BackgroundColor = Xamarin.Forms.Device.OnPlatform(Xamarin.Forms.Color.White, Xamarin.Forms.Color.White, Xamarin.Forms.Color.Transparent);

            var chart = new RadCartesianChart
            {
                HorizontalAxis = new Telerik.XamarinForms.Chart.CategoricalAxis()
                {
                    LabelFitMode = AxisLabelFitMode.MultiLine,
                },
                VerticalAxis = new Telerik.XamarinForms.Chart.NumericalAxis()
                {
                    LabelFitMode = AxisLabelFitMode.MultiLine,
                    RangeExtendDirection = NumericalAxisRangeExtendDirection.Both,
                    MajorStep = 30,
                },
            };

            var series = new BarSeries();
            chart.Series.Add(series);

            series.ValueBinding = new Telerik.XamarinForms.Chart.PropertyNameDataPointBinding
            {
                PropertyName = "Value"
            };

            series.CategoryBinding = new Telerik.XamarinForms.Chart.PropertyNameDataPointBinding
            {
                PropertyName = "Category"
            };

            series.ItemsSource = GetCategoricalData();

            var grid = new CartesianChartGrid();

            grid.MajorLinesVisibility = GridLineVisibility.Y;
            grid.MajorYLineDashArray = Device.OnPlatform(null, new double[] { 4, 2 }, new double[] { 4, 2 });
            grid.StripLinesVisibility = GridLineVisibility.Y;

            grid.YStripeColor = Color.FromRgba(99, 99, 99, 100);
            grid.YStripeAlternativeColor = Color.FromRgba(169, 169, 169, 31);
            grid.MajorLineColor = Color.FromRgb(211, 211, 211);
            grid.MajorLineThickness = Device.OnPlatform(0.5, 2, 2);

            chart.Grid = grid;


            var treshold = GetCategoricalData().Average(c => c.Value);
            var startTreshold = treshold * 0.95;
            var endTreshold = treshold * 1.05;

            var lineAnnotation = new CartesianGridLineAnnotation()
            {
                Axis = chart.VerticalAxis,
                Value = treshold,
                Stroke = Color.FromRgb(255, 0, 0),
                StrokeThickness = Device.OnPlatform(0.5, 2, 2),
                DashArray = new double[] { 4, 2 },

            };

            var bandAnnotation = new CartesianPlotBandAnnotation()
            {
                Axis = chart.VerticalAxis,
                From = startTreshold,
                To = endTreshold,
                Fill = Color.FromRgba(255, 0, 0, 50),
                StrokeThickness = 2,
                Stroke = Color.Transparent,
            };

            chart.Annotations.Add(bandAnnotation);
            chart.Annotations.Add(lineAnnotation);

            this.Content = chart;
        }

        public static List<CategoricalData> GetCategoricalData()
        {
            List<CategoricalData> data = new List<CategoricalData>();
            data.Add(new CategoricalData() { Value = 55, Category = Categories[0] });
            data.Add(new CategoricalData() { Value = 30, Category = Categories[1] });
            data.Add(new CategoricalData() { Value = 120, Category = Categories[2] });
            data.Add(new CategoricalData() { Value = 80, Category = Categories[3] });
            data.Add(new CategoricalData() { Value = 44, Category = Categories[4] });

            return data;
        }
    }
}
