using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amv.GeoClient.WinForms
{
    public abstract class MapMarker
    {
        protected int countContainsCorners;
        protected Point _pointInMap;
        protected Size _sizeMarker;
        protected Rectangle _boundsForMap;

        public MapMarker(Point pointInMap, Size sizeMarker) {
            this._pointInMap = pointInMap;
            this._sizeMarker = sizeMarker;
            this.CalcBounds(this._pointInMap, this._sizeMarker);
        }

        protected virtual void ValidForDraw(){
        }

        public abstract void CalcBounds(Point pointInMap,Size sizeMarker);

        public abstract void Draw();
        
    }
}
