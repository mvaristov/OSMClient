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
    /// маркер который используется для показа какая широта и долгота на данный момент отображается в центре панели карты
    /// </summary>
    public class CurrentLatLngMapMarker : MapMarkerBase
    {
        /// <summary>
        /// текущие широта и долгота
        /// </summary>
        public LatLng CurrentLatLng { get; private set; }
        
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="appMarkerPoint"></param>
        /// <param name="size"></param>
        /// <param name="latLng"></param>
        public CurrentLatLngMapMarker(Point appMarkerPoint,Size size,LatLng latLng)
            : base(appMarkerPoint, size) {           
            //расчитываем ширину строки текущей латлнг.
                this.CurrentLatLng = latLng;

        }

        /// <summary>
        /// расчитаем область, где выводить данные
        /// </summary>
        protected override void CalcMarkerAppPaneBounds() {
            //расчитываем координаты на панели карты 
           this.AppMarkerPoint=new Point(this.AppLocationPoint.X,this.AppLocationPoint.Y - this.Size.Height);
           this.AppMarkerBounds = new Rectangle(this.AppMarkerPoint, this.Size);
        }

        /// <summary>
        /// построение строки с широтой и долготой
        /// </summary>
        /// <returns></returns>
        public string BuildLatLngString() {
            return string.Format("{0}:{1}", this.CurrentLatLng.Lat,this.CurrentLatLng.Lng);
        }

       
    }
}
