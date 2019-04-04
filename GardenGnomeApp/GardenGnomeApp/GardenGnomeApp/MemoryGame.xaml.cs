using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class MemoryGame : ContentPage
    {
        string avatar = "";
        public MemoryGame(string avatarChosen)
        {
            avatar = avatarChosen;
            InitializeComponent();
            //Determine if offline mode before fetching data from database
            if (SettingsPage.savedOfflineMode == "false")
            {
                DatabaseGet();
            }
            // Else, initialise from local storage
            else
            {
                Pair1 = "BiggusFakeus.jpg";
                Pair2 = "FencingHedge.jpg";
                Pair3 = "GreenGrass.jpg";
                Pair4 = "PetiteFlower.jpg";
                Pair5 = "PlasticPlant.jpg";
                Pair6 = "ShortShrub.jpg";
            }
            InitialiseBoard();
            
        }

        // Settings page button
        async void Clicked3(object sender, EventArgs e)
        {
            SettingsPage.inGame = true;
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
        string[] PlantsImageArr = new string[] { };
        public async void DatabaseGet()
        {
            I00.IsEnabled = false;
            I10.IsEnabled = false;
            I20.IsEnabled = false;
            I01.IsEnabled = false;
            I11.IsEnabled = false;
            I21.IsEnabled = false;
            I02.IsEnabled = false;
            I12.IsEnabled = false;
            I22.IsEnabled = false;
            I03.IsEnabled = false;
            I13.IsEnabled = false;
            I23.IsEnabled = false;
            PlayAgainButton.IsEnabled = false;
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
                    }
                }
            }
            PlantsImageArr = PlantList.ToArray();
            System.Diagnostics.Debug.WriteLine("PlantsImageArr Length: " + PlantsImageArr.Length);
            Pair1 = PlantsImageArr[0];
            Pair2 = PlantsImageArr[1];
            Pair3 = PlantsImageArr[2];
            Pair4 = PlantsImageArr[3];
            Pair5 = PlantsImageArr[4];
            Pair6 = PlantsImageArr[5];
            I00.IsEnabled = true;
            I10.IsEnabled = true;
            I20.IsEnabled = true;
            I01.IsEnabled = true;
            I11.IsEnabled = true;
            I21.IsEnabled = true;
            I02.IsEnabled = true;
            I12.IsEnabled = true;
            I22.IsEnabled = true;
            I03.IsEnabled = true;
            I13.IsEnabled = true;
            I23.IsEnabled = true;
            PlayAgainButton.IsEnabled = true;

        }
        // Database code end

        // Initialising the game
        string Pair1 = "flowerIcon.png";
        string Pair2 = "flowerIcon2.png";
        string Pair3 = "flowerIcon3.png";
        string Pair4 = "flowerIcon4.png";
        string Pair5 = "flowerIcon5.png";
        string Pair6 = "leafIcon.png";
        int Remain = 0;
        double Flipped = 0;
        private void InitialiseBoard()
        {
            // Image background colour to white
            I00.BackgroundColor = Color.White;
            I10.BackgroundColor = Color.White;
            I20.BackgroundColor = Color.White;
            I01.BackgroundColor = Color.White;
            I11.BackgroundColor = Color.White;
            I21.BackgroundColor = Color.White;
            I02.BackgroundColor = Color.White;
            I12.BackgroundColor = Color.White;
            I22.BackgroundColor = Color.White;
            I03.BackgroundColor = Color.White;
            I13.BackgroundColor = Color.White;
            I23.BackgroundColor = Color.White;
            // Initialise avatar
            I00.Source = string.Format("{0}", avatar);
            I10.Source = string.Format("{0}", avatar);
            I20.Source = string.Format("{0}", avatar);
            I01.Source = string.Format("{0}", avatar);
            I11.Source = string.Format("{0}", avatar);
            I21.Source = string.Format("{0}", avatar);
            I02.Source = string.Format("{0}", avatar);
            I12.Source = string.Format("{0}", avatar);
            I22.Source = string.Format("{0}", avatar);
            I03.Source = string.Format("{0}", avatar);
            I13.Source = string.Format("{0}", avatar);
            I23.Source = string.Format("{0}", avatar);

            //Class Id used for image values
            I00.ClassId = "";
            I10.ClassId = "";
            I20.ClassId = "";
            I01.ClassId = "";
            I11.ClassId = "";
            I21.ClassId = "";
            I02.ClassId = "";
            I12.ClassId = "";
            I22.ClassId = "";
            I03.ClassId = "";
            I13.ClassId = "";
            I23.ClassId = "";


            //Initialising Image values
            Image card = InitialiseCard();
            card.ClassId = "1";
            card = InitialiseCard();
            card.ClassId = "1";
            card = InitialiseCard();
            card.ClassId = "2";
            card = InitialiseCard();
            card.ClassId = "2";
            card = InitialiseCard();
            card.ClassId = "3";
            card = InitialiseCard();
            card.ClassId = "3";
            card = InitialiseCard();
            card.ClassId = "4";
            card = InitialiseCard();
            card.ClassId = "4";
            card = InitialiseCard();
            card.ClassId = "5";
            card = InitialiseCard();
            card.ClassId = "5";
            card = InitialiseCard();
            card.ClassId = "6";
            card = InitialiseCard();
            card.ClassId = "6";
            Remain = 6;
            Flipped = 0;
            PairsRemain.Text = string.Format("{0}", Remain);
            PairsFlipped.Text = string.Format("{0}", Flipped);
        }

        // Used to add values to each image
        private Image InitialiseCard()
        {
            System.Diagnostics.Debug.WriteLine("Initialising Board");
            Image card = null;
            while (card == null)
            {
                card = RandomCard();
            }
            return card;
        }

        // Randomising values
        private Image RandomCard()
        {

            Random rnd = new Random();
            int anyCard = rnd.Next(1, 13);
            if ((I11.ClassId == "") && (anyCard == 1))
                return I11;
            if ((I10.ClassId == "") && (anyCard == 2))
                return I10;
            if ((I01.ClassId == "") && (anyCard == 3))
                return I01;
            if ((I21.ClassId == "") && (anyCard == 4))
                return I21;
            if ((I12.ClassId == "") && (anyCard == 5))
                return I12;
            if ((I00.ClassId == "") && (anyCard == 6))
                return I00;
            if ((I20.ClassId == "") && (anyCard == 7))
                return I20;
            if ((I02.ClassId == "") && (anyCard == 8))
                return I02;
            if ((I22.ClassId == "") && (anyCard == 9))
                return I22;
            if ((I03.ClassId == "") && (anyCard == 10))
                return I03;
            if ((I13.ClassId == "") && (anyCard == 11))
                return I13;
            if ((I23.ClassId == "") && (anyCard == 12))
                return I23;

            return null;
        }

        // The Game
        int NoCards = 0;
        Image Card1 = null;
        Image Card2 = null;
        
        // Detect Image Tap
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Image I = (Image)sender;
            System.Diagnostics.Debug.WriteLine("Image Tap running");
            //Show Tapped Image
            if (I.ClassId == "1")
            {
                I.Source = Pair1;
            }
            else if (I.ClassId == "2")
            {
                I.Source = Pair2;
            }
            else if (I.ClassId == "3")
            {
                I.Source = Pair3;
            }
            else if (I.ClassId == "4")
            {
                I.Source = Pair4;
            }
            else if (I.ClassId == "5")
            {
                I.Source = Pair5;
            }
            else if (I.ClassId == "6")
            {
                I.Source = Pair6;
            }
            NoCards += 1;

            if (NoCards == 1)
            {
                // Disable first image tapped
                Card1 = I;
                I.IsEnabled = false;
            }
            if (NoCards == 2)
            {
                // Disable all images when second image tapped
                Card2 = I;
                System.Diagnostics.Debug.WriteLine("2 cards condition running");
                // Delay for user interface to remember cards and disable all User interaction
                I00.IsEnabled = false;
                I10.IsEnabled = false;
                I20.IsEnabled = false;
                I01.IsEnabled = false;
                I11.IsEnabled = false;
                I21.IsEnabled = false;
                I02.IsEnabled = false;
                I12.IsEnabled = false;
                I22.IsEnabled = false;
                I03.IsEnabled = false;
                I13.IsEnabled = false;
                I23.IsEnabled = false;
                Flipped += 1;
                PairsFlipped.Text = string.Format("{0}", Flipped);
                await Task.Delay(2000);
                // Check for matching pair by using I.classId
                // If matching pair grey it out and disable user interaction
                if (Card1.ClassId == Card2.ClassId)
                {
                    Card1.ClassId = "grey";
                    Card2.ClassId = "grey";
                    if (SettingsPage.savedOfflineMode == "true")
                    {
                        Card1.Opacity = 0.25;
                        Card2.Opacity = 0.25;
                    }
                    else
                    {
                        Card1.Source = "grey.png";
                        Card2.Source = "grey.png";
                    }
                    Card1.BackgroundColor = Color.FromHex("706F6F");
                    Card2.BackgroundColor = Color.FromHex("706F6F");
                    Remain -= 1;
                    PairsRemain.Text = string.Format("{0}", Remain);
                }
                // Then reset UI
                if (Card1.ClassId != "grey")
                {
                    Card1.Source = string.Format("{0}", avatar);
                    Card2.Source = string.Format("{0}", avatar);
                }
                if (I00.ClassId != "grey")
                { I00.IsEnabled = true; }
                if (I10.ClassId != "grey")
                { I10.IsEnabled = true; }
                if (I20.ClassId != "grey")
                { I20.IsEnabled = true; }
                if (I01.ClassId != "grey")
                { I01.IsEnabled = true; }
                if (I11.ClassId != "grey")
                { I11.IsEnabled = true; }
                if (I21.ClassId != "grey")
                { I21.IsEnabled = true; }
                if (I02.ClassId != "grey")
                { I02.IsEnabled = true; }
                if (I12.ClassId != "grey")
                { I12.IsEnabled = true; }
                if (I22.ClassId != "grey")
                { I22.IsEnabled = true; }
                if (I03.ClassId != "grey")
                { I03.IsEnabled = true; }
                if (I13.ClassId != "grey")
                { I13.IsEnabled = true; }
                if (I23.ClassId != "grey")
                { I23.IsEnabled = true; }
                NoCards = 0;
                // Check for win
                if (
                I00.ClassId == "grey" &&
                I10.ClassId == "grey" &&
                I20.ClassId == "grey" &&
                I01.ClassId == "grey" &&
                I11.ClassId == "grey" &&
                I21.ClassId == "grey" &&
                I02.ClassId == "grey" &&
                I12.ClassId == "grey" &&
                I22.ClassId == "grey" &&
                I23.ClassId == "grey" &&
                I03.ClassId == "grey" &&
                I13.ClassId == "grey"
                )
                {
                    var answer = await DisplayAlert("Game Over", string.Format("You Win! \n {0} Attempts.", Flipped), "Play Again", "Quit");
                    System.Diagnostics.Debug.WriteLine("Answer: " + answer);
                    if (answer == false)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        ResetClicked();
                    }
                }

            }
        }

        // Used for restting the game
        private void ResetClicked()
        {
            System.Diagnostics.Debug.WriteLine("Resetting game");
            I00.Opacity = 1;
            I10.Opacity = 1;
            I20.Opacity = 1;
            I01.Opacity = 1;
            I11.Opacity = 1;
            I21.Opacity = 1;
            I02.Opacity = 1;
            I12.Opacity = 1;
            I22.Opacity = 1;
            I03.Opacity = 1;
            I13.Opacity = 1;
            I23.Opacity = 1;
           I00.ClassId = "";
            I10.ClassId = "";
            I20.ClassId = "";
            I01.ClassId = "";
            I11.ClassId = "";
            I21.ClassId = "";
            I02.ClassId = "";
            I12.ClassId = "";
            I22.ClassId = "";
            I03.ClassId = "";
            I13.ClassId = "";
            I23.ClassId = "";
            I00.Source = string.Format("{0}", avatar);
            I10.Source = string.Format("{0}", avatar);
            I20.Source = string.Format("{0}", avatar);
            I01.Source = string.Format("{0}", avatar);
            I11.Source = string.Format("{0}", avatar);
            I21.Source = string.Format("{0}", avatar);
            I02.Source = string.Format("{0}", avatar);
            I12.Source = string.Format("{0}", avatar);
            I22.Source = string.Format("{0}", avatar);
            I03.Source = string.Format("{0}", avatar);
            I13.Source = string.Format("{0}", avatar);
            I23.Source = string.Format("{0}", avatar);
            I00.IsEnabled = true;
            I10.IsEnabled = true;
            I20.IsEnabled = true;
            I01.IsEnabled = true;
            I11.IsEnabled = true;
            I21.IsEnabled = true;
            I02.IsEnabled = true;
            I12.IsEnabled = true;
            I22.IsEnabled = true;
            I03.IsEnabled = true;
            I13.IsEnabled = true;
            I23.IsEnabled = true;
            NoCards = 0;
            Card1 = null;
            Card2 = null;
            InitialiseBoard();
        }
    }
}