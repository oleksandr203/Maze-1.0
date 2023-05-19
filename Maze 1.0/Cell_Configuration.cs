using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_1._0
{
    public class Cell_Configuration
    {
        public Position LeftUpPoint { get; private set; }        
        public bool VetricalWall{ get; private set; }
        public bool HorizontalWall { get; private set; }

        public Cell_Configuration(int x, int y) 
        {
            LeftUpPoint = new Position(x, y);
            HorizontalWall = true;
            VetricalWall = true;
        }

        public void CanMoveRight()
        {
            if (!VetricalWall)
                VetricalWall = true;

            return;
        }

        public void CanMoveDown()
        {
            if (!HorizontalWall)
                HorizontalWall = true;

            return;
        }
        public void CanNotMoveRight()
        {
            if (VetricalWall)
                VetricalWall = false;

            return;
        }

        public void CanNotMoveDown()
        {
            if (HorizontalWall)
                HorizontalWall = false;

            return;
        }
    }
}
