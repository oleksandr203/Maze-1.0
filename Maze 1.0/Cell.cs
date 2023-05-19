﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class Cell
    {
        public int SizeOfCell { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
              
        public bool VetricalWall{ get; private set; }
        public bool HorizontalWall { get; private set; }


        public Cell(int column, int row, int sizeOfCell) 
        {             
            SizeOfCell = sizeOfCell;
            X = column * sizeOfCell;
            Y = row * sizeOfCell;
            HorizontalWall = true;
            VetricalWall = true;
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

        public Point GetPositionLU()
        {
            return new Point(X, Y);
        }

        public Point GetPositionLD()
        {
            return new Point(X, Y + SizeOfCell);
        }

        public Point GetPositionRU()
        {
            return new Point(X + SizeOfCell, Y);
        }

        public Point GetPositionRD()
        {
            return new Point(X + SizeOfCell, Y + SizeOfCell);
        }

    }
}
