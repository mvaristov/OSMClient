using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Amv.Geo.Core
{
    /// <summary>
    /// интерфейс для поставщика информации по картам.
    /// </summary>
    public interface IGeoMapLayer
    {
        /// <summary>
        /// получение коллекции тайлов карты для их загрузки и отображения
        /// </summary>
        /// <param name="mapPaneBounds">область панели, где должна отобразиться карта</param>
        /// <param name="zoom">масштаб отображемой карты</param>
        /// <param name="lat">широта отображаемого места</param>
        /// <param name="lng">долгота</param>
        /// <returns></returns>
        IEnumerable<MapTileBase> GetMapTilesForPlaceInfo(Rectangle mapPaneBounds, int zoom, double lat, double lng);

        /// <summary>
        /// получение коллекции тайлов карты для их загрузки и отображения
        /// </summary>
        /// <param name="mapPaneBounds">область панели, где должна отобразиться карта</param>
        /// <param name="zoom">масштаб отображемой карты</param>
        /// <param name="centerLatLng">широта, долгота отображаемого места</param>
        /// <param name="lng"></param>
        /// <returns></returns>
        IEnumerable<MapTileBase> GetMapTilesForPlaceInfo(Rectangle mapPaneBounds, int zoom,LatLng centerLatLng);

        /// <summary>
        /// получение координаты точки на панели где отрисовывается карта от заданных широты и долготы.
        /// </summary>
        /// <param name="mapPanBounds">область панели, где должна отобразиться карта</param>
        /// <param name="lat">масштаб</param>
        /// <param name="lng">широта</param>
        /// <param name="zoom">долгота</param>
        /// <returns></returns>
        Point GetLocationInMapPaneFromLatLng(Rectangle mapPanBounds, int zoom, double currentLat, double currentLng, double baseLat, double baseLng);

        /// <summary>
        /// получение координаты точки на панели где отрисовывается карта от заданных широты и долготы.
        /// </summary>
        /// <param name="mapPanBounds">область панели, где должна отобразиться карта</param>
        /// <param name="lat">масштаб</param>
        /// <param name="lng">широта</param>
        /// <param name="zoom">долгота</param>
        /// <returns></returns>
        Point GetLocationInMapPaneFromLatLng(Rectangle mapPanBounds,  int zoom, double currentLat, double currentLng, LatLng baseLatLng);
        /// <summary>
        /// получение координаты точки на панели где отрисовывается карта от заданных широты и долготы.
        /// </summary>
        /// <param name="mapPanBounds">область панели, где должна отобразиться карта</param>
        /// <param name="lat">масштаб</param>
        /// <param name="lng">широта</param>
        /// <param name="zoom">долгота</param>
        /// <returns></returns>
        Point GetLocationInMapPaneFromLatLng(Rectangle mapPanBounds, int zoom, LatLng currentLatLng, LatLng baseLatLng);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSize"></param>
        /// <param name="zoom"></param>
        /// <param name="baseLat"></param>
        /// <param name="baseLng"></param>
        /// <returns></returns>
        LatLng GetLatLngFromBaseLatLng(Size moveSize, int zoom, double baseLat, double baseLng);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSize"></param>
        /// <param name="zoom"></param>
        /// <param name="baseLat"></param>
        /// <param name="baseLng"></param>
        /// <returns></returns>
        LatLng GetLatLngFromBaseLatLng(Size moveSize, int zoom,LatLng baseLatLng);
    }
}
