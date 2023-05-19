using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_1._0
{
    public class Grid_Set
    {
        Cell_Configuration[,] gridOfCells;

        public Grid_Set(int rows, int columns)
        {
            gridOfCells = new Cell_Configuration[rows, columns]; 
            for (int r = 0; r < rows; r++ )
            {
                for (int c = 0; c < columns; c++ )
                {
                    gridOfCells[r, c] = new Cell_Configuration(r, c);
                    gridOfCells[r, c].CanMoveDown();
                    gridOfCells[r, c].CanMoveRight();
                }
            }            
        }

        public Cell_Configuration[,] CreateGrid( )
        {
            return gridOfCells;
        }
    }
}
