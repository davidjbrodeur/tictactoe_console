using System;
using System.Collections.Generic;

namespace Tic_Tac_Toe_console
{
    class Grid
    {

        private Cell[] cells = new Cell[9];
        private int totalCell;

        public Grid()
        {
            for (int i = 0; i < 9; i++)
            {
                this.cells[i] = new Cell(i);
            }
        }
        public string showGamingGrid()
        {
            string result = "\n";

            result = result + cells[0].getCellStatus() + " | ";
            result = result + cells[1].getCellStatus() + " | ";
            result = result + cells[2].getCellStatus();
            result = result + "\n_________ \n";

            result = result + cells[3].getCellStatus() + " | ";
            result = result + cells[4].getCellStatus() + " | ";
            result = result + cells[5].getCellStatus();
            result = result + "\n_________ \n";

            result = result + cells[6].getCellStatus() + " | ";
            result = result + cells[7].getCellStatus() + " | ";
            result = result + cells[8].getCellStatus();

            return result;
        }

        public bool checkIfCellIsAvailable(int i)
        {
            return this.cells[i].isEmpty();
        }

        public void inputMark(int i, Player p)
        {
            if (p.getMark() == 'X') cells[i].setCell(Cell.CellType.X);
            if (p.getMark() == 'O') cells[i].setCell(Cell.CellType.O);
            this.totalCell++;
        }

        public bool checkIfGameIsADraw()
        {
            if (this.totalCell == 9) return true;
            return false;
        }

        public bool checkVictory()
        {
            if (checkRowsForWin() || checkColumnForWin() || checkDiagonalForWin())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkRowsForWin()
        {
            if (checkIfTopRowIsAWin())
            {
                return true; 
            }
            if (checkIfMiddleRowIsAWin())
            {
                return true; 
            }
            if (checkIfBottomRowIsAWin())
            {
                return true;
            }
            return false; 
        }

        internal List<Cell> getAvailableCells()
        {
            List<Cell> availableCells = new List<Cell>(); 
            foreach(Cell c in cells)
            {
                if (c.isEmpty())
                {
                    availableCells.Add(c);
                }
            }
            return availableCells; 
        }

        private bool checkIfBottomRowIsAWin()
        {
            if (cells[6].getCellStatus() == cells[7].getCellStatus() && cells[6].getCellStatus() == cells[8].getCellStatus()) 
            {
                return true;
            }
            return false; 
        }

        private bool checkIfMiddleRowIsAWin()
        {
            if (cells[3].getCellStatus() == cells[4].getCellStatus() && cells[3].getCellStatus() == cells[5].getCellStatus()) 
            {
                return true;
            }
            return false; 
        }

        private bool checkIfTopRowIsAWin()
        {
            if (cells[0].getCellStatus() == cells[1].getCellStatus() && cells[0].getCellStatus() == cells[2].getCellStatus())
            {
                return true;
            }
            return false; 
        }

        public bool checkColumnForWin()
        {

            if (checkIfLeftColumnIsAWin())
            {
                return true; 
            }
            if (checkIfCenterColumnIsAWin())
            {
                return true;
            }
            if (checkIfRightColumnIsAWin())
            {
                return true;
            }
            return false;

        }

        private bool checkIfRightColumnIsAWin()
        {
            if (cells[2].getCellStatus() == cells[5].getCellStatus() && cells[2].getCellStatus() == cells[8].getCellStatus())
            {
                return true;
            }
            return false; 
        }

        private bool checkIfCenterColumnIsAWin()
        {
            if (cells[1].getCellStatus() == cells[4].getCellStatus() && cells[1].getCellStatus() == cells[7].getCellStatus()) 
            {
                return true;
            }
            return false; 
        }

        private bool checkIfLeftColumnIsAWin()
        {
            if (cells[0].getCellStatus() == cells[3].getCellStatus() && cells[0].getCellStatus() == cells[6].getCellStatus()) 
            { 
                return true;
            }
            return false; 
        }

        public bool checkDiagonalForWin()
        {
            if (checkIfLeftRightDiagonalIsAWin())
            {
                return true; 
            }
            if (checkIfRightLeftDiagonalIsAWin())
            {
                return true; 
            }
            return false;
        }

        private bool checkIfRightLeftDiagonalIsAWin()
        {
            if (cells[2].getCellStatus() == cells[4].getCellStatus() && cells[2].getCellStatus() == cells[6].getCellStatus())
            {
                return true;
            }
            return false; 
        }

        private bool checkIfLeftRightDiagonalIsAWin()
        {
            if (cells[0].getCellStatus() == cells[4].getCellStatus() && cells[0].getCellStatus() == cells[8].getCellStatus())
            {
                return true;
            }
            return false; 
        }
    }
}
