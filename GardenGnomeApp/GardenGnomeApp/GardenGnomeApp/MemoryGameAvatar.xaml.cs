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
	public partial class MemoryGameAvatar : ContentPage
	{
        public MemoryGameAvatar ()
		{
			InitializeComponent ();
		}

        // Settings page button
        async void Clicked8(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }

        string Avatar = "";
        // Detect Avatar Image Tappped 
        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Image I = (Image)sender;
            System.Diagnostics.Debug.WriteLine((FileImageSource)I.Source);
            Avatar = (FileImageSource)I.Source;
            PushToMemoryGame();


        }

        // Navigation to memory game depending on difficulty settings in settings page
        async void PushToMemoryGame()
        {
            if (SettingsPage.savedDifficulty == "Easy")
            {
                await Navigation.PushAsync(new MemoryGameEasy(Avatar));
            }
            else if (SettingsPage.savedDifficulty == "Moderate")
            {
                await Navigation.PushAsync(new MemoryGame(Avatar));
            }
            else if (SettingsPage.savedDifficulty == "Hard")
            {
                await Navigation.PushAsync(new MemoryGameHard(Avatar));
            }
            else
            {
                //Should never run as moderate default value is included in settings
                await DisplayAlert("Error", "Difficulty not selected", "OK");
            }
            
        }

	}
}