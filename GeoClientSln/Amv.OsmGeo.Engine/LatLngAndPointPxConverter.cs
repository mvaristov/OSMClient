using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Amv.Geo.Core;

namespace Amv.OsmGeo.MapLayer.Utils
{
   /// <summary>
   /// конвертер широты и долготы в глобальные координаты osm карты и обратно.
   /// взято из разбора кода java скрипта Leaflet используемого для отображения карт osm на сайте 
   /// openstreetmap.ru
   /// </summary>
    public class LatLngAndOsmPointConverter
    {
        public sealed class Transformer
        {
            private double _a;
            private double _b;
            private double _c;
            private double _d;
            private Transformer() {
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

            public static Transformer Inst {
                get { return _inst ?? (_inst = new Transformer()); }
            }
            private static Transformer _inst;
        }
       
        /// <summary>
        /// радиус для конверитрования
        /// </summary>
        private const double R = 6378137;

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
        public static PointD ConvertLatLngToOsmPoint(double lat, double lng, int zoom) {
            //конвертируем в сферические координаты.
            var d = Math.PI / 180;
            var max = 1 - 1E-15;
            var sin = Math.Max(Math.Min(Math.Sin(lat * d), max), -max);
            PointD pProject = new PointD(R * lng * d, R * Math.Log((1 + sin) / (1 - sin)) / 2);
            //трансформируем 
            return Transformer.Inst.Transform(pProject,calcScale(zoom));


        }
        /// <summary>
        /// конвертируем проектную координату в широту и долготу
        /// </summary>
        /// <param name="osmPoint"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static LatLng ConverOsmPointInLatLng(PointD osmPoint, int zoom) {
            PointD untransformedPoint = Transformer.Inst.Untransform(osmPoint, calcScale(zoom));
            var d = 180 / Math.PI;
            var latLng = new LatLng(
                (2 * Math.Atan(Math.Exp(untransformedPoint.Y / R)) - (Math.PI / 2)) * d,
                untransformedPoint.X * d / R);
            return latLng;
        }
    }
}
