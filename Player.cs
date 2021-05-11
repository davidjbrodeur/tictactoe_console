namespace Tic_Tac_Toe_console
{
    class Player
    {
        private string name;
        private Cell.CellType markType; 

        public Player(string n, Cell.CellType markType)
        {
            name = n;
            this.markType = markType;
        }

        public char getMark()
        {
            return this.markType.ToString()[0];
        }

        public string getName()
        {
            return this.name;
        }

    }
}