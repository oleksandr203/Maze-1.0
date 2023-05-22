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
        Random random = new Random();
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        

        public Grid_Set(int columns, int rows, int sizeOfCell)
        {
            Columns = columns;
            Rows = rows;            

            gridOfCells = new Cell[Columns, Rows]; 
            
                for (int r = 0; r < rows; r++ )
                {
                for (int c = 0; c < columns; c++)
                {
                    gridOfCells[r, c] = new Cell(r, c, sizeOfCell);
                    gridOfCells[c, r].CanNotMoveDown();
                    gridOfCells[c, r].CanNotMoveRight();
                }
            }
            StartCell();
            FinishCell();
        }

        private void StartCell()
        {
            gridOfCells[0, random.Next(Columns)].SetStartCell();
        }
        
        private void FinishCell()
        {
            gridOfCells[9, random.Next(Columns)].SetFinishCell();
        }

        public Cell[,] GetCells()
        {
            return gridOfCells;
        }
    }
}
