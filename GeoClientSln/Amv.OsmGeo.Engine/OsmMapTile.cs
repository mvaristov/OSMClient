using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Amv.Geo.Core;

namespace Amv.OsmGeo.MapLayer
{
    /// <summary>
    /// класс описывающий тайл карты для системы osm
    /// </summary>
    public class OsmMapTile:MapTileBase
    {
        /// <summary>
        /// размер тайла
        /// </summary>
        public const int TILE_SIZE = 256;
        /// <summary>
        /// шаблон урл для получения данных тайла
        /// </summary>
        public const string TILE_URL_TEMPLATE = "http://{0}.tile.openstreetmap.org/{1}/{2}/{3}.png";
        /// <summary>
        /// список доступных поддоменов серверов для урл тайла
        /// </summary>
        public const string TILE_SUBDOMAINS = "abc";

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="zoom"></param>
        public OsmMapTile(int zoom)
            : base(zoom) {
        }

        /// <summary>
        /// получение урл запроса для данных тайла
        /// </summary>
        public override string TileUrl {
            get {
                int subdomainIndex=(TileCoords.X + TileCoords.Y) % TILE_SUBDOMAINS.Length;
                if(subdomainIndex>=TILE_SUBDOMAINS.Length)subdomainIndex=TILE_SUBDOMAINS.Length-1;
                if(subdomainIndex<0)subdomainIndex=0;
                return string.Format(TILE_URL_TEMPLATE,
                    TILE_SUBDOMAINS[subdomainIndex],
                    this._zoom, TileCoords.X, TileCoords.Y);
            }
        }

        /// <summary>
        /// размер тайла
        /// </summary>
        public override Size TileSize {
            get { return new Size(TILE_SIZE, TILE_SIZE); }
        }

        /// <summary>
        /// уникальный ключ тайла
        /// </summary>
        public override string TileKey {
            get { return string.Format("{0}.{1}.{2}", this.Zoom, this.TileCoords.X, this.TileCoords.Y); }
        }
    }
}
