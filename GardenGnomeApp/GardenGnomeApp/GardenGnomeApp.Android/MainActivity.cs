using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace GardenGnomeApp.Droid
{
    [Activity(Label = "GardenGnomeApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        SettingsPage audiotoggle = new SettingsPage();
        MediaPlayer player;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            
            player = MediaPlayer.Create(this, Resource.Raw.soundtrack1);
            //audiotoggle.Switch_Toggled();
            if (audiotoggle.audioswitch == "True")
            {
                player.Start();
            }
            else
            {
                player.Stop();
            }
            
        }

        /*
        public void Main_switch_toggle()
        {
            if (audiotoggle.audioswitch == "True")
            {
                player.Start();
                System.Diagnostics.Debug.WriteLine("start " + audiotoggle.audioswitch);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("stop " + audiotoggle.audioswitch);
                player.Stop();
            }
        }
        
        public static void Main_switch_toggle()
        {
            SettingsPage audiotoggle = new SettingsPage();
            if (audiotoggle.audioswitch == "True")
            {
                player.Start();
                System.Diagnostics.Debug.WriteLine("start " + audiotoggle.audioswitch);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("stop " + audiotoggle.audioswitch);
                player.Stop();
            }
        }
        */
    }
}

