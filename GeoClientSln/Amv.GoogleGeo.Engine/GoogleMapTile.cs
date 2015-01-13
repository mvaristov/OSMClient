using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Amv.Geo.Core;

namespace Amv.GoogleGeo.MapLayer
{
    /// <summary>
    /// класс описывающий тайл карты для системы osm
    /// </summary>
    public class GoogleMapTile:MapTileBase
    {
        /// <summary>
        /// размер тайла
        /// </summary>
        public const int TILE_SIZE = 256;
        /// <summary>
        /// шаблон урл для получения данных тайла
        /// </summary>
        public const string TILE_URL_TEMPLATE = "https://mts{0}.googleapis.com/vt?pb=!1m4!1m3!1i{1}!2i{2}!3i{3}!2m3!1e0!2sm!3i000000000!3m9!2sru!3sUS!5e18!12m1!1e47!12m3!1e37!2m1!1ssmartmaps!4e0";//"http://vec0{0}.maps.yandex.net/tiles?l=map&x={2}&y={3}&z={1}";
        /// <summary>
        /// список доступных поддоменов серверов для урл тайла
        /// </summary>
        public const string TILE_SUBDOMAINS = "123"; 

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="zoom"></param>
        public GoogleMapTile(int zoom)
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
