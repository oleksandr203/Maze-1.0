using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    internal class Maze_Cell
    {
        public bool CanDown {get;}
        public bool CanUp { get;}
        public bool CanLeft { get;}
        public bool CanRight { get;}
        public bool IsBridge { get; }
        public int StartOrFinshCell { get; }

        public Maze_Cell(int rowX, int columnY, bool canUp, bool canRight, bool canDown, bool canLeft, bool isBridge, int startOrFinishCell) 
        {
        Point point = new Point(rowX, columnY);
            CanUp = canUp;
            CanDown = canDown;
            CanLeft = canLeft;
            CanRight = canRight;
            StartOrFinshCell = startOrFinishCell;
        }
    }
}
