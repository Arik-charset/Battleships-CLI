using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class GameBoard
    {
        readonly static int boardSize = 10;
        public int remainingShips = 5; // Starting amount
        public bool IsHost = false; // Changed by host in main

        public int[,] gameBoard = new int[boardSize, boardSize];
        Dictionary<int, string> iconMap = new Dictionary<int, string> // Will need to change when ships have multiple parts
        {
            { 0, "🌊" }, // Not checked, No placed
            { 1, "🟦" }, // Checked and empty
            { 2, "🚢" }, // Ship
            { 3, "💥" }, // Hit
        };
        public void PrintBoard(bool isOpponentsBoard = false)
        {
            Console.Clear();
            if (isOpponentsBoard)
            {
                Console.WriteLine("              OPPONENTS BOARD:");
            }
            else
            {
                Console.WriteLine("                YOUR BOARD:");
            }
            Console.Write("  | A | B | C | D | E | F | G | H | I | J |");
            Console.WriteLine("\n  -----------------------------------------");
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                Console.Write($"{x} |");
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    Console.Write($"{GetIcon(gameBoard[x, y], isOpponentsBoard)} |");
                }
                Console.WriteLine("\n  -----------------------------------------");
            }
        }
        string GetIcon(int cellValue, bool isOpponentsBoard)
        {
            string icon;
            if (iconMap.ContainsKey(cellValue))
            {
                icon = iconMap[cellValue].ToString();
            }
            else // Pls never
            {
                return "?";
            }
            if (isOpponentsBoard && icon == "🚢") // Will need to change when ships have multiple parts
            {
                icon = "🌊";
            }
            return icon;
        }
        public void PlaceShip()
        {   
            var (x, y) = UserInput.GetCooridnateInput();
            if (gameBoard[x, y] == 0) // If not placed
            {
                gameBoard[x, y] = 2; // Ship
                remainingShips--;
            }
            else // Revoke
            {
                gameBoard[x, y] = 0;
                remainingShips++;
            }
        }
        public bool ShootShip() // Shoots own ship, called by opponent on behalf
        {
            while (true)
            {
                var (x, y) = UserInput.GetCooridnateInput();
                switch (gameBoard[x, y])
                {
                    case 0:
                        gameBoard[x, y] = 1;
                        return false;
                    case 1:
                        Console.WriteLine("Invalid input! Already checked.");
                        break;
                    case 2:
                        gameBoard[x, y] = 3;
                        return true;
                }
            }
        }
        public bool CheckGameOver()
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(0); y++)
                {
                    if (gameBoard[x, y] == 2) { return false; }
                }
            }
            return true; // If gameover
        }
    }
}
