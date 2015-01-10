using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Amv.Geo.Core;


namespace Amv.GeoClient.WinForms
{
    /// <summary>
    /// маркер используется для определения, что все тайлы карты должны быть отрисованы.
    /// в основном применяется для уничтожения graphics.
    /// </summary>
    public class FullDrawMapMarker : MapMarkerBase
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="appMarkerPoint"></param>
        /// <param name="markerSize"></param>
        public FullDrawMapMarker(Point appMarkerPoint, Size markerSize)
            : base(appMarkerPoint, markerSize) {

        }

        protected override void CalcMarkerAppPaneBounds() {
            //расчитываем координаты на панели карты 
           this.AppMarkerBounds = new Rectangle(this.AppMarkerPoint, this.Size);
        }
    }
}
