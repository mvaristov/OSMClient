using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amv.Geo.Core
{
    
    /// <summary>
    /// описывает отдельную мировую координату (широта и долгота)
    /// </summary>
    [Serializable]
    public class LatLng
    {
        /// <summary>
        /// широта 
        /// </summary>
        public double Lat { get;  set; }
        /// <summary>
        /// долгота
        /// </summary>
        public double Lng { get;  set; }

        /// <summary>
        /// конструктор
        /// </summary>
        public LatLng() {
        }
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public LatLng(double lat, double lng):this() {
            this.Lat = lat;
            this.Lng = lng;
        }
    }
}
