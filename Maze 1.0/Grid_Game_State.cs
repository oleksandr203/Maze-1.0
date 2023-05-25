using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Maze_1._0
{
    public class GridGameState
    {
        Cell[,] gridOfCells;
        StepOnCell[,] stepOnCells;
        StepOnCell currentPlayerPosition;
        Cell currentCell;
        bool successMarkNewCell = false;        
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
            stepOnCells = new StepOnCell[Columns, Rows];

            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    stepOnCells[c, r] = new StepOnCell(c, r);
                }
            }

            GenerateBranchesOnField();
            MarkLocalPlayerPositon();
        }

        private void GenerateBranchesOnField() 
        {            
            startCell = gridOfCells[StartCell(), 0]; 
            finishCell = gridOfCells[FinishCell(), Rows-1];
            MakeBranchingWay(startCell);            
        }

        private void MakeBranchingWay(Cell startCell)
        { 
            currentCell = startCell;            
            GenarateBranch( currentCell);

            for (; IsFreeCell();)
            {
                Cell[] cellsForBranch = CellForBranching();
                try { GenarateBranch(cellsForBranch[random.Next(cellsForBranch.Length)]); }

                catch 
                {
                    foreach (var g in gridOfCells)
                        g.SetEmptyCell();
                        GenerateBranchesOnField(); 
                }
            }
        }

        public void MarkLocalPlayerPositon()
        {
            currentPlayerPosition.MarkAsStepped();
        }

        public void StepUp()
        {
            currentPlayerPosition = stepOnCells[currentPlayerPosition.X, currentPlayerPosition.Y - 1];
        }

        public void StepDown()
        {
            currentPlayerPosition = stepOnCells[currentPlayerPosition.X, currentPlayerPosition.Y + 1];
        }

        public void StepLeft()
        {
            currentPlayerPosition = stepOnCells[currentPlayerPosition.X - 1, currentPlayerPosition.Y];
        }

        public void StepRight()
        {
            currentPlayerPosition = stepOnCells[currentPlayerPosition.X + 1, currentPlayerPosition.Y];
        }

        private void Move(KeyEventArgs k)
        {

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

        private Cell[] CellForBranching()
        {
            List<Cell> cellCanBranch = new List<Cell>();
            foreach (var cell in gridOfCells)
                if (cell.Id == 3)
                {
                   cellCanBranch.Add(cell);
                }           
            return cellCanBranch.ToArray();
        }

        private void GenarateBranch( Cell currentCell)
        {
            Random rand = new Random();
            int variatyOfMaxWays = 3;

            while(IsFreeCell())
            {                
                currentCell = SetFlags(rand.Next(4), currentCell);
                if(successMarkNewCell)
                {
                    successMarkNewCell = false;
                }                
               
                else if (!successMarkNewCell && variatyOfMaxWays > 0)
                {
                    variatyOfMaxWays--;
                    currentCell = SetFlags(rand.Next(4), currentCell);

                    if (successMarkNewCell)
                    {
                        successMarkNewCell = false;
                        variatyOfMaxWays = 3;
                    }                    
                }
                else if (variatyOfMaxWays <= 0)
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
        }

        private int StartCell()
        {
            int c = random.Next(0, Columns);
            gridOfCells[c, 0].SetStartCell();
            currentPlayerPosition = stepOnCells[c, 0];            
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
           if (current.Y > 0 && gridOfCells[current.X, current.Y - 1].Id == 0)
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
            if (current.X > 0 && gridOfCells[current.X - 1, current.Y].Id == 0)
                return true;
            return false;
        }

        public Cell[,] GetCellsShot()
        {
            return gridOfCells;
        }
        public StepOnCell[,] GetStepsPoints()
        {
            return stepOnCells;
        }
    }
}
