using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamNativeUtils.Droid.Control;
using XamNativeUtils.Control;

[assembly: ExportRenderer(typeof(RoundedPicker), typeof(RoundedPickerRenderer))]

namespace XamNativeUtils.Droid.Control
{
    public class RoundedPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedEntry);
                Control.SetPadding(1, 1, 1, 1);
                Control.SetTextColor(Android.Graphics.Color.White);
            }
        }

    }
}