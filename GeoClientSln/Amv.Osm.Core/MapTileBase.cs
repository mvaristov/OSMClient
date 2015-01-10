using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amv.Geo.Core
{
    /// <summary>
    /// список состояний тайла карты при получении данных
    /// </summary>
    public enum MapTileDataState
    {
        Error,
        Downloading,
        Success,
        Cancel
    }

    /// <summary>
    /// класс описывающий тайл карты
    /// </summary>
    [Serializable]
    public abstract class MapTileBase
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="zoom"></param>
        public MapTileBase(int zoom) {
            this._zoom=zoom;
        }

        /// <summary>
        /// масштаб при котором данный тайл будет отображаться
        /// </summary>
        public int Zoom {
            get { return this._zoom; }
        }
        protected int _zoom;

        /// <summary>
        /// координаты тайла в системе геолокации
        /// </summary>
        public Point TileCoords { get; set; }

        /// <summary>
        /// координаты тайла в панели приложения 
        /// </summary>
        public Point AppPaneCoords { get; set; }

        /// <summary>
        /// область занимаемая тайлом на глобальной панели мира
        /// </summary>
        public virtual Rectangle WorldPaneBounds {
            get { return new Rectangle(new Point(this.TileCoords.X*256,this.TileCoords.Y*256), this.TileSize); }
        }

        /// <summary>
        /// область занимаемая тайлом на панели приложения
        /// </summary>
        public virtual Rectangle AppPaneBounds {
            get { return new Rectangle(this.AppPaneCoords, this.TileSize); }
        }

        /// <summary>
        /// урл запроса для данных тайла
        /// </summary>
        public abstract string TileUrl {get;}

        /// <summary>
        /// уникальный ключ тайла
        /// </summary>
        public abstract string TileKey { get; }

        /// <summary>
        /// размер тайла
        /// </summary>
        public abstract Size TileSize { get; }

        /// <summary>
        /// данные тайла
        /// </summary>
        public byte[] DataBinary { get; set; }

        /// <summary>
        /// текущее состояние получения данных тайла
        /// </summary>
        public MapTileDataState MapTileDataState { get; set; }
     
    }
}
