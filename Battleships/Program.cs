using Battleships;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var hostBoard = new GameBoard { } ; hostBoard.IsHost = true ;
var guestBoard = new GameBoard { } ;
GameBoard currentBoard = hostBoard;
GameBoard opponentBoard = guestBoard;


GameState.GameStates currentState = GameState.GameStates.PLACEMENT;
while (true)
{
    if (currentState == GameState.GameStates.PLACEMENT)
    {
        currentBoard.PrintBoard();
        Console.Write($"Place one of your remaining {currentBoard.remainingShips} remaining ships: ");
        if (currentBoard.remainingShips > 0)
        {
            currentBoard.PlaceShip();
        }
        else if (currentBoard == guestBoard)
        {
            currentBoard = hostBoard;
            opponentBoard = guestBoard;
            currentState = GameState.GameStates.SHOOTING;
        }
        else
        {
            currentBoard = guestBoard;
            opponentBoard = hostBoard;
            Console.Clear();
            Console.WriteLine("Player 1 has now placed all of their ships.");
            Console.WriteLine("\nPlayer 2 may now press any key to start placement.");
            Console.ReadKey();
        }
    }
    else if (currentState == GameState.GameStates.SHOOTING)
    {
        if (currentBoard == hostBoard)
        {
            Console.Clear();
            Console.WriteLine("Player 1 may now press any key to start their attack.");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Player 2 may now press any key to start their attack.");
            Console.ReadKey();
        }
        opponentBoard.PrintBoard(true);
        Console.Write($"Choose where to shoot your shot: ");
        if (opponentBoard.ShootShip())
        {
            opponentBoard.PrintBoard(true);
            if (opponentBoard.CheckGameOver())
            {
                currentState = GameState.GameStates.GAMEOVER;
            }
            else
            {
                Console.WriteLine("Nicely done! You sunk a ship.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Better luck next time :(");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        if (currentState != GameState.GameStates.GAMEOVER)
        {
            GameBoard x = currentBoard;
            currentBoard = opponentBoard;
            opponentBoard = x;
        }
    }
    else if (currentState == GameState.GameStates.GAMEOVER)
    {
        Console.Clear();
        if (currentBoard == hostBoard)
        {
            Console.WriteLine("Both of you played well. But Player 1 played best!");
        }
        else
        {
            Console.WriteLine("Both of you played well. But Player 2 played best!");
        }
        Console.WriteLine();
        Console.WriteLine("Type (R) to restart. Type (Q) to quit.");
        while (true)
        {
            string input = Console.ReadLine() ?? "Null!";
            input = input.ToUpper();
            if (input == "Q")
            {
                currentState = GameState.GameStates.QUIT;
                break;
            }
            else if (input == "R")
            {
                hostBoard = new GameBoard { }; hostBoard.IsHost = true;
                guestBoard = new GameBoard { };

                currentBoard = hostBoard;
                opponentBoard = guestBoard;

                currentState = GameState.GameStates.PLACEMENT;

                break;
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }
    }
    if (currentState == GameState.GameStates.QUIT)
    {
        Console.WriteLine("Thanks for playing :)");
        Console.ReadKey();
        break;
    }
}