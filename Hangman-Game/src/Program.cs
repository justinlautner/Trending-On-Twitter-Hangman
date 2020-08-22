using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Gtk;
using HangmanGame.TwitterAPI;
using Newtonsoft.Json;
using RestSharp;

namespace HangmanGame
{
    class MainClass
    {
        
        //Kansas City WOEID: 2430683
        
        public static void Main(string[] args)
        {
            //APIHelper.InitializeClient();
            //GET twitter trend data from API
            RunAsync().Wait();
            //new GameEngine();

            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
        
        static async Task RunAsync()
        {
            var client = new RestClient("https://api.twitter.com/1.1/trends/place.json?id=2430683");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + APIKeys.TwitterBearerToken);
            //request.AddHeader("Accept", "application/json");
            IRestResponse response = await client.ExecuteAsync(request);
            
            Console.WriteLine(response.ResponseStatus);
            //Console.WriteLine(response.ToString());

            if (response.ContentLength == 0)
            {
                Console.WriteLine("NO DATA!");
            }

            else
            {
                Console.WriteLine("Response length is: " + response.ContentLength);
                Console.WriteLine(JsonConvert.DeserializeObject(response.Content));

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);
                
                //Console.WriteLine(myDeserializedClass.MyArray[0].trends[0].name);
                
                if (response.IsSuccessful)
                {
                    Console.WriteLine("YO BITCH U DID IT");
                    Console.WriteLine(myDeserializedClass.MyArray[0].trends[0].name);
                }
                else
                {
                    Console.Error.WriteLine("DAWG WHO U THINK U FOOLIN");
                }
                //Console.WriteLine(response.ToString());
            }

            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.twitter.com/1.1/trends/place.json?id=2430683");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIKeys.TwitterBearerToken);
                
                HttpResponseMessage response = await client.GetAsync();

                if (response.IsSuccessStatusCode)
                {
                    //TwitterModel product = await response.Content.
                    TwitterModel model = await response.Content.ReadAsAsync<TwitterModel>();
                    Console.WriteLine("{0}\t${1}\t{2}", model.Trend);
                }
                else
                {
                    Console.WriteLine("NICE TRY MY DUDE BUT YOU SUCK");
                }
            }*/
        }
        
    }
}
