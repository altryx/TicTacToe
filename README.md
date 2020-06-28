# Tic Tac Toe
TECHCareers Assignment 2 - Simple TicTacToe Console Game

This is my second C# program - a simple Tic Tac Toe game. I will not be accepting any pull requests.

## Assignment
* The flow of the game must match the traditional flow of the game.

* An AI to play against the player is not a requirement of the base assignment.

* The game must end and display the game state (the winner, or that the game is a draw) when the end conditions are met.

* The console must clear between each player’s turn in order to ensure only the most up-to-date board is shown.

* The current game board (Game Board Status in the above screenshot) must be shown each turn, so the player knows which squares are taken, and can play strategically.

* A list of square numbers (Game Board Positions) must be shown each turn, for player to use when selecting a square in which to place their token.

* The prompt for a square number must indicate the current player.

* If the player makes an invalid square selection, a descriptive error message must be displayed, and the player must be prompted again (until a valid selection is made).

* The game board data must be stored in a two-dimensional array of characters.

* At the end of the game, the user(s) must be asked if they would like to play again and must select between ‘Y’ and ‘N’ (case insensitive). In the event of an invalid selection, they should be prompted again until a valid selection is made.

* The program must contain the Main() method, a PlayGame() method, and a PrintGameBoard() method at a minimum. You are free to use additional methods as needed, but ensure they are named semantically and contain comment docstrings.

* The program must be maintainable as well as functional. Another programmer should be able to look at your code/comments and understand what each line does without requiring extensive testing.

* The program must never crash due to an unhandled exception.

## Build requirements and instructions

.NET Core 3.1 or newer. 

Clone the repo or download a copy. Execute `dotnet run` in the local repo directory.

## Screenshots

Game after startup
![image](https://user-images.githubusercontent.com/60409723/85960699-d59d2280-b962-11ea-8513-6b191f79934d.png)

Game after a win
![image](https://user-images.githubusercontent.com/60409723/85960676-9bcc1c00-b962-11ea-88da-5c93749aae47.png)

Game after a draw
![image](https://user-images.githubusercontent.com/60409723/85960716-f9606880-b962-11ea-8dda-8a4d3ec9d2ea.png)

Game prompts during incorrect user input on each move
![image](https://user-images.githubusercontent.com/60409723/85960750-2280f900-b963-11ea-96b5-ab50c991acf1.png)


## Trello project board
Access the Trello board [here](https://trello.com/b/WpSL1ljr/c-tic-tac-toe)