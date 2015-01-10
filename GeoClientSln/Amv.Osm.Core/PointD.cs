using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Amv.Geo.Core
{
    /// <summary>
    /// класс описывающий точку положения в формате double
    /// </summary>
    [Serializable]
    public class PointD
    {
        /// <summary>
        /// координата X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// координата Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public PointD() {
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PointD(double x, double y) {
            this.X = x;
            this.Y = y;
        }
    }
}
