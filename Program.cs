using System;

namespace TicTacToe
{
    class Program
    {
        // Multidimensional array definition, https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
        public static char[,] emptyBoard = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };

        // There has got to be a better way of creating this var - investigate
        public static char[,] gameState = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        static void Main(string[] args)
        {
            bool continueGame = true; // Game continues until this is false
            // This is where the logic for repeat-game will go 
            do
            {
                // Continue game prompt boolean var, when false "play again" question will repeat. Needs to be in this context to reset on every repeat game
                bool validContinueGameResponse = false; 
                PlayGame();

                // Play again prompt on repeat
                do
                {
                    Console.Write("Would you like to play again? (Y/N): ");
                    // It would have been better to use ReadKey(), then compare the key to the array of acceptable key responses, and have a generic message
                    // providing instruction(s) to the user. However, as one must allow user to potentially enter more than one key ("Yes" response in a scrnshot)
                    // I will use ReadLine() and handle the user input the long way

                    string playAgain = Console.ReadLine();

                    // Process a valid "no" response.
                    if (playAgain.ToLower() == "n") 
                    { 
                        continueGame = false; 
                        validContinueGameResponse = true; 
                        continue; // Skip the rest of the evaluation in the do/while loop 
                    }
                    
                    // Process a valid "yes" response.
                    if (playAgain.ToLower() == "y")
                    {
                        continueGame = true;
                        validContinueGameResponse = true;
                        continue;
                    }

                    if (playAgain.Length > 1)
                    {
                        Console.WriteLine("Error: String must be exactly one character long. Please try again.");
                    }
                    // Save time by checking if the value of the variable playAgain is in an array of acceptable single letter responses. Also caters for
                    // case insensitivity by converting the response to lowercase by default and then comparing. 
                    // Read https://stackoverflow.com/questions/13257458/check-if-a-value-is-in-an-array-c 
                    // Tried using { }.method(), then I read https://stackoverflow.com/questions/30509177/cant-use-an-inline-array-in-c
                    if (Array.IndexOf((new[] { "y", "n" }), playAgain.ToLower()) == -1 && playAgain.Length == 1)
                    {
                        Console.WriteLine("Error: Must be 'Y' or 'N'. Please try again.");
                    }
                } while (!validContinueGameResponse);
            } while (continueGame);

        }

        static void PlayGame()
        {
            // Let's print the board for starters. This is where the logic check for game win will be
            // PrintGameBoard plus the procedure for entering new positions plus the logic check.
            // gameState variable should probably go here to avoid having to reset on new game.
            PrintGameBoard();
        }

        static void PrintGameBoard()
        {
            // Create the numbered grid, with 2 blanks/spaces around the number/symbol. 
            // I wasn't happy with just manually entering the req'd number of dashes, but rather opted to use
            // the string constructor to allow for a bit of flexibility in terms of grid spacing.
            // ref: https://stackoverflow.com/questions/3754582/is-there-an-easy-way-to-return-a-string-repeated-x-number-of-times

            string horizLine = new string('-', 13);
            string blankSpace = new string(' ', 1);     // Whitespace around the number
            char vertDelim = '|';

            // Clear console
            // https://docs.microsoft.com/en-us/dotnet/api/system.console.clear?view=netcore-3.1

            Console.Clear();

            // Passing arrays as arguments. Could learn from Main() declaration
            void genBoard(char[,] boardVar)
               {
                   Console.WriteLine(horizLine);
                   for (int arrRow = 0; arrRow < 3; arrRow++)
                   {
                       Console.Write($"{vertDelim}");
                       for (int arrCol = 0; arrCol < 3; arrCol++)
                       {
                           // Allows for passing boardVar to Write, as opposed to using interpolated strings (see below for ref.)
                           Console.Write("{0}{1}{0}{2}", blankSpace, boardVar[arrRow, arrCol], vertDelim);
                       }
                       // Interpolated string.
                       // INSERT REFERENCE HERE
                    Console.WriteLine($"\n{horizLine}");
                }
               }

            // Blank board
            Console.WriteLine("Game Board Positions:");
            genBoard(emptyBoard);
            
            // Status board
            Console.WriteLine("\nGame Board Status:");
            genBoard(gameState);
            
        }
    }
}
