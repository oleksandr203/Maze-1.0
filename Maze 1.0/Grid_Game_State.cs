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
        bool successMarkNewCell = false;        
        Cell startCell;
        Cell finishCell;
        Random random = new Random();
        int cellAll;
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
            bool fullField;

            startCell = gridOfCells[StartCell(), 0]; 
            finishCell = gridOfCells[FinishCell(), Rows-1];
            MakeSteps(startCell);                                
        }

        private bool IsFreeCell()
        {
            foreach (var cell in gridOfCells)        
            if (cell.Id == 0)
            {
                return true;
            }
            return false;             
        }

        private void MakeSteps(Cell startCell)
        {            
            cellAll = (Rows - 1) * (Columns - 1);

            currentCell = startCell;            
            GenarateSteps(ref cellAll, currentCell);
                      
            for (;cellAll > 0;)
            {
                Cell[] cellsForBranch = CellForBranching(); 
                GenarateSteps(ref cellAll, cellsForBranch[random.Next(cellsForBranch.Length)]);
            }           
        }

        private Cell[] CellForBranching()
        {
            List<Cell> capacity = new List<Cell>();
            foreach (var cell in gridOfCells)
                if (cell.Id == 3)
                {
                   capacity.Add(cell);   
                }           
            return capacity.ToArray();
        }

        private void GenarateSteps(ref int cellReamins, Cell currentCell)
        {
            Random rand = new Random();
            int variatyOfMaxWays = 3;

            for (int b = cellReamins; ;)
            {
                int ra = rand.Next(4);               

                currentCell = SetFlags(ra, currentCell);
                if(successMarkNewCell)
                {
                    cellReamins--;
                    successMarkNewCell = false;
                }                
               
                if (!successMarkNewCell && variatyOfMaxWays > 0)
                {
                    variatyOfMaxWays--;
                    currentCell = SetFlags(rand.Next(4), currentCell);

                    if (successMarkNewCell)
                    {
                        cellReamins--;
                        successMarkNewCell = false;
                        variatyOfMaxWays = 3;
                    }                    
                }
                if (cellReamins <= 0 | variatyOfMaxWays <= 0)
                    break;
            }
        }       

        private Cell SetFlags(int random, Cell currentCell)
        {            
           
            switch (random)
            {
                case 0:
                    if (CanUp(currentCell) && gridOfCells[currentCell.X, currentCell.Y - 1].Id == 0)
                    {
                        gridOfCells[currentCell.X, currentCell.Y - 1].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y - 1].CanMoveDown();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X, currentCell.Y - 1];
                    }
                    break;
                case 1:
                    if (CanDown(currentCell) && gridOfCells[currentCell.X, currentCell.Y + 1].Id == 0)
                    {
                        gridOfCells[currentCell.X, currentCell.Y + 1].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].CanMoveDown();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X, currentCell.Y + 1];
                    }
                    break;
                case 2:
                    if (CanLeft(currentCell) && gridOfCells[currentCell.X - 1, currentCell.Y].Id == 0)
                    {
                        gridOfCells[currentCell.X - 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X - 1, currentCell.Y].CanMoveRight();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X - 1, currentCell.Y];
                    }
                    break;
                case 3:
                    if (CanRight(currentCell) && gridOfCells[currentCell.X + 1, currentCell.Y].Id == 0)
                    {
                        gridOfCells[currentCell.X + 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].CanMoveRight();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X + 1, currentCell.Y];
                    }
                    break;                                 
            }
            return currentCell;

            //switch (random)
            //{
            //    case 0:
            //        if (CanUp(currentCell) && gridOfCells[currentCell.X, currentCell.Y - 1].Id == 0)
            //        {
            //            gridOfCells[currentCell.X, currentCell.Y - 1].SetFlagCell();
            //            gridOfCells[currentCell.X, currentCell.Y - 1].CanMoveDown();
            //            successMarkNewCell = true;
            //            return currentCell = gridOfCells[currentCell.X, currentCell.Y - 1];
            //        }
            //        break;
            //    case 1:
            //        if (CanDown(currentCell) && gridOfCells[currentCell.X, currentCell.Y + 1].Id == 0)
            //        {
            //            gridOfCells[currentCell.X, currentCell.Y + 1].SetFlagCell();
            //            gridOfCells[currentCell.X, currentCell.Y].CanMoveDown();
            //            successMarkNewCell = true;
            //            return currentCell = gridOfCells[currentCell.X, currentCell.Y + 1];
            //        }
            //        break;
            //    case 2:
            //        if (CanLeft(currentCell) && gridOfCells[currentCell.X - 1, currentCell.Y].Id == 0)
            //        {
            //            gridOfCells[currentCell.X - 1, currentCell.Y].SetFlagCell();
            //            gridOfCells[currentCell.X - 1, currentCell.Y].CanMoveRight();
            //            successMarkNewCell = true;
            //            return currentCell = gridOfCells[currentCell.X - 1, currentCell.Y];
            //        }
            //        break;
            //    case 3:
            //        if (CanRight(currentCell) && gridOfCells[currentCell.X + 1, currentCell.Y].Id == 0)
            //        {
            //            gridOfCells[currentCell.X + 1, currentCell.Y].SetFlagCell();
            //            gridOfCells[currentCell.X, currentCell.Y].CanMoveRight();
            //            successMarkNewCell = true;
            //            return currentCell = gridOfCells[currentCell.X + 1, currentCell.Y];
            //        }
            //        break;
            //}
            //return currentCell;
        }

        private int StartCell()
        {
            int c = random.Next(0, Columns);
            gridOfCells[c, 0].SetStartCell();
            return c;
        }
        
        private int FinishCell()
        {
            int c = random.Next(0, Columns);
            gridOfCells[c, Rows-1].SetFinishCell();
            return c;
        }

        private bool CanUp(Cell current)
        {
           if (current.Y > 1 && gridOfCells[current.X, current.Y ].Id == 0)
                return true;
           return false;
        }

        private bool CanDown(Cell current)
        {
            if (current.Y < Rows  && gridOfCells[current.X, current.Y + 1].Id == 0)
                return true;
            return false;
        }

        private bool CanRight(Cell current)
        {
            if (current.X < Columns  && gridOfCells[current.X + 1, current.Y].Id == 0)
                return true;
            return false;
        }

        private bool CanLeft(Cell current)
        {
            if (current.X > 1 && gridOfCells[current.X, current.Y].Id == 0)
                return true;
            return false;
        }

        public Cell[,] GetCellsShot()
        {
            return gridOfCells;
        }
    }
}
