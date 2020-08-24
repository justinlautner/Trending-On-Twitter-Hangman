using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;

namespace HangmanGame.TwitterAPI
{
    public class GetTwitterTrends
    {
        
        private List<string> saveWordsList = new List<string>();

        public List<string> GetWords()
        {
            return saveWordsList;
        }
        
        public GetTwitterTrends()
        {
            
            /*if (File.Exists("Resources/twitter-words/twitter-hangman.txt"))
            {
                //Re-use the same list throughout the day, as Twitter API usage is rate limited (could block out user)
                 //This is unnecessary for now, current rate limit is 75/15min
                if ((File.GetLastWriteTime("Resources/twitter-words/twitter-hangman.txt") - DateTime.Now).TotalDays < 1)
                {
                    
                }
                saveWordsList = File.ReadAllLines("Resources/twitter-words/twitter-hangman.txt").ToList();
                return;
            }*/
            
            var client = new RestClient("https://api.twitter.com");
            var request = new RestRequest("1.1/trends/place.json?id=2430683", Method.GET);
            request.AddHeader("Authorization", "Bearer " + APIKeys.TwitterBearerToken);
            var response = client.Execute<List<MyArray>>(request);
            
            Console.WriteLine(response.ResponseStatus);

            //TODO: Replace console writing with GUI updating
            //TODO: File saving not functional
            if (response.ContentLength == 0)
            {
                Console.WriteLine("NO DATA WAS RETURNED FROM TWITTER!" + "/n" + "RESORTING TO SAVE OR RANDOM WORD LIST!");
                if (File.Exists("Resources/twitter-words/twitter-hangman.txt"))
                {
                    saveWordsList = File.ReadAllLines("Resources/twitter-words/twitter-hangman.txt").ToList();
                    return;
                }
                LoadRandomWordList();
            }
            else if (!response.IsSuccessful)
            {
                Console.Error.WriteLine("TWITTER'S RESPONSE WAS UNSUCCESSFUL!" + "/n" + "RESORTING TO SAVE OR RANDOM WORD LIST!");
                if (File.Exists("Resources/twitter-words/twitter-hangman.txt"))
                {
                    saveWordsList = File.ReadAllLines("Resources/twitter-words/twitter-hangman.txt").ToList();
                    return;
                }
                LoadRandomWordList();
            }
            
            else
            {
                foreach (var dataPoint in response.Data)
                {
                    foreach (var trend in dataPoint.trends)
                    {
                        string str = trend.name;
                        if (trend.name.Contains("#"))
                        {
                            string strNew = Regex.Replace(str, "#", "");
                            Console.WriteLine(strNew);
                            saveWordsList.Add(strNew);
                        }
                        else
                        {
                            Console.WriteLine(str);
                            saveWordsList.Add(str);
                        }
                    }
                }
            }
            SaveTwitterTrends(saveWordsList);
        }
        
        private static void SaveTwitterTrends(List<string> saveWordList)
        {
            if (File.Exists("Resources/twitter-words/twitter-hangman.txt"))
            {
                //Remove outdated trending list if applicable
                File.Delete("Resources/twitter-words/twitter-hangman.txt");
            }
            Console.WriteLine("is this happening?");
            File.WriteAllLines("Resources/twitter-words/twitter-hangman.txt", saveWordList);
        }

        private void LoadRandomWordList()
        {
            if (File.Exists("Resources/test-words/hangman.txt"))
            {
                var file = File.ReadAllLines("Resources/test-words/hangman.txt", Encoding.UTF8);
                saveWordsList = file.ToList();
            }
            else
            {
                Console.Write("The file \"hangman.txt\" is missing. Aborting...");
                Environment.Exit(1);
            }
        }
        
    }
}