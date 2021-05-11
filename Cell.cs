namespace Tic_Tac_Toe_console
{
    class Cell
    {
        public enum CellType
        {
            X,
            O,
            Empty
        }

        CellType cellType = CellType.Empty; 
        int position = 0;
        public int Position { get => position; set => position = value; }

        public Cell(int p)
        {
            this.Position = p;
        }
        public void setCell(CellType cellTypeInput)
        {
            this.cellType = cellTypeInput; 
        }
        public string getCellStatus()
        {
            if(cellType == CellType.Empty)
            {
                return Position.ToString();
            } else
            {
                return cellType.ToString();
            }
        }
        public bool isEmpty()
        {
            if (cellType == CellType.Empty)
            {
                return true;
            }
            return false; 
        }
    }
}
