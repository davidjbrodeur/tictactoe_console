using System;

namespace Tic_Tac_Toe_console
{
    class Game
    {
        private Player[] players = new Player[2];
        private Player activePlayer; 
        Grid gridGame;

        public Game(string n1, string n2)
        {
            players[0] = new Player(n1, Cell.CellType.X);
            players[1] = new Player(n2, Cell.CellType.O);
            this.activePlayer = players[0];
            gridGame = new Grid();
        }

        public Player getPlayer(int playerNumber)
        {
            return players[playerNumber];
        }
        public Player getActivePlayer()
        {
            return activePlayer;
        }
        public Grid getGridGame()
        {
            return this.gridGame;
        }
        public string showGame()
        {
            Console.Clear(); 
            return this.gridGame.showGamingGrid();
        }
        public void switchActivePlayer()
        {
            if(this.activePlayer == players[0])
            {
                this.activePlayer = players[1];
            } else
            {
                this.activePlayer = players[0]; 
            }
        }

    }
}