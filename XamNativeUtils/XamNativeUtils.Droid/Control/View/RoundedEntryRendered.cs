using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamNativeUtils.Control;
using XamNativeUtils.Droid.Control;
using System.ComponentModel;
using Android.Views.InputMethods;
using System;
using Android.Widget;
using Android.Views;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRendered))]

namespace XamNativeUtils.Droid.Control
{
    public class RoundedEntryRendered : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedEntry);
                Control.SetPadding(1, 1, 1, 1);
                Control.SetTextColor(Android.Graphics.Color.White);

                if (e.NewElement != null) {

                    var entryExt = (e.NewElement as RoundedEntry);
                    Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
                    // This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
                    Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);

                    Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                    {
                        if (entryExt?.ReturnKeyType.GetValueFromDescription() != ImeAction.Next)
                            entryExt?.Unfocus();

                        entryExt?.InvokeCompleted();
                    };

                    // Vertical text align
                    Entry entry = e.NewElement;
                    if (entry.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                    {
                        this.Control.Gravity = GravityFlags.Center;
                    }
                    else
                    {
                        this.Control.Gravity = GravityFlags.CenterVertical;
                    }
                }

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RoundedEntry.ReturnKeyPropertyName)
            {
                var entryExt = (sender as RoundedEntry);
                Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
                // This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
                Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
            }
        }

    }

    public static class EnumExtensions
    {
        public static ImeAction GetValueFromDescription(this ReturnKeyTypes value)
        {
            var type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
            }
            throw new NotSupportedException($"Not supported on Android: {value}");
        }
    }


}