using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class GameState
    {
        public enum GameStates
        {
            PLACEMENT,
            SHOOTING,
            GAMEOVER,
            QUIT
        }
    }
}