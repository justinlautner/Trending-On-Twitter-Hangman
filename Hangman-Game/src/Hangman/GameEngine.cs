using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangmanGame
{
    public class GameEngine
    {

        private static List<string> hangman;
        private static int hangCount;
        private static List<Char> guesses = new List<Char>();
        private static StringBuilder correctGuesses = new StringBuilder();
        private static List<string> wordsUsed = new List<string>();
        private static string chosenWord;
        private static StringBuilder blurredWord = new StringBuilder();
        private static int wins;
        private static int losses;
        private static bool winorloss;

        public static String GetBlurredWord()
        {
            return blurredWord.ToString();
        }

        public static String GetGuessesRemaining()
        {
            return "You have " + Math.Abs(hangCount - 6) + " incorrect guess(es) remaining before you lose...";
        }

        public static int GetHangCount()
        {
            return hangCount;
        }

        public static bool GetWinOrLoss()
        {
            return winorloss;
        }

        public static String GetGameResults()
        {
            string feedback;
            feedback = "You have " + wins + " wins" + " and " + losses + " losses.";
            StringBuilder words = new StringBuilder();
            foreach(string s in wordsUsed)
            {
                if (words.Length != 0)
                {
                    words.Append(", " + s);
                }
                else
                {
                    words.Append(s);
                }
            }
            return feedback + "\n" + "The words used so far have been: " + words;
        }

        public GameEngine(List<string> saveWordList)
        {

            hangman = saveWordList;
            
            chosenWord = WordSelection();
            //Replaces every character that hasn't already been guessed with an '*'
            blurredWord.Clear();
            foreach (char temp in chosenWord)
            {
                if (Char.IsWhiteSpace(temp))
                {
                    blurredWord.Append(" ");
                }
                else if (!correctGuesses.ToString().Contains(temp.ToString().ToLower()))
                {
                    blurredWord.Append("*");
                }
                else
                {
                    blurredWord.Append(temp);
                }
            }
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

        public static String GuessAction(char guess)
        {
            //Create blurred word to show to user
            //Replaces every character that hasn't already been guessed with an '*'
            blurredWord.Clear();
            foreach (char temp in chosenWord)
            {
                if (Char.IsWhiteSpace(temp))
                {
                    blurredWord.Append(" ");
                }
                else if (!correctGuesses.ToString().Contains(temp.ToString().ToLower()))
                {
                    blurredWord.Append("*");
                }
                else
                {
                    blurredWord.Append(temp);
                }
            }
            //Input error catching
            /*if (char.IsDigit(guess)){
                return "Please enter an alphabetic letter. ";
            }*/
            if (guesses.Contains(guess))
            {
                return "You have already guessed '" + guess + "'... Try another...";
            }
            if (guess.Equals(""))
            {
                return "Please enter a character into the field.";
            }
            guesses.Add(guess);
            //If your guess is correct, make sure you cannot guess it again
            if (chosenWord.ToLower().Contains(("" + guess).ToLower()) && !chosenWord.Equals(blurredWord.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                correctGuesses.Append(guess);
                //Replaces every character that hasn't already been guessed with an '*'
                blurredWord.Clear();
                foreach (char temp in chosenWord)
                {
                    if (Char.IsWhiteSpace(temp))
                    {
                        blurredWord.Append(" ");
                    }
                    else if (!correctGuesses.ToString().ToLower().Contains(temp.ToString().ToLower()))
                    {
                        blurredWord.Append("*");
                    }
                    else
                    {
                        blurredWord.Append(temp);
                    }
                }
                if (!chosenWord.Equals(blurredWord.ToString(), StringComparison.CurrentCultureIgnoreCase))
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
                    winorloss = false;
                    return "You have been hung! May you rest in peace..."
                        + "\n" + "Would you like to play again?";
                }
                return "Sorry, '" + guess + "' is not in the word...";
            }
            //If you guess the word in its entirety, win sequence
            if (chosenWord.Equals(blurredWord.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                // GAME OVER
                wins++;
                winorloss = true;
                return "You have successfully avoided death! Yay surviving!"
                    + "\n" + "You had " + hangCount + " miss(es)."
                    + "\n" + "Would you like to play again?";
            }
            return "Awaiting your attempt... The word could contain digits, letters or some characters (hashtags are omitted)";
        }

        public static void GameOver()
        {
            //User has optioned to continue playing, add word used to bank and clear all variables for a replay
            hangCount = 0;
            guesses.Clear();
            correctGuesses.Clear();
            chosenWord = WordSelection();
            //Replaces every character that hasn't already been guessed with an '*'
            blurredWord.Clear();
            foreach (char temp in chosenWord)
            {
                if (Char.IsWhiteSpace(temp))
                {
                    blurredWord.Append(" ");
                }
                else if (!correctGuesses.ToString().ToLower().Contains(temp.ToString().ToLower()))
                {
                    blurredWord.Append("*");
                }
                else
                {
                    blurredWord.Append(temp);
                }
            }
        }

    }
}
