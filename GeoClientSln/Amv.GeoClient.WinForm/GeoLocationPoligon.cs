using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amv.Geo.Core;
using System.Drawing;

namespace Amv.GeoClient.WinForms
{
    public class GeoLocationPoligon:MapPoligonBase
    {
        public override void CalcAppPointFromLatLng(IGeoMapLayer geoMapLayer,Rectangle mapBounds,int zoom, List<LatLng> latLngs,LatLng baseLatLng) {
            this.AppMapPanePoints = new List<Point>(latLngs.Count);
            foreach (LatLng latLng in latLngs) {
                this.AppMapPanePoints.Add(geoMapLayer.GetLocationInMapPaneFromLatLng(mapBounds,zoom,latLng,baseLatLng));
            }
        }
    }
}
