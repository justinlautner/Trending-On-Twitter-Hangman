using System;
using System.Net.Http;
using Gtk;

namespace HangmanGame
{
    class MainClass
    {
        
        HttpClient client = new HttpClient();
            
        public static void Main(string[] args)
        {
            new GameEngine();

            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
