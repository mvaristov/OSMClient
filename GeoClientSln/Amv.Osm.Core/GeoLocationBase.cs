using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// класс описывающий показываемое на карте место.
    /// </summary>
    [Serializable]
    public abstract class GeoLocationBase
    {
        /// <summary>
        /// текущий масштаб, при котором отображается данное место геолокации
        /// </summary>
        public abstract int Zoom { get; set; }

        

        /// <summary>
        /// идентификатор места геолокации
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// класс места геолокации
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// тип места геолокации
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ранг геолокации
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// широта и долгота места
        /// </summary>
        public LatLng LatLng {
            get {
                return (this._latLng)??(this._latLng=new LatLng());
            }
            set {
                this._latLng = value;
            }
        }
        private LatLng _latLng;

        /// <summary>
        /// значение места геолакации
        /// </summary>
        public float Importance { get; set; }

        /// <summary>
        /// отображаемое имя
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// расчет начального масштаба для места геолокации при первом показе на карте.
        /// </summary>
        public abstract void CalcInitZoom();

        /// <summary>
        /// массив координат (широта долгота) на карте показывающих отображаемый гео объект
        /// </summary>
        public List<LatLng> GeoPoligon {
            get { return this._geoPoligon ?? (this._geoPoligon = new List<LatLng>()); }
        }
        protected List<LatLng> _geoPoligon;

        /// <summary>
        /// область (широта долгота) на карте  отображаемого гео объекта
        /// </summary>
        public LatLngBounds GeoBounds {
            get { return this._geoBounds ?? (this._geoBounds = new LatLngBounds()); }
        }
        protected LatLngBounds _geoBounds;

        /// <summary>
        /// урл для получения картинки для местоположения
        /// </summary>
        public string IconUrl { get; set; }

        
    }
}
