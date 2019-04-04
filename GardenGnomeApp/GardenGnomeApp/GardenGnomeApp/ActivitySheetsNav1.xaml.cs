using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// database imports
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
// database imports end

namespace GardenGnomeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitySheetsNav1 : ContentPage
    {
        public ActivitySheetsNav1()
        {
            InitializeComponent();
            DatabaseGet();
        }

        // Settings page button
        async void Clicked1(object sender, EventArgs e)
        {
            SettingsPage.inGame = false;
            await Navigation.PushAsync(new SettingsPage());
        }

        // database code
        private static readonly HttpClient client = new HttpClient();

        // copy and paste a test result and use visual studio edit > paste special > Paste JSON as Classes

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
            public string name { get; set; }
            public string description { get; set; }
            public DateTime publishDate { get; set; }
            public DateTime expiryDate { get; set; }
            public Step[] steps { get; set; }
            public Activityimage[] activityImages { get; set; }
            public int ageBrackets { get; set; }
            public int id { get; set; }
        }

        public class Step
        {
            public int stepNumber { get; set; }
            public string stepDescription { get; set; }
            public Stepimage[] stepImage { get; set; }
            public bool isDeleted { get; set; }
            public int? deleterUserId { get; set; }
            public DateTime? deletionTime { get; set; }
            public DateTime? lastModificationTime { get; set; }
            public int? lastModifierUserId { get; set; }
            public DateTime creationTime { get; set; }
            public int creatorUserId { get; set; }
            public int id { get; set; }
        }

        public class Stepimage
        {
            public string imageName { get; set; }
            public string altText { get; set; }
            public object size { get; set; }
            public object contentType { get; set; }
            public object width { get; set; }
            public object height { get; set; }
            public bool isDeleted { get; set; }
            public object deleterUserId { get; set; }
            public object deletionTime { get; set; }
            public object lastModificationTime { get; set; }
            public object lastModifierUserId { get; set; }
            public DateTime creationTime { get; set; }
            public int creatorUserId { get; set; }
            public int id { get; set; }
        }

        public class Activityimage
        {
            public string imageName { get; set; }
            public string altText { get; set; }
            public object size { get; set; }
            public object contentType { get; set; }
            public object width { get; set; }
            public object height { get; set; }
            public bool isDeleted { get; set; }
            public object deleterUserId { get; set; }
            public object deletionTime { get; set; }
            public object lastModificationTime { get; set; }
            public object lastModifierUserId { get; set; }
            public DateTime creationTime { get; set; }
            public int creatorUserId { get; set; }
            public int id { get; set; }
        }


        // database get and response
        public async void DatabaseGet()
        {
            Rootobject result = null;
            HttpResponseMessage task = await client.GetAsync("http://test.gardengnome.info/api/services/app/Activity/GetAll?SkipCount=0&MaxResultCount=999");
            var jsonString = task.Content.ReadAsStringAsync();
            jsonString.Wait();
            result = JsonConvert.DeserializeObject<Rootobject>(jsonString.Result);
            //View being added after fetching data from database
            ActivityStack.Margin = new Thickness(10, 10, 10, 20);
            ActivityStack.Children.Add(new Label {Text = "List of Activity Sheets", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center });
            for (var i = 0; i < result.result.items.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("looping " + i);
                Button button = new Button();
                button.Clicked += new EventHandler(ActivitySheets2Clicked);
                button.FontSize = 24;
                button.WidthRequest = 200;
                button.HeightRequest = 40;
                button.VerticalOptions = LayoutOptions.StartAndExpand;
                button.HorizontalOptions = LayoutOptions.Center;
                button.Text = result.result.items[i].name;
                button.ClassId = result.result.items[i].id.ToString(); 
                ActivityStack.Children.Add(button);
            }
        }
        // Database code end

        // User clicks on activity sheets
        async void ActivitySheets2Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string id = button.ClassId;
            await Navigation.PushAsync(new ActivitySheetsNav2(id));
        }



    }
}