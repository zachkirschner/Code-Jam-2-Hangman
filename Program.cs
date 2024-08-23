using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static readonly ConsoleColor[] HangmanColors =
    {

        //Defining the potential colors that can be used
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Blue,
        ConsoleColor.Yellow,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta
    };

    static void Main()
    {
        // Array of possible words

        string[] words = ["tundra", "camry", "yaris", "prius", "corolla", "sienna", "supra", "crown", "highlander", "sequoia", "venza"];
        var random = new Random();
        string wordToGuess = words[random.Next(words.Length)];
        var guessedLetters = new HashSet<char>();
        int incorrectGuesses = 0;
        const int maxIncorrectGuesses = 5; // Maximum amount of tries before the Game Over sequence if the user hasn't already guessed the full word

        while (true)
        {
            Console.Clear();
            DisplayHangman(incorrectGuesses);
            DisplayWord(wordToGuess, guessedLetters);

            Console.Write("\nGuess a letter: ");
            var input = Console.ReadLine()?.ToLower();

            if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid input. Please enter a single letter.");
                Console.ResetColor();
                Console.ReadKey();
                continue;
            }

            char guessedLetter = input[0];
            if (guessedLetters.Contains(guessedLetter))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You've already guessed that letter.");
                Console.ResetColor();
                Console.ReadKey();
                continue;
            }

            guessedLetters.Add(guessedLetter);

            //This adds new body parts to the hangman with each incorrect guess
            if (wordToGuess.Contains(guessedLetter))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Good guess!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong guess!");
                Console.ResetColor();
                incorrectGuesses++;
            }

            if (HasWon(wordToGuess, guessedLetters))
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You've guessed the word!");
                Console.ResetColor();
                break;
            }

            if (incorrectGuesses >= maxIncorrectGuesses)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Game over! The word was '{wordToGuess}'.");
                Console.ResetColor();
                break;
            }
        }
    }

    static void DisplayHangman(int incorrectGuesses)
    {
        // Define colors for each part of the hangman figure
        var colors = new[]
        {
            ConsoleColor.Gray, // 0 Base holding the Hangman
            ConsoleColor.Red,  // 1 Head
            ConsoleColor.Green, // 2 Body
            ConsoleColor.Blue, // 3 Left arm
            ConsoleColor.Yellow, // 4 Right arm
            ConsoleColor.Cyan, // 5 Left leg
            ConsoleColor.Magenta // 6 Right leg
        };


        Console.WriteLine("Hangman: Toyota Edition!");
        Console.WriteLine("  ______");
        Console.WriteLine(" /|_||_\\`.___");
        Console.WriteLine("(   _TOYOTA_ \\");
        Console.WriteLine("=`-(_)----(_)-'");


        // Head
        Console.ForegroundColor = colors[1];
        if (incorrectGuesses >= 1) Console.WriteLine("  O   |");
        else Console.WriteLine("      |");

        Console.ResetColor();

        // Body
        Console.ForegroundColor = colors[2];
        if (incorrectGuesses >= 2) Console.WriteLine("  |   |");
        else if (incorrectGuesses == 1) Console.WriteLine("      |");
        else Console.WriteLine("      |");

        Console.ResetColor();

        // Arms
        Console.ForegroundColor = colors[3];
        if (incorrectGuesses >= 3) Console.WriteLine(" /|\\  |");
        else if (incorrectGuesses == 2) Console.WriteLine("  |   |");
        else Console.WriteLine("      |");

        Console.ResetColor();

        // Legs
        Console.ForegroundColor = colors[4];
        if (incorrectGuesses >= 4) Console.WriteLine(" / \\  |");
        else if (incorrectGuesses == 3) Console.WriteLine("      |");
        else Console.WriteLine("      |");

        Console.ResetColor();

        // Bottom of the Hangman
        Console.ForegroundColor = colors[5];
        Console.WriteLine("      |");
        Console.WriteLine("=========");
        Console.ResetColor();
    }

    static void DisplayWord(string wordToGuess, HashSet<char> guessedLetters)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (char letter in wordToGuess)
        {
            if (guessedLetters.Contains(letter))
                Console.Write(letter + " ");
            else
                Console.Write("_ ");
        }
        Console.ResetColor();
    }

    static bool HasWon(string wordToGuess, HashSet<char> guessedLetters)
    {
        return wordToGuess.All(letter => guessedLetters.Contains(letter));
    }
}
