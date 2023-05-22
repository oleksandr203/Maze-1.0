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
              
        public bool VetricalWall{ get; private set; }
        public bool HorizontalWall { get; private set; }
        public bool IsStartCell { get; private set; }
        public bool IsFinishCell { get; private set; }
        public int Id { get; private set; }

        public Cell(int column, int row) 
        { 
            X = column;
            Y = row;
            HorizontalWall = true;
            VetricalWall = true;
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
                Id = 2;
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

        public void CanMoveRight()
        {
            if (VetricalWall)
            {
                VetricalWall = false;
            }           
        }

        public void CanMoveDown()
        {
            if (HorizontalWall)
            {
                HorizontalWall = false;
            }           
        }
        public void CanNotMoveRight()
        {
            if (VetricalWall)
            {
                VetricalWall = true;
            }
        }

        public void CanNotMoveDown()
        {
            if (HorizontalWall)
            {
                HorizontalWall = true;
            }           
        }

        public Point GetPosition()
        {
            return new Point(X, Y);
        }        
    }
}
