using Android.Content;
using Android.Graphics.Drawables;
using ContactsApp.Controls;
using ContactsApp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRender))]
namespace ContactsApp.Droid.Controls
{
    public class CustomEntryRender : EntryRenderer
    {
        public CustomEntryRender(Context context) : base(context) 
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}