using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze_1._0
{
    public class StepOnCell
    {
        Point step = new Point();

        public int Id { get; private set; } = 0;
        
        public StepOnCell(int r, int c) 
        {
           step = new Point(r, c);
        }

        public void MarkAsStepped()
        {
            if (Id == 0)
            {
                Id = 1;
            }
            else Id = 2;
        }
        public void ClearStep()
        {
            Id = 0;
        }
    }
}
