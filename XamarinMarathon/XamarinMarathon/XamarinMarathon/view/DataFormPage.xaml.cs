using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace XamarinMarathon.view
{
    public partial class DataFormPage : ContentPage
    {
        public DataFormPage()
        {
            InitializeComponent();

            dataForm.RegisterEditor(nameof(SourceItem.Age), EditorType.IntegerEditor);
            dataForm.RegisterEditor(nameof(SourceItem.Name), EditorType.TextEditor);
            dataForm.RegisterEditor(nameof(SourceItem.Weight), EditorType.DecimalEditor);
            dataForm.RegisterEditor(nameof(SourceItem.Height), EditorType.IntegerEditor);
        }
    }
}
