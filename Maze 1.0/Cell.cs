using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }              
        public bool VerticalWall { get; private set; }
        public bool HorizontalWall { get; private set; }
        public bool IsStartCell { get; private set; }
        public bool IsFinishCell { get; private set; }
        public bool IsStepped { get; private set; } = false;  
        public bool IsSteppedBySolution { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsEmpty { get; private set; }
        
        public int Id { get; private set; }

        public Cell(int column, int row) 
        { 
            X = column;
            Y = row;
            HorizontalWall = true;
            VerticalWall = true;
        }

        public void SetStartCell()
        {
            if(!IsStartCell)
            {
                IsStartCell = true;                
            }
        }

        public void SetFinishCell()
        {
            if (!IsFinishCell)
            {
                IsFinishCell = true;              
            }
        }

        public void SetBuildFlagCell()
        {
            if (Id != 3)
            {
                Id = 3;
            }
            if (!IsBuild && !IsStartCell)
            {
                IsBuild = true;
            }
        }

        public void SetEmptyCell()
        {
            if (!IsEmpty)
            {
                IsBuild = false;
                IsEmpty = true;
            }            
        }

        public void NoWallRight()
        {
            if (VerticalWall)
            {
                VerticalWall = false;
            }           
        }

        public void NoWallDown()
        {
            if (HorizontalWall)
            {
                HorizontalWall = false;
            }           
        }

        public void ClearCell()
        {            
            SetEmptyCell();
            IsStepped = false;                     
        }
        
        public void ClearAutoStep()
        {
            if (IsSteppedBySolution)
            {
                IsSteppedBySolution = false;
            }
        }

        public void MarkAsStepped(bool auto)
        {
            if (!IsSteppedBySolution & auto)
            {
                IsSteppedBySolution = true;
            }
            else
            if (!IsStepped & !auto)
            {
                IsStepped = true;
            }
        }

        public Point GetPosition()
        {
            return new Point(X, Y);
        }        
    }
}
