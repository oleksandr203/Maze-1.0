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
                Id = 1;
            }
        }

        public void SetFinishCell()
        {
            if (!IsFinishCell)
            {
                IsFinishCell = true;
                Id = 0;
            }
        }

        public void SetFlagCell()
        {
            if (Id != 3)
            {
                Id = 3;
            }
        }

        public void SetEmptyCell()
        {
             Id = 0;
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
            Id = 0;
            IsStepped = false;
            IsStartCell = false;
            IsFinishCell = false;            
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
