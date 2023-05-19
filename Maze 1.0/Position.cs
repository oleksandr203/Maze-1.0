using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }
               
        public Position(int c, int r)
        {
            X = c;
            Y = r;
        }

        public Point GetPosition()
        {
            return new Point(X, Y);
        }
    }
}
