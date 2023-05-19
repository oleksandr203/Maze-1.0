using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_1._0
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }
        private readonly int _y;        
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
