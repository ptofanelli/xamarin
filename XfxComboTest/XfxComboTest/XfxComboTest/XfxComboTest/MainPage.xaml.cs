using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XfxComboTest
{
    public partial class MainPage : ContentPage
    {
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().Contains(text.ToLower()))
        .OrderBy(x => x)
        .ToList();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            combobox.FloatingHintEnabled = true;
            combobox.ItemsSource = new List<string>()
            {
                "Freda Curtis",
                "Jeffery Francis",
                "Eva Lawson",
                "Emmett Santos",
                "Theresa Bryan",
                "Jenny Fuller",
                "Terrell Norris",
                "Eric Wheeler",
                "Julius Clayton",
                "Alfredo Thornton",
                "Roberto Romero",
                "Orlando Mathis",
                "Eduardo Thomas",
                "Harry Douglas",
                "Parker Blanton",
                "Leanne Motton",
                "Shanti Osborn",
                "Merry Lasker",
                "Jess Doyon",
                "Kizzie Arjona",
                "Augusta Hentz",
                "Tasha Trial",
                "Fredda Boger",
                "Megan Mowery",
                "Hong Telesco",
                "Inez Landi",
                "Taina Cordray",
                "Shantel Jarrell",
                "Soo Heidt",
                "Rayford Mahon",
                "Jenny Omarah",
                "Denita Dalke",
                "Nida Carty",
                "Sharolyn Lambson",
                "Niki Samaniego",
                "Rudy Jankowski",
                "Matha Whobrey",
                "Jessi Knouse",
                "Vena Rieser",
                "Roosevelt Boyce",
                "Kristan Swiney",
                "Lauretta Pozo",
                "Jarvis Victorine",
                "Dane Gabor"
            };

            combobox.GestureRecognizers.Add(new TapGestureRecognizer((view) => { combobox.ShowDropDown(); }));
        }

        private void combobox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void combobox_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void combobox_Focused(object sender, FocusEventArgs e)
        {
            combobox.ShowDropDown();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            combobox.ShowDropDown();
        }
    }
}
