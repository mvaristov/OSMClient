using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// интерфейс определящий работу со слоем кеширования тайлов карты
    /// </summary>
    public interface ICacheMapLayer
    {
        /// <summary>
        /// добавление тайла в кэш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tile"></param>
        void AddMapTile(string key, MapTileBase tile);

        /// <summary>
        /// получение тайла из кэша
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        MapTileBase this[string key] { get; }

        /// <summary>
        /// проверка наличия тайла в кэше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsMapTile(string key);

        /// <summary>
        /// получение тайла из кэша без генерации исключение в случае отсутствия тайла
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tile"></param>
        void TryGetMapTile(string key, out MapTileBase tile);

        /// <summary>
        /// размер кэша. Определяется в сумме байтов данных для всех тайлов
        /// </summary>
        /// <returns></returns>
        int SizeMapTileCacheInKB();

        /// <summary>
        /// удаление тайла из кэша
        /// </summary>
        /// <param name="key"></param>
        void RemoveMapTile(string key);

        /// <summary>
        /// удаление тайла из кэша
        /// </summary>
        /// <param name="key"></param>
        void RemoveSafeMapTile(string key);

        /// <summary>
        /// очистка кэша
        /// </summary>
        void Clear();
        /// <summary>
        /// очистка из кэша тайлов, которые не имеют статус Sussed
        /// </summary>
        void ClearUnsussedTiles();
    }
}
