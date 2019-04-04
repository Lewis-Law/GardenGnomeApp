using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenGnomeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayGame : ContentPage
	{
		public PlayGame ()
		{
			InitializeComponent ();
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
        async void PlayTicTacToe(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicTacToeChoosePlayer());
        }

        async void PlayMemoryGame(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoryGameAvatar());
        }

        async void PlaySpotThePlant(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SpotThePlant());
        }
        async void Clicked4(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}