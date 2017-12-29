using System;
using Xamarin.Forms;

namespace XamNativeUtils.Control
{
    public class RoundedEntry : Entry
    {
        public const string ReturnKeyPropertyName = "ReturnKeyType";
        public const string FieldTypePropertyName = "FieldType";

        public new event EventHandler Completed;

        public RoundedEntry() { }

        public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
        propertyName: ReturnKeyPropertyName,
        returnType: typeof(ReturnKeyTypes),
        declaringType: typeof(RoundedEntry),
        defaultValue: ReturnKeyTypes.Done);

        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnKeyTypeProperty); }
            set { SetValue(ReturnKeyTypeProperty, value); }
        }

        public static readonly BindableProperty FieldTypeProperty = BindableProperty.Create(
        propertyName: FieldTypePropertyName,
        returnType: typeof(FieldTypes),
        declaringType: typeof(RoundedEntry),
        defaultValue: FieldTypes.Default);

        public FieldTypes FieldType
        {
            get { return (FieldTypes)GetValue(FieldTypeProperty); }
            set { SetValue(FieldTypeProperty, value); }
        }

        public void InvokeCompleted()
        {
            Completed?.Invoke(this, null);
        }

    }

    // Not all of these are support on Android, consult EntryEditText.ImeOptions
    public enum ReturnKeyTypes : int
    {
        Default,
        Go,
        Google,
        Join,
        Next,
        Route,
        Search,
        Send,
        Yahoo,
        Done,
        EmergencyCall,
        Continue
    }

    public enum FieldTypes : int
    {
        Default,
        Search
    }
}
