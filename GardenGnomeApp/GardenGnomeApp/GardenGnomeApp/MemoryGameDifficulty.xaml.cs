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
	public partial class MemoryGameDifficulty : ContentPage
	{
        string avatar = "";
        public MemoryGameDifficulty (string a)
		{
            avatar = a;
			InitializeComponent ();
		}
       
        async void EasyClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoryGameEasy(avatar));
        }

        async void MorderateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoryGame(avatar));
        }

        async void HardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemoryGameHard(avatar));
        }
    }
}