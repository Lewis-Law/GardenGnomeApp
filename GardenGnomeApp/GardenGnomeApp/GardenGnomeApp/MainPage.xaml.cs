using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GardenGnomeApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        // Settings page button
        async void SettingsClicked(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }

        // Responsive Layout
        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                
                if (width > height)
                {
                    ScrollLayout.IsEnabled = true;
                }
                else
                {
                    ScrollLayout.IsEnabled = false;
                }
            }
        }

        // Navigation buttons
        async void PlayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayGame());
        }

        async void ActivitySheetsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActivitySheetsNav1());
        }


    }
}
