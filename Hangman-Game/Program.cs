using System;
using Gtk;

namespace HangmanGame
{
    class MainClass
    {
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
