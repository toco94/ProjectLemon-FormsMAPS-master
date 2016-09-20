using ProjectLemon.ViewModels;
using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net.Http;
using ProjectLemon.Models;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ProjectLemon.Views
{
    public partial class FacebookProfilePage : ContentPage
    {

        private string ClientId = "165942640479284";
        private string accessToken = "";

        public FacebookProfilePage()
        {
            InitializeComponent();


            var apiRequest =
            "https://www.facebook.com/dialog/oauth?client_id="
            + ClientId
            + "&display=popup&scope=user_friends&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;


        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                Content = MainStackLayout;

                var requestUrl =
                "https://graph.facebook.com/v2.7/me/friends?access_token="
                + accessToken;
                
                var httpClient = new HttpClient();
                
                var friendsJson = await httpClient.GetStringAsync(requestUrl);
                
                //var friends = JsonConvert.DeserializeObject<RootObject>(friendsJson);
                //
                //
                JObject friendsJ = JObject.Parse(friendsJson);
                //
                string id = "";
                string name = "";
                //
                ////Loop through the returned friends
                foreach (var i in friendsJ["data"].Children())
                {
                    id = i["id"].ToString().Replace("\"", "");
                    name = i["name"].ToString().Replace("\"", "");
                    Friendslbl.Text = Friendslbl.Text + "\n" + name;
                    //+ "<img src=" + "https://graph.facebook.com/" + id + "/picture>"
                }
            }
        }


        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Xamarin.Forms.Device.OS == TargetPlatform.WinPhone || Xamarin.Forms.Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }

        private void FacebookLogout_Clicked(object sender, EventArgs e)
        {
            accessToken = "";
        }
    }
}
