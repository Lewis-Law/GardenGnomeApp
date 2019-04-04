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
	public partial class TicTacToeAvatar : ContentPage
	{
        bool aiMode;
        public TicTacToeAvatar (bool ai)
		{
            aiMode = ai;
			InitializeComponent ();
        }

        //Settings page button
        async void Clicked7(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }

        string playerOne = "";
        string playerTwo = "";
        // Detect Avatar Image Tappped 
        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Image I = (Image)sender;
             System.Diagnostics.Debug.WriteLine((FileImageSource)I.Source);
            if (playerOne == "")
            {
                // Assign player one avatar and grey it out and disable user interaction
                playerOne = (FileImageSource)I.Source;
                I.IsEnabled = false;
                I.BackgroundColor = Color.FromHex("706F6F");
                I.Source = playerOne.Replace(".png", "Grey.png");
                System.Diagnostics.Debug.WriteLine("playerOne: " + playerOne);
                avatarLabel.Text = "Player Two, Choose Your Avatar";
            } else if (playerTwo == "")
            {
                // Assign player two avatar and pushes to the game while resetting this page's variables
                playerTwo = (FileImageSource)I.Source;
                System.Diagnostics.Debug.WriteLine("playerTwo: " + playerTwo);
                PushToTicTacToe();
                playerOne = "";
                playerTwo = "";
                avatarLabel.Text = "Player One, Choose Your Avatar";
                I1.Source = "ggapple.png";
                I2.Source = "ggbird.png";
                I3.Source = "ggbutterfly.png";
                I4.Source = "ggflower.png";
                I5.Source = "ggleaf";
                I6.Source = "ggtree";
                I1.BackgroundColor = Color.White;
                I2.BackgroundColor = Color.White;
                I3.BackgroundColor = Color.White;
                I4.BackgroundColor = Color.White;
                I5.BackgroundColor = Color.White;
                I6.BackgroundColor = Color.White;   
                I1.IsVisible = true;
                I2.IsVisible = true;
                I3.IsVisible = true;
                I4.IsVisible = true;
                I5.IsVisible = true;
                I6.IsVisible = true;
                I1.IsEnabled = true;
                I2.IsEnabled = true;
                I3.IsEnabled = true;
                I4.IsEnabled = true;
                I5.IsEnabled = true;
                I6.IsEnabled = true;
            } else
            {
                System.Diagnostics.Debug.WriteLine("error"+ playerOne + "," + playerTwo);
            }

        }

        // Used for pushing to the game depending on which mode the player has chosen in previous page
        async void PushToTicTacToe()
        {
            if (aiMode == false)
            {
                await Navigation.PushAsync(new TicTacToe(playerOne, playerTwo, aiMode));
            } else {
                await Navigation.PushAsync(new TicTacToe(playerOne, playerTwo, aiMode));
            }

           
        }
    }
}