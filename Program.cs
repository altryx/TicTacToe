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
            // This is where the logic for repeat-game will go 
            PlayGame();
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

            // Passing arrays as arguments. Could learn from Main() declaration, but the crucial part is that 
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
