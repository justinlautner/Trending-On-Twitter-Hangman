using Gtk;
using HangmanGame.TwitterAPI;

namespace HangmanGame
{
    class MainClass
    {
        
        //Kansas City WOEID: 2430683
        
        public static void Main(string[] args)
        {
            //GET twitter trend data from API
            GetTwitterTrends trending = new GetTwitterTrends();
            new GameEngine(trending.GetWords());

            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }

        //Save to implement HTTPClient version (for funsies)
        /*static async Task RunAsync()
        {
            

            using (var client = new HttpClient())
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
            }
        }*/
        
    }
}
