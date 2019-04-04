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
	public partial class SettingsPage : ContentPage
	{
        public String audioswitch = "True";
		public SettingsPage ()
		{
			InitializeComponent ();
            
            // Check if user is inside a game
            // If in game, user is not allowed to change difficulty and offline/online modes
            if (inGame == false)
            {
                easyButton.IsVisible = true;
                moderateButton.IsVisible = true;
                hardButton.IsVisible = true;
                DifficultyLabel.IsVisible = true;
                OnButton.IsVisible = true;
                OffButton.IsVisible = true;
                OfflineModeLabel.IsVisible = true;
            } else
            {
                easyButton.IsVisible = false;
                moderateButton.IsVisible = false;
                hardButton.IsVisible = false;
                DifficultyLabel.IsVisible = false;
                OnButton.IsVisible = false;
                OffButton.IsVisible = false;
                OfflineModeLabel.IsVisible = false;
            }
		}

        // Audio switch not working
        public void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            System.Diagnostics.Debug.WriteLine("isToggled: " + isToggled);
            audioswitch = isToggled.ToString();
            System.Diagnostics.Debug.WriteLine("audioswitch: " + audioswitch);
            //Droid.MainActivity.Main_switch_toggle();
        }


        
        public static string savedOfflineMode = "false";
        private string unsavedOfflineMode = "";
        public static bool inGame = false; 
        public static string savedDifficulty = "Moderate" ;
        private string unsavedDifficulty = "";

        // Easy button
        private void EasyClicked()
        {
            unsavedDifficulty = "Easy";
            easyButton.IsEnabled = false;
            moderateButton.IsEnabled = true;
            hardButton.IsEnabled = true;
        }

        // Moderate button
        private void ModerateClicked()
        {
            unsavedDifficulty = "Moderate";
            easyButton.IsEnabled = true;
            moderateButton.IsEnabled = false;
            hardButton.IsEnabled = true;
        }

        // Hard Button
        private void HardClicked()
        {
            unsavedDifficulty = "Hard";
            easyButton.IsEnabled = true;
            moderateButton.IsEnabled = true;
            hardButton.IsEnabled = false;
        }

        // Online Button
        private void OnClicked()
        {
            unsavedOfflineMode = "false";
            OnButton.IsEnabled = false;
            OffButton.IsEnabled = true;

        }

        // Offline Button
        private void OffClicked()
        {
            unsavedOfflineMode = "true";
            OnButton.IsEnabled = true;
            OffButton.IsEnabled = false;

        }

        // Apply Button
        private async void ApplyClicked()
        {
            if (inGame == false && unsavedDifficulty != "")
            {
                savedDifficulty = unsavedDifficulty;
            }
            if (inGame == false && unsavedOfflineMode != "")
            {
                savedOfflineMode = unsavedOfflineMode;
                System.Diagnostics.Debug.WriteLine("OfflineMode" + savedOfflineMode);
            }
            inGame = false;
            await Navigation.PopAsync();
        }

        // Default button
        private void DefaultClicked()
        {
            ModerateClicked();
            OnClicked();
        }

        // Back button
        protected override bool OnBackButtonPressed()
        {
            inGame = false;
            base.OnBackButtonPressed();
            return false;
        }
    }
}