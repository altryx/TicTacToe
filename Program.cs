using System;

namespace TicTacToe
{
    class Program
    {
        // Declaring a multidimensional array with a fixed size
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
        static char[,] emptyBoard = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        static int gameCount = 1; // Will be used to decide which player's turn is next
        static char currentPlayer; //= (gameCount % 2 == 0) ? 'O' : 'X';

        // There has got to be a better way of creating this var - investigate
        // It would have been easier to use an array with single dimension instead though
        static char[,] gameState = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        static void Main(string[] args)
        {
            bool continueGame = true; // Game continues until this is false
            
            // Game on auto-repeat unless continueGame is false
            do
            {
                // Continue game prompt boolean var, when false "play again" question will repeat. Needs to be in this context to reset on every repeat game
                bool validContinueGameResponse = false;

                // Change player on each run, start with X 
                currentPlayer = (gameCount % 2 == 0) ? 'O' : 'X';
                
                // Clear the gameState variable when starting a new game
                gameState = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
            
                // Start a round of tic-tac-toe
                PlayGame();

                // Play again on auto-repeat - allows for prompting the player and repeating a game should the user choose so
                do
                {
                    Console.Write("Would you like to play again? (Y/N): ");
                    // It would have been better to use ReadKey(), then compare the key to the array of acceptable key responses, and have a generic message
                    // providing instruction(s) to the user. This takes care of (playAgain.Length > 1).However, as one must allow user to 
                    // potentially enter more than one key ("Yes" response in a scrnshot) for this exercise,
                    // ReadLine() must be used

                    string playAgain = Console.ReadLine();

                    // Checking the input (playAgain) and processing errors as required as well as the correct responses (y/n) 
                    
                    if (playAgain.Length > 1)       // First check if the user typed in a sentence out at random for the y/n prompt
                    {
                        Console.WriteLine("Error: String must be exactly one character long. Please try again.");
                    }
                    // Process a valid "no" response. 
                    else if (playAgain.ToLower() == "n") 
                    {
                        continueGame = false;
                        validContinueGameResponse = true;
                    }
                    // Process a valid "yes" response.
                    else if (playAgain.ToLower() == "y")
                    {
                        continueGame = true;
                        validContinueGameResponse = true;
                    }
                    else
                    // It's not longer than 1 in length, it's not a 'y' or 'n', so it has to be some other single char. This will also catch empty/blank input
                    {
                        Console.WriteLine("Error: Must be 'Y' or 'N'. Please try again.");
                    }
                    
                } while (!validContinueGameResponse);
                
                // Increase the game count to change the starting player for the next game
                gameCount++;
            } while (continueGame);

        }


        // Procedure that handles game mechanics (user inputs and associated errors)
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
                // This loop allows for the game to carry on until an end-state has been reached (one of the players wins, or the game is a draw)

                // Print the game board
                PrintGameBoard();

                // Handle the player input. This loop controls the prompts in case incorrect input has been provided. Contains a function/(method) for 
                // checking if a position has already been taken. Depends on the checkGameState to verify game state, i.e. (win/draw).

