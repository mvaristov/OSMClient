using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// интерфейс определяющий доступ к слою получения данных для карт геолокации
    /// </summary>
    public interface IGeoDataLayer
    {
        /// <summary>
        /// очистка кэша загруженных тайлов
        /// </summary>
        void ClearCacheTiles();

        /// <summary>
        /// получение данных для единичного тайлоа
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="dataTileComplete"></param>
        void GetMapTileDataAsync(MapTileBase tile, Action<MapTileBase> dataTileComplete);

        /// <summary>
        /// получение данных для коллекции тайлов
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="dataTileComplete"></param>
        void GetMapTilesDataAsync(IEnumerable<MapTileBase> tiles, Action<MapTileBase> dataTileComplete);

        /// <summary>
        /// отмена всех активных запросов
        /// </summary>
        void CancelPrevRequests();

        /// <summary>
        /// очистка из кэша тайлов, которые не имеют статус Sussed
        /// </summary>
        void ClearCacheUnsussedTiles();
    }
}
