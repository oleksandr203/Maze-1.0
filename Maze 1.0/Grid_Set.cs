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

            gridOfCells = new Cell[Columns, Rows]; 
            for (int r = 0; r < rows; r++ )
            {
                for (int c = 0; c < columns; c++ )
                {
                    gridOfCells[c, r] = new Cell(r, c, sizeOfCell);
                    gridOfCells[c, r].CanNotMoveDown();
                    gridOfCells[c, r].CanNotMoveRight();
                }
            }            
        }
        
        public Cell[,] GetCells()
        {
            return gridOfCells;
        }
    }
}
