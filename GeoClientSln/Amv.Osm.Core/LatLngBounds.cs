using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amv.Geo.Core
{
    /// <summary>
    /// описывает область в мировых координатах (широта и долгота)
    /// </summary>
    [Serializable]
    public class LatLngBounds
    {
        //юго - запад (правый верхний угол)
        private LatLng _southWest;
        //северо-восток(левый нижний угол области)
        private LatLng _northEast;
        //северо - запад(хуй знает пока какой угол)
        //private LatLng _northWest;
        //юго - восток()
        //private LatLng _southEast;
        public LatLngBounds()
            : this(new LatLng(), new LatLng()) {
        }
        //ctor
        public LatLngBounds(LatLng southWest, LatLng northEast) {
            this._southWest = southWest;
            this._northEast = northEast;
        }
        /// <summary>
        /// получит координаты центра области
        /// </summary>
        public LatLng GetCenter {
            get {
                return new LatLng(
                    (this._southWest.Lat + this._northEast.Lat) / 2,
                   (this._southWest.Lng + this._northEast.Lng) / 2
                );
            }
        }
        /// <summary>
        /// получить координаты по юго-востоку
        /// </summary>
        public LatLng GetSouthWest {
            get {
                return this._southWest;
            }
        }
        /// <summary>
        /// получить координату по север-востоку
        /// </summary>
        public LatLng GetNorthEast {
            get {
                return this._northEast;
            }
        }
        /// <summary>
        /// получить координату по северо-западу
        /// </summary>
        public LatLng GetNorthWest {
            get {
                return new LatLng(this.GetNorth, this.GetWest);

            }
        }
        /// <summary>
        /// получить координаты
        /// </summary>
        public LatLng GetSouthEast {
            get {
                return new LatLng(this.GetSouth, this.GetEast);
            }
        }
        /// <summary>
        /// получить координату по западу
        /// </summary>
        public double GetWest {
            get {
                return this._southWest.Lng;
            }
        }
        /// <summary>
        /// получить координату югу
        /// </summary>
        public double GetSouth {
            get {
                return this._southWest.Lat;
            }
        }

        /// <summary>
        /// получить координату по востоку
        /// </summary>
        public double GetEast {
            get {
                return this._northEast.Lng;
            }
        }
        /// <summary>
        /// получить координату по северу
        /// </summary>
        public double GetNorth {
            get {
                return this._northEast.Lat;
            }
        }


    }
}
