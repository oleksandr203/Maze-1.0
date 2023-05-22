using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class GridGameState
    {
        int a;
        Cell[,] gridOfCells;
        Cell currentCell;
        Cell nextCell;
        Cell startCell;
        Cell finishCell;
        Random random = new Random();
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public GridGameState(int columns, int rows, int sizeOfCell)
        {
            Columns = columns;
            Rows = rows;

            gridOfCells = new Cell[Columns, Rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    gridOfCells[c, r] = new Cell(c, r, sizeOfCell);
                    gridOfCells[c, r].SetEmptyCell();
                    gridOfCells[c, r].CanNotMoveDown();
                    gridOfCells[c, r].CanNotMoveRight();
                }
            }            
            PathMaze();            
        }

        private void PathMaze() //logic here
        {
            startCell = gridOfCells[StartCell(), 0]; 
            finishCell = gridOfCells[FinishCell(), 0];
            MakeSteps(startCell, finishCell);
        }

        private int[] PossibleWays(Cell cell) 
        {
            currentCell = cell;
            int[] ways = new int[4];
            int xCUp = currentCell.X / 50 - 1;
            if (xCUp < 0) { xCUp = 0; }
            int xCDown = currentCell.X / 50;
            int yRLeft = currentCell.Y / 50;
            int yRRight = currentCell.Y / 50;
                      
                Cell cellUp = gridOfCells[currentCell.X / 50, xCUp];
                Cell cellDown = gridOfCells[currentCell.X / 50, (currentCell.Y / 50) + 1];
                Cell cellLeft = gridOfCells[(currentCell.X / 50) - 1, currentCell.Y / 50];
                Cell cellRight = gridOfCells[(currentCell.X / 50) + 1, currentCell.Y / 50];
           
            if (cellUp.Id == 0)
                ways[0] = 1;
            if (cellDown.Id == 0)
                ways[1] = 1;
            if (cellLeft.Id == 0)
                ways[2] = 1;
            if (cellRight.Id == 0)
                ways[3] = 1;            

            return ways;
        }

        private void MakeSteps(Cell startCell, Cell finishCell)
        {
            Random ra = new Random();
            int rand = 0;//ra.Next(4);
            
            int xC = startCell.X / 50;
            int yR = startCell.Y / 50;
            currentCell = startCell;
            
            int[] way = PossibleWays(currentCell);
           
            for(int i = 0; i < way.Length; i++)
            {
                if (rand == 0 && yR < 11 && way[1] == 1)//down
                {
                    gridOfCells[xC, yR].CanMoveDown();
                    gridOfCells[xC, yR + 1].SetFlagCell();                   
                }
            }
            
                //if (rand == 1 && yR > 1 )//up
                //{
                //    gridOfCells[xC, yR - 1].CanMoveDown();
                //    gridOfCells[xC, yR - 1].SetFlagCell();

                //}
                //if (rand == 2 && xC > 1 && IsNearEmpty(gridOfCells[xC - 1, yR]))//left
                //{
                //    gridOfCells[xC - 1, yR].CanMoveRight();
                //    gridOfCells[xC, yR].SetFlagCell();

                //}
                //if (rand == 3 && xC < 9 && IsNearEmpty(gridOfCells[xC + 1, yR]))//right
                //{
                //    gridOfCells[xC, yR].CanMoveRight();
                //    gridOfCells[xC, yR].SetFlagCell();
                //}
                                
        }


        private int StartCell()
        {
            int c = random.Next(Columns);
            gridOfCells[c, 0].SetStartCell();
            return c;
        }
        
        private int FinishCell()
        {
            int c = random.Next(Columns);
            gridOfCells[c, Rows-1].SetFinishCell();
            return c;
        }

        public Cell[,] GetCellsShot()
        {
            return gridOfCells;
        }
    }
}
