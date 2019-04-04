using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Database imports
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
// Database imports end

namespace GardenGnomeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SpotThePlant : ContentPage
	{
        public SpotThePlant ()
		{
			InitializeComponent ();
            DatabaseGet();
           
        }

        // Settings page button
        async void Clicked5(object sender, EventArgs e)
        {
            SettingsPage.inGame = true;
            await Navigation.PushAsync(new SettingsPage());
        }

        // Database code
        private static readonly HttpClient client = new HttpClient();

        // Copy and paste a test result and use visual studio edit > paste special > Paste JSON as Classes
        public class Rootobject
        {
            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        }

        public class Result
        {
            public int totalCount { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public string commonName { get; set; }
            public string scientificName { get; set; }
            public object family { get; set; }
            public float minHeight { get; set; }
            public object creatorUser { get; set; }
            public float maxHeight { get; set; }
            public float minWidth { get; set; }
            public float maxWidth { get; set; }
            public bool fullSun { get; set; }
            public bool partShade { get; set; }
            public bool semiShade { get; set; }
            public bool spring { get; set; }
            public bool summer { get; set; }
            public bool autumn { get; set; }
            public bool winter { get; set; }
            public bool arid { get; set; }
            public bool tropical { get; set; }
            public bool subTropical { get; set; }
            public bool temperate { get; set; }
            public bool warm { get; set; }
            public bool cool { get; set; }
            public bool cold { get; set; }
            public bool frost { get; set; }
            public bool annual { get; set; }
            public bool biennial { get; set; }
            public bool evergreen { get; set; }
            public bool bulb { get; set; }
            public bool deciduous { get; set; }
            public float minPh { get; set; }
            public float maxPh { get; set; }
            public object geography { get; set; }
            public object[] regions { get; set; }
            public float minLongitutde { get; set; }
            public float minLatitude { get; set; }
            public float maxLatitude { get; set; }
            public float maxLongitude { get; set; }
            public bool isActive { get; set; }
            public object[] colours { get; set; }
            public object[] soilTypes { get; set; }
            public object[] breeder { get; set; }
            public Plantimage[] plantImage { get; set; }
            public object description { get; set; }
            public string id { get; set; }
        }

        public class Plantimage
        {
            public string id { get; set; }
            public string imageName { get; set; }
            public object altText { get; set; }
            public object size { get; set; }
            public string contentType { get; set; }
            public object width { get; set; }
            public object height { get; set; }
            public bool isDeleted { get; set; }
            public object deleterUserId { get; set; }
            public object deletionTime { get; set; }
            public object lastModificationTime { get; set; }
            public object lastModifierUserId { get; set; }
            public DateTime creationTime { get; set; }
            public int creatorUserId { get; set; }
        }


        // Database get and response

        List<string> PlantList = new List<string>();
        List<string> PlantsNameList = new List<string>();
        string[] PlantsImageArr = new string[] { };
        string[] PlantsNameArr = new string[] { };
        public async void DatabaseGet()
        {
            FoundButton.IsEnabled = false;
            SkipButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
            Rootobject result = null;
            HttpResponseMessage task = await client.GetAsync("http://test.gardengnome.info/api/services/app/Plant/GetAll?SkipCount=0&MaxResultCount=999");
            var jsonString = task.Content.ReadAsStringAsync();
            jsonString.Wait();
            result = JsonConvert.DeserializeObject<Rootobject>(jsonString.Result);


            for (var i = 0; i < result.result.items.Length; i++)
            {
                if (i != 10)
                {
                    if (result.result.items[i].plantImage.Length > 0)
                    {
                        string plant = ("http://cdn.gardengnome.info/images/plant/" + result.result.items[i].plantImage[0].imageName);
                        PlantList.Add(plant);
                        System.Diagnostics.Debug.WriteLine(i + "  " + result.result.items[i].commonName);
                        PlantsNameList.Add(result.result.items[i].commonName);
                    }
                }
            }
            PlantsImageArr = PlantList.ToArray();
            PlantsNameArr = PlantsNameList.ToArray();
            System.Diagnostics.Debug.WriteLine("PlantsNameArr Length: " + PlantsNameArr.Length);
            System.Diagnostics.Debug.WriteLine("PlantsImageArr Length: "+PlantsImageArr.Length);
            PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
            PlantImage.Source = PlantsImageArr[0];
            FoundButton.IsEnabled = true;
            SkipButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
        }
        // Database code end

        // Responsive layout
        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                //Reconfigure layout
                if (width > height)
                {
                    outerStack.Orientation = StackOrientation.Horizontal;
                    mainGrid.HeightRequest = height;
                    mainGrid.WidthRequest = height;
                }
                else
                {
                    outerStack.Orientation = StackOrientation.Vertical;
                    mainGrid.HeightRequest = width;
                    mainGrid.WidthRequest = width;
                }
            }
        }


        double FoundCount = 0;
        double SkipCount = 0;

        int arrayCount = 0;
        
        //Found Button
        private void FoundClicked()
        {
            FoundCount += 1;
            PlantsFound.Text = string.Format("Plants found: {0}", FoundCount);
            if (PlantsImageArr.Length - 1 > arrayCount)
            {
                arrayCount += 1;
                PlantImage.Source = PlantsImageArr[arrayCount];
                PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
            }
            else
            {
                arrayCount = 0;
                PlantImage.Source = PlantsImageArr[arrayCount];
                PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
            }
            CheckForWin();
        }

        // Skip Button
        private void SkipClicked()
        {
            SkipCount += 1;
            PlantsSkipped.Text = string.Format("Plants Skipped: {0}", SkipCount);
            if (PlantsImageArr.Length - 1 > arrayCount)
            {
                arrayCount += 1;
                PlantImage.Source = PlantsImageArr[arrayCount];
                PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
            }
            else
            {
                arrayCount = 0;
                PlantImage.Source = PlantsImageArr[arrayCount];
                PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
            }
        }


        // Check for win
        // Scores for Easy, Moderate, Hard are set to 5, 10 and 15 respectively
        private void CheckForWin()
        {
            if (SettingsPage.savedDifficulty == "Easy")
            {
                if (FoundCount == 5)
                {
                    AlertWin();
                }
            }
            else if (SettingsPage.savedDifficulty == "Moderate")
            {
                if (FoundCount == 10)
                {
                    AlertWin();
                }
            }
            else if (SettingsPage.savedDifficulty == "Hard")
            {
                if (FoundCount == 15)
                {
                    AlertWin();
                }
            }
            else
            {
                //Should never run as moderate default value is included in settings
                DisplayAlert("Error", "Difficulty not selected", "OK");
            }
        }

        // Winning Message
        async void AlertWin()
        {
            var answer = await DisplayAlert("GameOver", "You win!", "Play Again", "Quit");
            System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            if (answer == false)
            {
                    await Navigation.PopAsync();
            }
            else
            {
                ResetClicked();
            }
        }

        // Used for restting the game
        private void ResetClicked()
        {
            FoundCount = 0;
            SkipCount = 0;
            PlantsFound.Text = string.Format("Plants found: {0}", FoundCount);
            PlantsSkipped.Text = string.Format("Plants Skipped: {0}", SkipCount);
            arrayCount = 0;
            PlantImage.Source = PlantsImageArr[arrayCount];
            PlantName.Text = "Plant Name: " + PlantsNameArr[arrayCount];
        }

    }
}