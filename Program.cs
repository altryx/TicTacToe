using System;

namespace TicTacToe
{
    class Program
    {
        // Multidimensional array definition, https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
        public static char[,] emptyBoard = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        // public static char[] players = new char[] { 'X', 'O' }; Probably not needed
        public static int gameCount = 1; // Will be used to decide which player's turn is next
        public static char currentPlayer; //= (gameCount % 2 == 0) ? 'O' : 'X';

        // There has got to be a better way of creating this var - investigate
        // It would have been easier to use an array with single dimensions
        public static char[,] gameState = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        static void Main(string[] args)
        {
            bool continueGame = true; // Game continues until this is false
            
            // This is where the logic for repeat-game will go 
            do
            {
                // Continue game prompt boolean var, when false "play again" question will repeat. Needs to be in this context to reset on every repeat game
                bool validContinueGameResponse = false;

                // Change player on each run, start with X *** revise ***
                currentPlayer = (gameCount % 2 == 0) ? 'O' : 'X';
                
                // Clear the gameState variable - investigate better way to achieve this
                for (int i = 0; i < 3; i++) 
                {
                    for (int j = 0; j < 3; j++)
                    { gameState[i, j] = ' '; }
                    }

                PlayGame();

                // Play again prompt on repeat
                do
                {
                    Console.Write("Would you like to play again? (Y/N): ");
                    // It would have been better to use ReadKey(), then compare the key to the array of acceptable key responses, and have a generic message
                    // providing instruction(s) to the user. However, as one must allow user to potentially enter more than one key ("Yes" response in a scrnshot)
                    // I will use ReadLine() and handle the user input the long way

                    string playAgain = Console.ReadLine();


                    // **** Remove continue, replace with a if-elseif-else ****

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
                
                // Increase the game count to change the starting player for the next game
                gameCount++;
            } while (continueGame);

        }

        static void PlayGame()
        {
            
            bool gameOver = false;

            bool validPlayerMovement = false; // Test for valid response received for user input

            // Let's print the board for starters. This is where the logic check for game win will be
            // PrintGameBoard plus the procedure for entering new positions plus the logic check.
            // gameState variable should probably go here to avoid having to reset on new game.
            int moveCount = 1; // Used to change players between moves
            
            do
            {
                
                PrintGameBoard();

                do
                {
                    Console.Write($"Player {currentPlayer}, please enter a square number to place your token in: ");
                    string currentPlayerMove = Console.ReadLine();

                    // Function for checking if the position in the grid has been taken by a symbol (player)
                    // If taken, returns true. If not taken, returns false and sets the value at the appropriate position in the gameState array equal
                    // to the currentPlayer symbol. Consequence of the 3x3 array
                    bool checkSetPlayerPosition(int playerMove)
                    {
                        if (playerMove <= 3)
                        {
                            // Compensate for the 0-based index by subtracting 1
                            if (gameState[0, playerMove - 1] != ' ')
                            {
                                return (true);
                            }
                            else
                            {
                                gameState[0, playerMove - 1] = currentPlayer;
                                return (false);
                            }
                        }
                        else if (playerMove > 3 && playerMove <= 6)
                        {
                            // Rebase for the number of columns (subtract 3 from playerMove) and the 0-based index (subtract 1)
                            if (gameState[1, playerMove - 4] != ' ')
                            {
                                return (true);
                            }
                            else
                            {
                                gameState[1, playerMove - 4] = currentPlayer;
                                return (false);
                            }
                        }
                        else
                        {
                            // Rebase for the number of columns (subtract 6 because it's in row 3)
                            if (gameState[2, playerMove - 7] != ' ')
                            {
                                return (true);
                            }
                            else
                            {
                                gameState[2, playerMove - 7] = currentPlayer;
                                return (false);
                            }
                        }
                    }

                    // Simple way to detect if response is a number and return a boolean 
                    // https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netcore-3.1#System_Int32_TryParse_System_String_System_Int32__
                    if (Int32.TryParse(currentPlayerMove, out int intCurrentPlayerMove))
                    {
                        // Elegant, since result is produced by TryParse() above
                        if (intCurrentPlayerMove < 1 || intCurrentPlayerMove > 9)
                        {
                            // The number entered is not between 1 and 9 (inclusive), display error
                            Console.WriteLine("Error: That square number does not exist. Please try again.");
                        }
                        else if (checkSetPlayerPosition(intCurrentPlayerMove)) // See above for function that checks if position is taken
                        {
                            // The number chosen has already been taken
                            Console.WriteLine("Error: That square number is already occupied. Please try again.");
                            validPlayerMovement = false;
                        }
                        else
                        {
                            // Increase the move count to change the current player
                            moveCount++;
                            // Change the player for the next move
                            currentPlayer = (moveCount % 2 == 0) ? 'O' : 'X';
                            // Current move is over
                            validPlayerMovement = true;
                        }
                    }
                    else
                    {
                        // String entered was not a number
                        Console.WriteLine("Error: Input string was not in the correct format. Please try again.");
                    }

                } while (!validPlayerMovement);
                

                // Fake check if board is full
                if (moveCount == 10) { gameOver = true; }

            } while (!gameOver);

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
