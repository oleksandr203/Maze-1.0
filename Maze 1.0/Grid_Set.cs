using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class Grid_Set
    {
        Cell[,] gridOfCells;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Grid_Set(int rows, int columns, int sizeOfCell)
        {
            Rows = rows;
            Columns = columns;

            gridOfCells = new Cell[Rows, Columns]; 
            for (int r = 0; r < rows; r++ )
            {
                for (int c = 0; c < columns; c++ )
                {
                    gridOfCells[r, c] = new Cell(r, c, sizeOfCell);
                    gridOfCells[r, c].CanNotMoveDown();
                    gridOfCells[r, c].CanNotMoveRight();
                }
            }            
        }
        
        public Cell[,] GetCells()
        {
            return gridOfCells;
        }
    }
}
