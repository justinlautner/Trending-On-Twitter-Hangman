using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            
            if (File.Exists("Hangman-Game/Resources/twitter-words/twitter-hangman.txt"))
            {
                //Re-use the same list throughout the day, as Twitter API usage is rate limited (could block out user)
                if ((File.GetLastWriteTime("Hangman-Game/Resources/twitter-words/twitter-hangman.txt") - DateTime.Now).TotalDays < 1)
                {
                    saveWordsList = File.ReadAllLines("Hangman-Game/Resources/twitter-words/twitter-hangman.txt").ToList();
                    return;
                }
            }
            
            var client = new RestClient("https://api.twitter.com");
            var request = new RestRequest("1.1/trends/place.json?id=2430683", Method.GET);
            request.AddHeader("Authorization", "Bearer " + APIKeys.TwitterBearerToken);
            var response = client.Execute<List<MyArray>>(request);
            
            Console.WriteLine(response.ResponseStatus);

            //TODO: Replace console writing with GUI updating
            if (response.ContentLength == 0)
            {
                Console.WriteLine("NO DATA WAS RETURNED FROM TWITTER!");
            }
            else if (!response.IsSuccessful)
            {
                Console.Error.WriteLine("TWITTER'S RESPONSE WAS UNSUCCESSFUL!");
            }
            
            else
            {
                foreach (var dataPoint in response.Data)
                {
                    foreach (var trend in dataPoint.trends)
                    {
                        //TODO: Remove hashtags (#) and handle empty spaces
                        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                        string str = trend.name;
                        if (trend.name.Contains("#"))
                        {
                            trend.name.Remove(trend.name.IndexOf("#"));
                        }
                        str = String.Empty;
                        Console.WriteLine(trend.name);
                        saveWordsList.Add(trend.name);
                    }
                }
            }
            SaveTwitterTrends(saveWordsList);
        }
        
        private static void SaveTwitterTrends(List<string> saveWordList)
        {
            File.Delete("Hangman-Game/Resources/twitter-words/twitter-hangman.txt");
            File.WriteAllLines("Hangman-Game/Resources/twitter-words/twitter-hangman.txt", saveWordList);
        }
        
    }
}