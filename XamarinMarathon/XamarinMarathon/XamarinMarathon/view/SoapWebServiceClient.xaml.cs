using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarathon.Calculator;

namespace XamarinMarathon.view
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoapWebServiceClient : ContentPage
    {
        public SoapWebServiceClient()
        {
            InitializeComponent();
            BindingContext = new SoapWebServiceClientViewModel();
        }

        
    }

    class SoapWebServiceClientViewModel : INotifyPropertyChanged
    {

        private CalculatorClient client;

        public SoapWebServiceClientViewModel()
        {
            Add = new Command(add);
            Subtract = new Command(subtract);
            Multiply = new Command(multiply);
            Divide = new Command(divide);

            client = new Calculator.CalculatorClient();
            client.addCompleted += addCompletedCallBack;
            client.subtractCompleted += subtractCompletedCallBack;
            client.multiplyCompleted += multiplyCompletedCallBack;
            client.divideCompleted += devideCompletedCallBack;
        }

        string countDisplay;
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        float value1;
        public float Value1
        {
            get { return value1; }
            set { value1 = value; OnPropertyChanged(); }
        }
        float value2;
        public float Value2
        {
            get { return value2; }
            set { value2 = value; OnPropertyChanged(); }
        }

        public ICommand Add { get; }
        public ICommand Subtract { get; }
        public ICommand Multiply { get; }
        public ICommand Divide { get; }


        void add() => client.addAsync(Value1, Value2);
        void subtract() => client.subtractAsync(Value1, Value2);
        void multiply() => client.multiplyAsync(Value1, Value2);
        void divide() => client.divideAsync(Value1, Value2);

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void addCompletedCallBack(object sender, Calculator.addCompletedEventArgs e)
        {
            CountDisplay = $"Result: {e.Result}";
        }
        void devideCompletedCallBack(object sender, divideCompletedEventArgs e)
        {
            CountDisplay = $"Result: {e.Result}";
        }
        void subtractCompletedCallBack(object sender, Calculator.subtractCompletedEventArgs e)
        {
            CountDisplay = $"Result: {e.Result}";
        }
        void multiplyCompletedCallBack(object sender, Calculator.multiplyCompletedEventArgs e)
        {
            CountDisplay = $"Result: {e.Result}";
        }
    }
}
