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
	public partial class TicTacToeChoosePlayer : ContentPage
	{
        public TicTacToeChoosePlayer ()
		{
			InitializeComponent ();

		}

        // Settings page button
        async void Clicked13(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }

        bool ai;
        // Two Players Button
        async void PlayerTwoClicked()
        {
            ai = false;
            await Navigation.PushAsync(new TicTacToeAvatar(ai));
        }

        // Computer Button
        async void ComputerClicked()
        {
            ai = true;
            await Navigation.PushAsync(new TicTacToeAvatar(ai));
        }

    }
}