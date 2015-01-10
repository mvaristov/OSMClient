using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amv.Geo.Core;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// класс предоставляющий доступ к кешированным тайлам карты
    /// </summary>
    public class CacheMapTiles:ICacheMapLayer
    {
        private object _sync = new object();
        private ThreadSafeDictionary<string, MapTileBase> _cacheDictionary;

        /// <summary>
        /// конструктор
        /// </summary>
        public CacheMapTiles() {
            this._cacheDictionary = new ThreadSafeDictionary<string, MapTileBase>();
        }

        /// <summary>
        /// добавление тайла в кэш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tile"></param>
        public void AddMapTile(string key, MapTileBase tile) {
            this._cacheDictionary.Add(key, tile);
        }

        /// <summary>
        /// получение тайла из кэша по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MapTileBase this[string key] {
            get { return this._cacheDictionary[key]; }
        }

        /// <summary>
        /// проверка наличия тайла в кэше
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsMapTile(string key) {
            return this._cacheDictionary.ContainsKey(key);
        }

        /// <summary>
        /// получение тайла из кэша по ключу без генерации исключения в случае его отсутствия
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tile"></param>
        public void TryGetMapTile(string key,out MapTileBase tile) {
            this._cacheDictionary.TryGetValue(key, out tile);
        }

        /// <summary>
        /// размер кэша (определяетя суммой байтов всех тайлов)
        /// </summary>
        /// <returns></returns>
        public int SizeMapTileCacheInKB() {
            int size = 0;
            foreach (MapTileBase tile in this._cacheDictionary.Values) {
                size += tile.DataBinary.Length;
            }
            return size * 1024;
        }

        /// <summary>
        /// удаление тайла из кэша по ключу
        /// </summary>
        /// <param name="key"></param>
        public void RemoveMapTile(string key) {
            this._cacheDictionary.Remove(key);
        }

        /// <summary>
        /// удаление тайла из кэша по ключу
        /// </summary>
        /// <param name="key"></param>
        public void RemoveSafeMapTile(string key) {
            this._cacheDictionary.RemoveSafe(key);
        }

        /// <summary>
        /// очистка всего кэша
        /// </summary>
        public void Clear() {
            this._cacheDictionary.Clear();
        }

        /// <summary>
        /// очистка из кэша тайлов, которые не имеют статус Sussed
        /// </summary>
        public void ClearUnsussedTiles() {
            ICollection<MapTileBase> unsussTiles=this._cacheDictionary.Values.Where(t=>t.MapTileDataState!=MapTileDataState.Success).ToList();
           foreach (MapTileBase t in unsussTiles) {
               this._cacheDictionary.RemoveSafe(t.TileKey);
           }
        }
    }
}
