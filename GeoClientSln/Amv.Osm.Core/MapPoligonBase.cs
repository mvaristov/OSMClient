using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amv.Geo.Core
{
    public abstract class MapPoligonBase
    {
        public List<Point> AppMapPanePoints { get; protected set; }
        protected int _currentValidPoints;
        public MapPoligonBase() {
            
        }

        public virtual bool ValidForDraw(Rectangle appTileBounds) {
            foreach (Point pt in this.AppMapPanePoints) {
                if (appTileBounds.Contains(pt)) {
                    this._currentValidPoints++;
                }
            }
            return this._currentValidPoints >= this.AppMapPanePoints.Count();
        }

        public abstract void CalcAppPointFromLatLng(IGeoMapLayer geoMapLayer, Rectangle mapBounds, int zoom, List<LatLng> latLngs, LatLng baseLatLng);
    }
}
