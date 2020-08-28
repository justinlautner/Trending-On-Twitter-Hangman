using System;
using Gtk;
using HangmanGame;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        wordText.Text = GameEngine.GetBlurredWord();
        hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Empty.png", 400, 400);
    }

    private void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    private void OnEnter(object sender, EventArgs e)
    {
        string rawInput = inputText.Text;
        //This if-else ensures that if the game is over, the program responds accordingly
        if (gameOverText.Visible)
        {
            GameEngine.GameOver();
            wordText.Text = GameEngine.GetBlurredWord();
            guessesRemaining.Text = GameEngine.GetGuessesRemaining();
            gameOverText.Visible = false;
            quitButton.Visible = false;
            guessesRemaining.Visible = true;
            resultText.Buffer.Text = ("Awaiting your attempt...");
        }
        //Resume normal function here... Input char and update GUI accordingly
        else
        {
            if (String.IsNullOrEmpty(rawInput))
            {
                return;
            }
            char input = rawInput[0];
            resultText.Buffer.Text = GameEngine.GuessAction(input);
            wordText.Text = GameEngine.GetBlurredWord();
            guessesRemaining.Text = GameEngine.GetGuessesRemaining();
            if (!wordText.Text.Contains("*") || guessesRemaining.Text.Contains("0"))
            {
                resultText.Buffer.Text = GameEngine.GetGameResults() + "\n" + "Would you like to continue playing?" + "\n" + "Click OK to continue or Quit to dishonor your family...";
                gameOverText.Visible = true;
                quitButton.Visible = true;
                guessesRemaining.Visible = false;
                if (!GameEngine.GetWinOrLoss())
                {
                    gameOverText.Text = "You stink... LOSER!";
                }
                else
                {
                    gameOverText.Text = "Wait... You actually managed to do win something?";
                }
            }
        }
        //Change ImageView based on amount of incorrect answers
        int hangCount = GameEngine.GetHangCount();
        switch (hangCount)
        {
            case 1:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_One.png", 400, 400);
                break;
            case 2:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Two.png", 400, 400);
                break;
            case 3:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Three.png", 400, 400);
                break;
            case 4:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Four.png", 400, 400);
                break;
            case 5:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Five.png", 400, 400);
                break;
            case 6:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Six.png", 400, 400);
                break;
            default:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Empty.png", 400, 400);
                break;
        }
        //Prevents having to click the text box after every input
        inputText.Text = "";
        inputText.IsFocus = true;
    }

    private void OnQuit(object sender, EventArgs e)
    {
        Application.Quit();
    }

    public void changeText(string s)
    {
        resultText.Buffer.Text = s;
    }
}
