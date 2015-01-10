using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Amv.Geo.Core;
using Amv.OsmGeo.MapLayer;


namespace Amv.GeoClient.WinForms
{
    /// <summary>
    /// маркер который необходим для рисования рисунка в виде указателя на отображаемое на карте место
    /// </summary>
    public class GeoLocatinMarker : MapMarkerBase
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="appMarkerPoint"></param>
        /// <param name="markerSize"></param>
        public GeoLocatinMarker(Point appMarkerPoint, Size markerSize)
            : base(appMarkerPoint, markerSize) {

        }

        protected override void CalcMarkerAppPaneBounds() {
            //расчитываем координаты на панели карты 
           this.AppMarkerPoint=new Point(this.AppLocationPoint.X - (this.Size.Width / 2),this.AppLocationPoint.Y - this.Size.Height);
           this.AppMarkerBounds = new Rectangle(this.AppMarkerPoint, this.Size);
        }
    }
}
