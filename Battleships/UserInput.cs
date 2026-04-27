using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class UserInput
    {
        public static (int x, int y) GetCooridnateInput()
        {
            int x, y;
            while (true)
            {
                string input = Console.ReadLine() ?? "Was null!";
                try
                {
                    // Map letter to x-coordinate (A -> 0, B -> 1, etc.)
                    char firstChar = char.ToUpper(input[0]);
                    char secondChar = input[1];
                    if (firstChar < 'A' || firstChar > 'J') throw new Exception(); ; // If Out of bounds
                    x = firstChar - 'A';

                    if (secondChar < '0' || secondChar > '9') throw new Exception(); // If Out of bounds
                    y = input[1] - '0';

                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            return (y, x); // Inverted in lack of better solution... Wonder how long this will last...
        }
    }
}
