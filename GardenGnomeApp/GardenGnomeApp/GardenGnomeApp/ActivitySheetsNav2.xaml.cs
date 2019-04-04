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
    public partial class ActivitySheetsNav2 : ContentPage
    {
        public ActivitySheetsNav2(string id)
        {
            ActivityID = id;
            InitializeComponent();
            DatabaseGet();
        }

        // Settings page button
        async void Clicked2(object sender, EventArgs e)
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
        string ActivityID;
        string ActivityName;
        string ActivityDescription;
        public async void DatabaseGet()
        {
            Rootobject result = null;
            HttpResponseMessage task = await client.GetAsync("http://test.gardengnome.info/api/services/app/Activity/Get?id="+ActivityID);
            var jsonString = task.Content.ReadAsStringAsync();
            jsonString.Wait();
            result = JsonConvert.DeserializeObject<Rootobject>(jsonString.Result);
            ActivityName = result.result.name;
            ActivityDescription = result.result.description;
            ActivityStack.Margin = new Thickness(10, 10, 10, 20);
            ActivityStack.Children.Add(new Label { FontSize = 24, Text = "Name: " + ActivityName, FontAttributes=FontAttributes.Bold});
            ActivityStack.Children.Add(new Label { FontSize = 24, Text = "Description: " + ActivityDescription });
            string imageURL;
            for (var i = 0; i < result.result.steps.Length; i++)
            {
                ActivityStack.Children.Add(new Label { FontSize = 16, Text = "Step : " + result.result.steps[i].stepNumber });
                ActivityStack.Children.Add(new Label { FontSize = 16, Text = result.result.steps[i].stepDescription + "\n"});
                System.Diagnostics.Debug.WriteLine(result.result.steps[i].stepImage);
                if (result.result.steps[i].stepImage != null )
                {
                    imageURL = "http://cdn.gardengnome.info/images/activities/" + ActivityID + "/" + result.result.steps[i].stepImage[0].imageName;
                    System.Diagnostics.Debug.WriteLine(imageURL);
                    ActivityStack.Children.Add(new Image { Source = imageURL, HorizontalOptions = LayoutOptions.Start });
                }
            }

        }
        // database code end

    }
}