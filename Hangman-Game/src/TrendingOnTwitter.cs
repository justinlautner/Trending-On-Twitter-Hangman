using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Gtk;

namespace HangmanGame
{
    public class TrendingOnTwitter
    {
        //U.S. WOEID: ("United States" - Country) |---- 2347572
        public static async Task<TwitterModel> LoadTrending()
        {
            string url = "";
            
            url = "https://api.twitter.com/1.1/trends/place.json?id=2347572";

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    TwitterModel twitter = await response.Content.ReadAsAsync<TwitterModel>();

                    return twitter;
                }
                else
                {
                    throw  new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}