using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    public sealed class MetricalPxTransformer
    {
        /// <summary>
        /// большая полуось эллипсоида
        /// </summary>
        public const double R = 6378137;

        private double _a;
            private double _b;
            private double _c;
            private double _d;
            private MetricalPxTransformer() {
                this._a = 0.5 / (Math.PI * R);
                this._b = 0.5;
                this._c = -_a;
                this._d = 0.5;

            }
            public PointD Transform(PointD point, double scale) {
                point.X = scale * (_a * point.X + _b);
                point.Y = scale * (_c * point.Y + _d);
                return point;
            }
            public PointD Untransform(PointD point, double scale) {
                point.X = (point.X / scale - this._b) / this._a;
                point.Y = (point.Y / scale - this._d) / this._c;
                return point;
            }

            public static MetricalPxTransformer Inst {
                get { return _inst ?? (_inst = new MetricalPxTransformer()); }
            }
            private static MetricalPxTransformer _inst;
    }
}
