// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System;
using System.Collections.Generic;

public class Trend    {
    public string name { get; set; } 
    public string url { get; set; } 
    public object promoted_content { get; set; } 
    public string query { get; set; } 
    public int? tweet_volume { get; set; } 
}

public class Location    {
    public string name { get; set; } 
    public int woeid { get; set; } 
}

public class MyArray    {
    public List<Trend> trends { get; set; } 
    public DateTime as_of { get; set; } 
    public DateTime created_at { get; set; } 
    public List<Location> locations { get; set; } 
}

public class Root    {
    public List<MyArray> MyArray { get; set; } 
}