using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amv.Geo.Core;
using System.IO;
using System.Drawing;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// класс реализующий слой доступа к данным системы osm через протокол http
    /// </summary>
    public class HttpDataLayer:IGeoDataLayer
    {
        private object _sync = new object();
        /// <summary>
        /// поставщик кэша для тайлов
        /// </summary>
        private ICacheMapLayer _mapCacheLayer;

        /// <summary>
        /// список все веб клиентов, которые загружают данные для тайлов
        /// </summary>
        private List<MapTileDataClient> _mapTileDataClients = new List<MapTileDataClient>();


        /// <summary>
        /// конструктор
        /// </summary>
        public HttpDataLayer() {
            this._mapCacheLayer = new CacheMapTiles();
        }
   
        /// <summary>
        /// не реализовано
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public byte[] GetTileData(MapTileBase tile) {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// получение данных тайла в асинхронном режиме
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="dataTileComplete"></param>
        public void GetMapTileDataAsync(MapTileBase tile, Action<MapTileBase> dataTileComplete) {
            MapTileDataClient dataTileClient = new MapTileDataClient(tile, dataTileComplete);
            lock (_sync) {
                this._mapTileDataClients.Add(dataTileClient);
            }
            tile.MapTileDataState = MapTileDataState.Downloading;
            dataTileClient.DownloadDataAsync(); 
        }

        /// <summary>
        /// получение данных коллекции тайлов в асинхронном режиме.
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="dataTileComplete"></param>
        public void GetMapTilesDataAsync(IEnumerable<MapTileBase> tiles, Action<MapTileBase> dataTileComplete) {
            foreach (MapTileBase mt in tiles) {
                //проверяем может тайл с таким ключом уже есть в системе кеширования тайлов
                MapTileBase cachedTile = null;
                this._mapCacheLayer.TryGetMapTile(mt.TileKey, out cachedTile);
                if (cachedTile != null) {
                    //проверяем наличие у тайла данных
                    cachedTile.AppPaneCoords = mt.AppPaneCoords;
                    dataTileComplete(cachedTile);
                }
                else {
                    //тайла в кэше нет, добавляем его в кэш, и отправляем на загрузку данных
                    this._mapCacheLayer.AddMapTile(mt.TileKey, mt);
                    //инициализируем асинхронную загрузку данных для тайла
                    this.GetMapTileDataAsync(mt, (tile) => {
                        if (tile.MapTileDataState == MapTileDataState.Cancel||tile.MapTileDataState==MapTileDataState.Error) {
                            this._mapCacheLayer.RemoveSafeMapTile(tile.TileKey);
                        }
                        if (tile.MapTileDataState == MapTileDataState.Success) {
                            using (MemoryStream ms = new MemoryStream(tile.DataBinary)) {
                                tile.ImageTile = new Bitmap(ms);
                            }
                        }
                        //генерим изображение из байтов
                        dataTileComplete(tile);
                    });
                }
            }
        }

        /// <summary>
        /// очистка кэша тайлов
        /// </summary>
        public void ClearCacheTiles() {
            this.CancelPrevRequests();
            this._mapCacheLayer.Clear();
        }

        /// <summary>
        /// очистка из кэша тайлов, которые не имеют статус Success
        /// </summary>
        public void ClearCacheUnsussedTiles() {
            this._mapCacheLayer.ClearUnsussedTiles();
        }

        /// <summary>
        /// отмена все активных запросов за данными тайлов
        /// </summary>
        public void CancelPrevRequests() {
            lock (_sync) {
                foreach (MapTileDataClient dataCleint in this._mapTileDataClients) {
                    dataCleint.CancelAsync();
                }
                this._mapTileDataClients.Clear();
            }
        }
    }
}