                do
                {
                    validPlayerMovement = false; // Resolution for Bug 1. Resets the validPlayerMovement var after a successful move

                    Console.Write($"\nPlayer {currentPlayer}, please enter a square number to place your token in: ");
                    string currentPlayerMove = Console.ReadLine();

                    // Function for checking if the position in the grid has been taken by a symbol (player)
                    // If taken, returns true. If not taken, returns false and sets the value at the appropriate position in the gameState array equal
                    // to the currentPlayer symbol. 
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
                        } //playerMove > 3 and <= 6
                        else if (playerMove <= 6)
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
                            // Rebase for the number of columns (subtract 6 because it's in row 3, subtract 1 to correct for index)
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
                        }
                        else
                        {
                            // Increase the move count to change the current player
                            moveCount++;
                            // Change the player for the next move
                            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                            // Check game state (are there any winners, is there a draw?)
                            gameOver = checkGameState();
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

            } while (!gameOver);

        } // End of PlayGame()
    
    // Procedure for generating an empty gameboard as well as the game status board. 

    static void PrintGameBoard()
        {
            // Create the numbered grid, with 2 blanks/spaces around the number/symbol. 
            // I wasn't happy with just manually entering the req'd number of dashes, but rather opted to use
            // the string constructor to allow for a bit of flexibility in terms of grid spacing. This may come in
            // handy should one want to change the spacing/sizing in the future
            // ref: https://stackoverflow.com/questions/3754582/is-there-an-easy-way-to-return-a-string-repeated-x-number-of-times

            string horizLine = new string('-', 13);     // 13 dashes
            string blankSpace = new string(' ', 1);     // Whitespace around the number, a single space. 
            char vertDelim = '|';                       // Symbol for vertical lines

            // Clear console
            // https://docs.microsoft.com/en-us/dotnet/api/system.console.clear?view=netcore-3.1

            Console.Clear();

            // Passing a character array as an argument to a procedure, idea being to recycle the code for drawing the board and substitute numbers 
            // or symbols using an array. Using a for loop which draws row per row.
            void genBoard(char[,] boardVar)
            {
                // Horizontal line above the grid
                Console.WriteLine(horizLine); 

                // Let's go row by row of the array
                for (int arrRow = 0; arrRow < 3; arrRow++)
                {
                    // Interpolated string. It's more concise. I like concise
                    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
                    Console.Write($"{vertDelim}");

                    // Column by column
                    for (int arrCol = 0; arrCol < 3; arrCol++)
                    {
                        // Allows for passing boardVar to Write, which can't be done using interpolated strings (see above for ref.)
                        Console.Write("{0}{1}{0}{2}", blankSpace, boardVar[arrRow, arrCol], vertDelim);
                    }

                    Console.WriteLine($"\n{horizLine}");
                }
            }

            // Generate blank board
            Console.WriteLine("Game Board Positions:");
            genBoard(emptyBoard);

            // Generate status board
            Console.WriteLine("\nGame Board Status:");
            genBoard(gameState);

        }

        // Function checkGameState looks at the rows, columns and two diagonals of the gameState array.
        // If a sequence of 3 consecutive 'X' or 'O' characters has been found, it prints the game board,
        // declares a winner and returns true. Return value is set to the gameOver variable which stops
        // the execution of the specific game currently in progress.

        static bool checkGameState()
        {

            // Check if game is a draw, the cheesy way
            int fullBoard = 0;
            foreach (char playerMove in gameState)
            {
                if (playerMove != ' ') { fullBoard++; }
            }

            if (fullBoard == 9)
            {
                PrintGameBoard();
                Console.WriteLine("The game was a draw.");
                return (true);
            }

            // Check gameState columns. The idea is to concat the values in across the row for each column, then compare to a 
            // string XXX or OOO. 
            for (int arrCol = 0; arrCol < 3; arrCol++)
            {
                string colCheck = "";
                for (int arrRow = 0; arrRow < 3; arrRow++)
                {
                    colCheck = colCheck + gameState[arrRow, arrCol].ToString();
                }

                switch (colCheck)
                {
                    case "XXX":
                        PrintGameBoard();
                        Console.WriteLine("Player X was the winner!");
                        return (true);
                    case "OOO":
                        PrintGameBoard();
                        Console.WriteLine("Player O was the winner!");
                        return (true);
                    default:
                        break;
                }
            }

            // Check gameState rows
            for (int arrRow = 0; arrRow < 3; arrRow++)
            {
                string rowCheck = "";
                for (int arrCol = 0; arrCol < 3; arrCol++)
                {
                    rowCheck = rowCheck + gameState[arrRow, arrCol].ToString();
                }

                switch (rowCheck)
                {
                    case "XXX":
                        PrintGameBoard();
                        Console.WriteLine("Player X was the winner!");
                        return (true);
                    case "OOO":
                        PrintGameBoard();
                        Console.WriteLine("Player O was the winner!");
                        return (true);
                    default:
                        break;
                }
            }

            // Check gameState diagonals. I don't like having too many for loops, so two diagonals are determined
            // using a single loop. 
            string diagonalCheck1 = "", diagonalCheck2 = "";
            for (int arrRow = 0; arrRow < 3; arrRow++)
            {
                diagonalCheck1 = diagonalCheck1 + gameState[arrRow, arrRow].ToString();
                diagonalCheck2 = diagonalCheck2 + gameState[arrRow, 2 - arrRow].ToString();
            }

            // I don't like repeating chunks of code unless necessary, so here's a foreach loop to determine if either of 
            // the two variables with diagonals (diagonalCheck1 and diagonalCheck2) contains a winning combination. 
            // I could do this since I did not have a loop within a loop like for column/row checks above

            // Note: This is also a bug fix for Bug 2: Initial version only checked one diagonal for a winner.

            // Reference https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/using-foreach-with-arrays

            foreach (string selDiag in new string[] { diagonalCheck1, diagonalCheck2 } )
            {
                switch (selDiag)
                {
                    case "XXX":
                        PrintGameBoard();
                        Console.WriteLine("Player X was the winner!");
                        return (true);
                    case "OOO":
                        PrintGameBoard();
                        Console.WriteLine("Player O was the winner!");
                        return (true);
                    default:
                        break;
                }
            }
            // Covers fail-safe scenario
            return (false);
        }

    } // End of Program

    
}
