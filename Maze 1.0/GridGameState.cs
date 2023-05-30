﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Security.Cryptography;

namespace Maze_1._0
{
    
    public class GridGameState
    {
        enum SidesOfWorld { Up = 0, Right = 1, Down = 2, Left = 3 }
        Cell[,] gridOfCells;
        Cell[,] stepOnCells;       
        Cell currentPosition;
        Cell currentCell;
        Cell currentCellAuto;
        Cell currentCellAutoTemp;
        bool successMarkNewCell = false;       
        bool[] currentDirections = { false, false, false, false };
       // SidesOfWorld[] tempDirections;
       // int stepsAfterCrossWays = 0;
        int numberOfDirects = 0;
        Random random = new Random();

        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public bool IsFinished { get; private set; } = false;
        public Point CurrentPosition { get; private set; }
        public Cell StartCellProp { get; private set; }
        public Cell FinishCellProp { get; private set; }
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
                }
            }
            stepOnCells = new Cell[Columns, Rows];

            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    stepOnCells[c, r] = new Cell(c, r);
                }
            }
            GenerateBranchesOnField();
            MarkLocalPlayerPositon();
        }

        private void GenerateBranchesOnField()
        {
            StartCell();
            FinishCell();
            MakeBranchingWay(StartCellProp);
        }

        private void GenerateAutoSolution()
        {
            gridOfCells[StartCellProp.X, StartCellProp.Y].MarkAsStepped(true);            
            //MakeAutoStepps();
            MakeAutoSteppsByRecursion();
            IsFinished = false;
        }

        private void MakeAutoSteppsByRecursion()
        {            
            currentCellAuto = StartCellProp;

            while (!IsFinished) //first part of solving
            {
                currentDirections = CurrentDirections(currentCellAuto);
                numberOfDirects = AvailableDirection();
                if (numberOfDirects == 0 & !IsFinished)
                {
                    break;
                }
                if (currentCellAuto == FinishCellProp)
                {
                    IsFinished = true;
                }
                if (numberOfDirects == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (currentDirections[i] == true)
                        {
                            MakeAStepByDirection(i);
                            break;
                        }
                    }
                }
                if (numberOfDirects > 1)
                {
                    currentCellAutoTemp = currentCellAuto;
                    break;
                }
            }
           // while (!IsFinished)
            {
                MakeSolutionSteps();
                if (!successMarkNewCell)
                {
                    MakeSolutionStepBack();
                }
            }

        }

        private void MakeSolutionSteps()
        {
            for (int d = 0; d < 4; d++)
            {
                int qu = 0;
                if (currentDirections[d] == true)
                {
                    MakeAStepByDirection(d);
                    while (!IsFinished)
                    {
                        currentDirections = CurrentDirections(currentCellAuto);
                        numberOfDirects = AvailableDirection();
                        if (numberOfDirects == 0 & !IsFinished)
                        {
                            currentCellAuto = currentCellAutoTemp;
                            break;
                        }
                        else if (currentCellAuto == FinishCellProp)
                        {
                            IsFinished = true;
                        }
                        else if (numberOfDirects == 1)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (currentDirections[i] == true)
                                {
                                    MakeAStepByDirection(i);
                                    break;
                                }
                            }
                        }
                        else if (numberOfDirects > 1 && qu < 20)
                        {
                            qu++;
                            break;
                        }
                    }
                }
            }
        }

        private void MakeSolutionStepBack()
        {

        }

        private void MakeAutoStepps()
        {             
            while (!IsFinished)
            {
                int randTemp = random.Next(4);
                ClearAutoHistory();
                currentCellAuto = StartCellProp;
                currentDirections = CurrentDirections(currentCellAuto);
                for (int c = 0; c < 200; c++)
                {                    
                    if (!IsFinished)
                    {
                        gridOfCells[StartCellProp.X, StartCellProp.Y].MarkAsStepped(true);
                        currentDirections = CurrentDirections(currentCellAuto);
                        AvailableDirection();
                        if (numberOfDirects == 0)
                        {
                            break;
                        }
                        else if (numberOfDirects == 1)
                        {
                            for (int i = 0; i < currentDirections.Length; i++)
                            {
                                if (currentDirections[i] == true)
                                {
                                    randTemp = i;
                                }
                            }
                        }
                        else
                        {
                            randTemp = random.Next(4);
                            while (!currentDirections[randTemp])
                            {
                                randTemp  = random.Next(4);
                            }
                        }
                        MakeAStepByDirection(randTemp);   // here must be a logic of recurs adding                     
                    }
                    if (IsFinished)
                    {
                        break;
                    }
                }
            }
                  
        }

        private void MakeAStepByDirection(int direct)
        {
            switch (direct)
            {
                case 0:
                    {
                        currentCellAuto = gridOfCells[currentCellAuto.X, currentCellAuto.Y - 1]; //up                        
                    }
                    break;
                case 1:
                    {
                        currentCellAuto = gridOfCells[currentCellAuto.X + 1, currentCellAuto.Y];//right                        
                    }
                    break;
                case 2:
                    {
                        currentCellAuto = gridOfCells[currentCellAuto.X, currentCellAuto.Y + 1]; //down                        
                    }
                    break;
                case 3:
                    {
                        currentCellAuto = gridOfCells[currentCellAuto.X - 1, currentCellAuto.Y]; //left                       
                    }
                    break;
            }
            CheckForFinish();
            MakeNewAutoCell();           
        }

        private void MakeNewAutoCell()
        {
            currentCellAuto.MarkAsStepped(true);
            currentDirections = CurrentDirections(currentCellAuto);            
        }

        private void CheckForFinish()
        {
            if (currentCellAuto.X == FinishCellProp.X && currentCellAuto.Y == FinishCellProp.Y)
            {
               IsFinished = true;
            }            
        }

        private int AvailableDirection()
        {
            numberOfDirects = 0;
            foreach (var direction in currentDirections)
            {
                if (direction)
                    numberOfDirects++;
            }
            return numberOfDirects;
        }

        private void ClearAutoHistory()
        {
            foreach (var cell in gridOfCells)
            {
                cell.ClearAutoStep();
            }
        }       

        private bool[] CurrentDirections(Cell _current)
        {
            //up
            if (_current.Y >= 1 && !gridOfCells[_current.X, _current.Y - 1].HorizontalWall && !gridOfCells[_current.X, _current.Y - 1].IsSteppedBySolution)
            {
                currentDirections[(int)SidesOfWorld.Up] = true;
            }
            else
            {
                currentDirections[(int)SidesOfWorld.Up] = false;
            }
            //right
            if (_current.X < Columns && !gridOfCells[_current.X, _current.Y].VerticalWall && !gridOfCells[_current.X + 1, _current.Y].IsSteppedBySolution)
            {
                currentDirections[(int)SidesOfWorld.Right] = true;
            }
            else
            {
                currentDirections[(int)SidesOfWorld.Right] = false;
            }
            //down
            if (_current.Y < Rows && !gridOfCells[_current.X, _current.Y].HorizontalWall && !gridOfCells[_current.X, _current.Y + 1].IsSteppedBySolution)
            {
                currentDirections[(int)SidesOfWorld.Down] = true;
            }
            else
            {
                currentDirections[(int)SidesOfWorld.Down] = false;
            }
            //left
            if (_current.X >= 1 && !gridOfCells[_current.X - 1, _current.Y].VerticalWall && !gridOfCells[_current.X - 1, _current.Y].IsSteppedBySolution)
            {
                currentDirections[(int)SidesOfWorld.Left] = true;
            }
            else
            {
                currentDirections[(int)SidesOfWorld.Left] = false;
            }
            return currentDirections;
        }

        private void MakeBranchingWay(Cell startCell)
        {
            currentCell = startCell;
            GenarateBranch(currentCell);

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
            if (currentPosition.GetPosition() != FinishCellProp.GetPosition())
            {
                currentPosition.MarkAsStepped(false);
            }
            else IsFinished = true;
        }

        public void StepUp()
        {
            if (currentPosition.Y >= 1 && !gridOfCells[currentPosition.X, currentPosition.Y - 1].HorizontalWall)
                currentPosition = stepOnCells[currentPosition.X, currentPosition.Y - 1];
        }

        public void StepDown()
        {
            if (!gridOfCells[currentPosition.X, currentPosition.Y].HorizontalWall && currentPosition.Y < Rows)
                currentPosition = stepOnCells[currentPosition.X, currentPosition.Y + 1];
        }

        public void StepLeft()
        {
            if (currentPosition.X >= 1 && !gridOfCells[currentPosition.X - 1, currentPosition.Y].VerticalWall)
                currentPosition = stepOnCells[currentPosition.X - 1, currentPosition.Y];
        }

        public void StepRight()
        {
            if (!gridOfCells[currentPosition.X, currentPosition.Y].VerticalWall && currentPosition.X < Rows)
                currentPosition = stepOnCells[currentPosition.X + 1, currentPosition.Y];
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

        private void GenarateBranch(Cell currentCell)
        {
            Random rand = new Random();
            int variatyOfMaxWays = 3;

            while (IsFreeCell())
            {
                currentCell = SetFlags(rand.Next(4), currentCell);
                if (successMarkNewCell)
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
                        gridOfCells[currentCell.X, currentCell.Y - 1].NoWallDown();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X, currentCell.Y - 1];
                    }
                    break;
                case 1:
                    if (CanDown(currentCell) && gridOfCells[currentCell.X, currentCell.Y + 1].Id == 0)
                    {
                        gridOfCells[currentCell.X, currentCell.Y + 1].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].NoWallDown();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X, currentCell.Y + 1];
                    }
                    break;
                case 2:
                    if (CanLeft(currentCell) && gridOfCells[currentCell.X - 1, currentCell.Y].Id == 0)
                    {
                        gridOfCells[currentCell.X - 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X - 1, currentCell.Y].NoWallRight();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X - 1, currentCell.Y];
                    }
                    break;
                case 3:
                    if (CanRight(currentCell) && gridOfCells[currentCell.X + 1, currentCell.Y].Id == 0)
                    {
                        gridOfCells[currentCell.X + 1, currentCell.Y].SetFlagCell();
                        gridOfCells[currentCell.X, currentCell.Y].NoWallRight();
                        successMarkNewCell = true;
                        return gridOfCells[currentCell.X + 1, currentCell.Y];
                    }
                    break;
            }
            return currentCell;
        }

        private void StartCell()
        {
            int c = random.Next(0, Columns);
            gridOfCells[c, 0].SetStartCell();
            currentPosition = stepOnCells[c, 0];
            CurrentPosition = stepOnCells[c, 0].GetPosition();
            StartCellProp = currentPosition;
        }

        private void FinishCell()
        {
            int c = random.Next(0, Columns);
            gridOfCells[c, Rows - 1].SetFinishCell();
            FinishCellProp = gridOfCells[c, Rows - 1];
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

        public Cell[,] GetStepsPoints()
        {
            CurrentPosition = currentPosition.GetPosition();
            return stepOnCells;
        }

        public Cell[,] GetAutoSolution()
        { 
                GenerateAutoSolution();
                return gridOfCells;
        }
    }
}
