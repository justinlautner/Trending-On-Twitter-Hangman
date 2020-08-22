using System.Net.Http;
using System.Net.Http.Headers;

namespace HangmanGame
{
    public class APIHelper
    {
        public static HttpClient ApiClient { get; set; }

        
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}