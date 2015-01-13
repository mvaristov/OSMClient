using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// конвертер карты элиптический меркатор (используется для карт систем яндекс и google)
    /// </summary>
    public static class WSD84LatLngAndPxConverter
    {
        /// <summary>
        /// малая полуось эллипсоида
        /// </summary>
        private const double R_MINOR = 6356752.314245179;

        /// <summary>
        /// расчет коэффициента масштабирования
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        private static double calcScale(int zoom) {
            var scale = 256 * Math.Pow(2, zoom);
            if (scale <= 0) {
                scale = 1;
            }
            return scale;
        }
        /// <summary>
        /// конвертируем координату по широте и долготе в проектную
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static PointD ConvertLatLngToPx(double lat, double lng, int zoom) {
            //конвертируем в сферические координаты.
            var d = Math.PI / 180;
            double r = MetricalPxTransformer.R,
            y = lat * d,
            tmp = R_MINOR / r,
            e = Math.Sqrt(1 - tmp * tmp),
            con = e * Math.Sin(y);

            var ts = Math.Tan(Math.PI / 4 - y / 2) / Math.Pow((1 - con) / (1 + con), e / 2);
            y = -r * Math.Log(Math.Max(ts, 1E-10));
            PointD pProject= new PointD(lng * d * r, y);
            return MetricalPxTransformer.Inst.Transform(pProject,calcScale(zoom));


        }
        /// <summary>
        /// конвертируем проектную координату в широту и долготу
        /// </summary>
        /// <param name="osmPoint"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static LatLng ConverPxInLatLng(PointD osmPoint, int zoom) {
            PointD untransformedPoint = MetricalPxTransformer.Inst.Untransform(osmPoint, calcScale(zoom));
            var d = 180 / Math.PI;
            double r =MetricalPxTransformer.R,
            tmp = R_MINOR / r,
            e = Math.Sqrt(1 - tmp * tmp),
            ts = Math.Exp(-untransformedPoint.Y / r),
            phi = Math.PI / 2 - 2 * Math.Atan(ts);

            for (double i = 0,dphi = 0.1, con; i < 15 && Math.Abs(dphi) > 1e-7; i++) {
                con = e * Math.Sin(phi);
                con = Math.Pow((1 - con) / (1 + con), e / 2);
                dphi = Math.PI / 2 - 2 * Math.Atan(ts * con) - phi;
                phi += dphi;
            }
            return new LatLng(phi * d, untransformedPoint.X * d / r);
        }
    }
}
