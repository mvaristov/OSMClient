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
    /// класс помощник для отображения на карте панели информации по объекту геолокации
    /// </summary>
    public class PanelInfoMapMarker : MapMarkerBase
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="appMarkerPoint">начальная точка для маркера</param>
        /// <param name="markerSize">размер маркера</param>
        public PanelInfoMapMarker(Point appMarkerPoint, Size markerSize)
            : base(appMarkerPoint, markerSize) {

        }

        protected override void CalcMarkerAppPaneBounds() {
            //расчитываем координаты на панели карты 
            this.AppMarkerPoint = this.AppLocationPoint;//new Point(this.AppLocationPoint.X - (this.Size.Width / 2),this.AppLocationPoint.Y);
           this.AppMarkerBounds = new Rectangle(this.AppMarkerPoint, this.Size);
        }
    }
}
