using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgApp_2_WinForms
{
    public class MyPoint
    {
        public double X { set; get; }

        public double Y { set; get; }

        public MyPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
