using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamNativeUtils.Control;
using XamNativeUtils.Droid.Control;

[assembly: ExportRenderer(typeof(RoundedEditor), typeof(RoundedEditorRenderer))]

namespace XamNativeUtils.Droid.Control
{
    public class RoundedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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