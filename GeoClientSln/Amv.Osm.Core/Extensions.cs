using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amv.Geo.Core
{
    public static class Extensions
    {
        public static Point LeftTop(this Rectangle rect){
            return rect.Location;
        }

        public static Point LeftBottom(this Rectangle rect) {
            return new Point(rect.Left,rect.Bottom);
        }

        public static Point RightTop(this Rectangle rect) {
            return new Point(rect.Right, rect.Top);
        }

        public static Point RightBottom(this Rectangle rect) {
            return new Point(rect.Right, rect.Bottom);
        }

        //public static Rectangle SectionFrom(this Rectangle baseRectangle, Rectangle subRecangle) {
            //получаем прямоугольник который пересекается с subRectangle.
            //if(baseRectangle.co
           // Rectangle.
        //}

    }
}
