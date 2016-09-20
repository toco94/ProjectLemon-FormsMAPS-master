using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProjectLemon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectLemon.Services
{
    public class FacebookServices
    {

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,bio,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            //var requrl =
              //  "https://graph.facebook.com/v2.7/me/?fields=friends&access_token="
              //  + accessToken;

            //var friendsJson = await httpClient.GetStringAsync(requrl);

            //var friendsList = JsonConvert.DeserializeObject<FriendsModel>(friendsJson);

            return facebookProfile;
        }
    }
}
