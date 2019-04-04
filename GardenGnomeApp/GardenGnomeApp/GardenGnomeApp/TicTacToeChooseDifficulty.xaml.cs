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
	public partial class TicTacToeChooseDifficulty : ContentPage
	{
        string PlayerOne = "";
        string PlayerTwo = "";
        bool aiMode;
        string difficulty = "";

        public TicTacToeChooseDifficulty (string p1, string p2, bool ai)
		{
            PlayerOne = p1;
            PlayerTwo = p2;
            aiMode = ai;
			InitializeComponent ();
		}

        async void EasyClicked(object sender, EventArgs e)
        {
            difficulty = "Easy";
            await Navigation.PushAsync(new TicTacToe(PlayerOne, PlayerTwo, aiMode));
        }

        async void MorderateClicked(object sender, EventArgs e)
        {
            difficulty = "Moderate";
            await Navigation.PushAsync(new TicTacToe(PlayerOne, PlayerTwo, aiMode));
        }

        async void HardClicked(object sender, EventArgs e)
        {
            difficulty = "Hard";
            await Navigation.PushAsync(new TicTacToe(PlayerOne, PlayerTwo, aiMode));
        }
    }
}