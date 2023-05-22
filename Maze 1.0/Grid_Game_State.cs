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
        Cell[,] gridOfCells;
        Cell currentCell;
        Cell currentCellRevers;
        Cell upperOfCurrent;
        Cell lowerOfCurrent;
        Cell rightOfCurrent;
        Cell leftOfCurrent;
        Cell nextCell;
        Cell startCell;
        Cell finishCell;
        Random random = new Random();
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public GridGameState(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            gridOfCells = new Cell[Columns, Rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    gridOfCells[c, r] = new Cell(c, r);
                    gridOfCells[c, r].SetEmptyCell();
                    gridOfCells[c, r].CanNotMoveDown();
                    gridOfCells[c, r].CanNotMoveRight();
                }
            }            
            GeneratePathOfMaze();            
        }

        private void GeneratePathOfMaze() //logic here
        {
            startCell = gridOfCells[StartCell(), 0]; 
            finishCell = gridOfCells[FinishCell(), Rows-1];
            MakeSteps(startCell, finishCell);
        }

        private bool IsPossibleToStepHere(Cell cell) 
        {
            currentCell = cell;            
            if (currentCell.Id == 0)
            {
                return true;
            }
            return false;             
        }

        private void MakeSteps(Cell startCell, Cell finishCell)
        {
            Random rand = new Random();
            Random rand2 = new Random();
            int ra = rand.Next(4);  
            int rb = rand.Next(4);
            int rr = rand.Next(4);
            int re = rand2.Next(10);
            int rs = rand2.Next(10);
            int rq = rand2.Next(4);
            int sq = rand2.Next(4);
            currentCell = startCell;
            currentCellRevers = finishCell;            
            
                for (int b = 0; b < 100; b++)
                {
                    currentCell = SetFlags(ra, currentCell);
                    ra = rand.Next(4);
                }
            for (int b = 0; b < 50; b++)
            {
                currentCellRevers = SetFlags(rb, currentCellRevers);
                rb = rand.Next(4);
            }

            for (int b = 0; b < 15; b++)
            {
                SetFlags(rq, gridOfCells[re, rs]);
                rq = rand.Next(4);
            }
            
        }

        private void RandomBranch()
        {

        }

        private Cell SetFlags(int random, Cell currentCell)
        {
            switch (random)
            {
                case 0:
                    if (CanUp(currentCell))
                    {
                        gridOfCells[currentCell.X, currentCell.Y - 1].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y - 1].CanMoveDown();
                        return currentCell = gridOfCells[currentCell.X, currentCell.Y - 1];
                    }
                    break;
                case 1:
                    if (CanDown(currentCell))
                    {
                        gridOfCells[currentCell.X, currentCell.Y + 1].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].CanMoveDown();
                        return currentCell = gridOfCells[currentCell.X, currentCell.Y + 1];
                    }
                    break;
                case 2:
                    if (CanLeft(currentCell))
                    {
                        gridOfCells[currentCell.X - 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X - 1, currentCell.Y].CanMoveRight();
                        return currentCell = gridOfCells[currentCell.X - 1, currentCell.Y];
                    }
                    break;
                case 3:
                    if (CanRight(currentCell))
                    {
                        gridOfCells[currentCell.X + 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].CanMoveRight();
                       return currentCell = gridOfCells[currentCell.X + 1, currentCell.Y];
                    }
                    break;                                 
            }
            return currentCell;
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

        private bool CanUp(Cell current)
        {
           if (current.Y > 1 && gridOfCells[current.X, current.Y - 1].Id == 0)
                return true;
           return false;
        }

        private bool CanDown(Cell current)
        {
            if (current.Y < Rows - 1 && gridOfCells[current.X, current.Y + 1].Id == 0)
                return true;
            return false;
        }

        private bool CanRight(Cell current)
        {
            if (current.X < Columns - 1 && gridOfCells[current.X + 1, current.Y].Id == 0)
                return true;
            return false;
        }

        private bool CanLeft(Cell current)
        {
            if (current.X > 1 && gridOfCells[current.X - 1, current.Y].Id == 0)
                return true;
            return false;
        }

        public Cell[,] GetCellsShot()
        {
            return gridOfCells;
        }
    }
}
