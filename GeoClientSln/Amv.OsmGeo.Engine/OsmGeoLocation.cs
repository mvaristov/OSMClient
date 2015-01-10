using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amv.Geo.Core;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// класс описывающий место положение на Osm карте
    /// </summary>
    public class OsmGeoLocation:GeoLocationBase
    {
        /// <summary>
        /// идентификатор объекта в системе osm
        /// </summary>
        public string OsmID { get; set; }

        /// <summary>
        /// тип объекта в системе osm
        /// </summary>
        public string OsmType { get; set; }

        /// <summary>
        /// установка и получение зума карты
        /// </summary>
        public override int Zoom {
            get { return this._zoom; }
            set {
                if (value < 0) this._zoom = 0;
                else if (value > 19) this._zoom = 19;
                else this._zoom = value;
            }

        }
        private int _zoom;

        /// <summary>
        /// расчет начального зума карты при отображении объекта
        /// </summary>
        public override void CalcInitZoom() {
            //расчитывем начальный зум исходя из значения importance
            this.Zoom = (this.Rank / 2) + 2;
        }

        
    }
}
