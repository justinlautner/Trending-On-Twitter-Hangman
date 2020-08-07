using System;
using Gtk;
using HangmanGame;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        wordText.Text = GameEngine.GetBlurredWord();
        hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Empty.png");
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnEnter(object sender, EventArgs e)
    {
        string rawInput = inputText.Text;
        //This if-else ensures that if the game is over, the program catches 'play' or 'exit' input instead of a letter
        if (gameOverText.Visible)
        {
            GameEngine.GameOver(rawInput);
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
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_One.png");
                break;
            case 2:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Two.png");
                break;
            case 3:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Three.png");
                break;
            case 4:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Four.png");
                break;
            case 5:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Five.png");
                break;
            case 6:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Six.png");
                break;
            default:
                hangmanImage.Pixbuf = new Gdk.Pixbuf("Resources/images/Hangman_Empty.png");
                break;
        }
        //Prevents having to click the text box after every input
        inputText.Text = "";
        inputText.IsFocus = true;
    }

    protected void OnQuit(object sender, EventArgs e)
    {
        Application.Quit();
    }
}
