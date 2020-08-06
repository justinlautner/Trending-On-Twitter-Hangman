using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    public class GameEngine
    {


        private static List<string> hangman = new List<string>();
        private static int hangCount = 0;
        private static List<Char> guesses = new List<Char>();
        private static StringBuilder correctGuesses = new StringBuilder();
        private static List<string> wordsUsed = new List<string>();
        private static string chosenWord;
        private static string blurredWord;
        private static int wins;
        private static int losses;
        //private static Scanner input = new Scanner(System.in);

        static String GetBlurredWord()
        {
            return blurredWord;
        }

        static String GetGuessesRemaining()
        {
            return "You have " + Math.Abs(hangCount - 6) + " incorrect guess(es) remaining before you lose...";
        }

        static int GetHangCount()
        {
            return hangCount;
        }

        static String GetGameResults()
        {
            string feedback;
            feedback = "You have " + wins + " wins" + " and " + losses + " losses.";
            return feedback + "\n" + "The words used so far have been: " + wordsUsed + "\n" + "Enter 'play' or 'exit'";
        }

        GameEngine()
        {
            LoadFile();

            chosenWord = WordSelection();
            blurredWord = chosenWord.Replace("[^ " + correctGuesses + "]", "*");
        }

        private static String WordSelection()
        {
            //Choose a hangman word at random from list
            Random rnd = new Random();
            int i = rnd.Next(0, hangman.Count);
            string word = hangman[i];
            wordsUsed.Add(word);
            return word;
        }

        private static void LoadFile()
        {
            //load array of hangman words to choose from
            if (File.Exists("src/assets/hangman.txt"))
            {
                string[] file = File.ReadAllLines(@"src/assets/hangman.txt", Encoding.UTF8);
            }
            else
            {
                Console.Write("The file \"hangman.txt\" is missing. Aborting...");
                Environment.Exit(1);
            }
        }

        static String GuessAction(char guess)
        {
            //Create blurred word to show to user
            //blurredWord = chosenWord.ReplaceAll("[^ " + correctGuesses + "]", "*");
            blurredWord = chosenWord.Replace("[^ " + correctGuesses + "]", "*");
            //Input error catching
            if (char.IsDigit(guess)){
                return "Please enter an alphabetic letter. ";
            }
            if (guesses.Contains(guess))
            {
                return "You have already guessed '" + guess + "'... Try another...";
            }
            guesses.Add(guess);
            //If your guess is correct, make sure you cannot guess it again
            if (chosenWord.Contains("" + guess) && !chosenWord.Equals(blurredWord, StringComparison.CurrentCultureIgnoreCase))
            {
                correctGuesses.Append(guess);
                blurredWord = chosenWord.Replace("[^ " + correctGuesses + "]", "*");
                if (!chosenWord.Equals(blurredWord, StringComparison.CurrentCultureIgnoreCase))
                {
                    return "'" + guess + "' is in the word. You have guessed correctly!";
                }
            }
            //Your guess is incorrect, end game or lessen guesses
            else
            {
                hangCount++;
                if (hangCount == 6)
                {
                    losses++;
                    return "You have been hung! May you rest in peace..."
                        + "\n" + "Would you like to play again?";
                }
                return "Sorry, '" + guess + "' is not in the word...";
            }
            //If you guess the word in its entirety, win sequence
            if (chosenWord.Equals(blurredWord, StringComparison.CurrentCultureIgnoreCase))
            {
                // GAME OVER
                wins++;
                return "You have successfully avoided death! Yay surviving!"
                    + "\n" + "You had " + hangCount + " miss(es)."
                    + "\n" + "Would you like to play again?";
            }
            return "Awaiting your attempt...";
        }

        static void GameOver(String playAgain)
        {
            //If user chose to exit, do so here
            if (playAgain.Contains("exit"))
            {
                Environment.Exit(0);
            }
            //User has optioned to continue playing, add word used to bank and clear all variables for a replay
            hangCount = 0;
            guesses.Clear();
            correctGuesses.Clear();
            chosenWord = WordSelection();
            blurredWord = chosenWord.Replace("[^ " + correctGuesses + "]", "*");
        }

    }
}
