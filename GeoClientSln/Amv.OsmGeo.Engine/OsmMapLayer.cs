﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amv.Geo.Core;
using System.Drawing;
using Amv.OsmGeo.MapLayer.Utils;

namespace Amv.OsmGeo.MapLayer
{
    /// <summary>
    /// менеджер для работы с системой osm
    /// </summary>
    public class OsmMapLayer:IGeoMapLayer
    {
        private const int TILE_SIZE = 256;

        /// <summary>
        /// получение всех тайлов которые необходимы для отображения карты
        /// </summary>
        /// <param name="mapPaneBounds"></param>
        /// <param name="zoom"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public virtual IEnumerable<MapTileBase> GetMapTilesForPlaceInfo(Rectangle mapPaneBounds, int zoom, double lat, double lng) {
            //коллекция тайлов
            List<MapTileBase> tilesForRequest = new List<MapTileBase>();
            //получаем заданную точку в координатах osm
            PointD osmPointFromLatLng = LatLngAndOsmPointConverter.ConvertLatLngToOsmPoint(lat, lng, zoom);
            //получаем координаты центра панели карты
            Point centerMapPanePoint=this.getPointOfCenterMapPaneBounds(mapPaneBounds);
            //тайл который будет располагаться в центре панели карты.
            OsmMapTile centerTile = new OsmMapTile(zoom);
            //координаты центрального тайла, для загрузки изображения.
            centerTile.TileCoords = new Point((int)Math.Floor(osmPointFromLatLng.X / TILE_SIZE), (int)Math.Floor(osmPointFromLatLng.Y / TILE_SIZE));
            //получаем смещение от центра 
            Point osmCoordCenterTile = new Point((centerTile.TileCoords.X * TILE_SIZE)+(TILE_SIZE/2),( centerTile.TileCoords.Y * TILE_SIZE)+(TILE_SIZE/2));
            //смещение заданной точки от центра тайла.
            Size offsetCenterTile = new Size((int)Math.Floor(osmCoordCenterTile.X - osmPointFromLatLng.X), (int)Math.Floor(osmCoordCenterTile.Y - osmPointFromLatLng.Y));
            //расчитываем координаты на панели, чтобы заданная точка в широте и долготе оказалась в центре панели
            centerTile.AppPaneCoords = new Point(
                centerMapPanePoint.X-(TILE_SIZE/2)+offsetCenterTile.Width,
                centerMapPanePoint.Y-(TILE_SIZE/2)+offsetCenterTile.Height);
            //получаем необходимое количество тайлов по черырем сторонам относительно центрального тайла 
            int countTilesLeft=0;
            int countTilesTop=0;
            int countTilesRight=0;
            int countTilesBottom=0;
            this.getCountTilesForMapPane(centerTile.AppPaneBounds, mapPaneBounds, out countTilesTop, out countTilesLeft, out countTilesBottom, out countTilesRight);
            //получаем общее количество тайлов по горизонтали
            int countTilesForWidth = countTilesLeft + countTilesRight + 1;
            //получаем общее количество тайлов по вертикали
            int countTilesForHeight = countTilesTop + countTilesBottom + 1;
            //получаем начальную osm координату по оси X
            int startTileOsmX = centerTile.TileCoords.X - countTilesLeft;
            //получаем начальную osm координату по оси Y
            int startTileOsmY = centerTile.TileCoords.Y - countTilesTop;
            //получаем начальную координату в панели карты по оси X
            int startTileAppX = centerTile.AppPaneCoords.X - (countTilesLeft * TILE_SIZE);
            //получаем начальную координату в панели карты по оси Y
            int startTileAppY = centerTile.AppPaneCoords.Y - (countTilesTop * TILE_SIZE);
            //раставляем тайлы по координатам osm и координатам панели карты.
            tilesForRequest.Clear();
            for (int j = 0; j < countTilesForHeight; j++) {
                for (int i = 0; i < countTilesForWidth; i++) {
                    OsmMapTile tile = new OsmMapTile(zoom);
                    tile.TileCoords = new Point(startTileOsmX+i,startTileOsmY+j);
                    tile.AppPaneCoords = new Point(startTileAppX + (i * TILE_SIZE), startTileAppY + (j * TILE_SIZE));
                    tilesForRequest.Add(tile);
                }
            }
            return tilesForRequest;
        }

        /// <summary>
        /// получение всех тайлов которые необходимы для отображения карты
        /// </summary>
        /// <param name="mapPaneBounds"></param>
        /// <param name="zoom"></param>
        /// <param name="centerLatLng"></param>
        /// <returns></returns>
        public virtual IEnumerable<MapTileBase> GetMapTilesForPlaceInfo(Rectangle mapPaneBounds, int zoom, LatLng centerLatLng) {
            return this.GetMapTilesForPlaceInfo(mapPaneBounds, zoom, centerLatLng.Lat, centerLatLng.Lng);
        }

        /// <summary>
        /// получение широты и долготы точки от базовой широты и долготы относительно смещения панели карты
        /// </summary>
        /// <param name="moveSize"></param>
        /// <param name="zoom"></param>
        /// <param name="baseLat"></param>
        /// <param name="baseLng"></param>
        /// <returns></returns>
        public virtual LatLng GetLatLngFromBaseLatLng(Size moveSize, int zoom, double baseLat, double baseLng) {
            //получаем заданную точку в координатах osm
            PointD osmPointFromLatLng = LatLngAndOsmPointConverter.ConvertLatLngToOsmPoint(baseLat,baseLng, zoom);
            PointD osmPointFromLatLngFromMoveSize = new PointD(osmPointFromLatLng.X - moveSize.Width, osmPointFromLatLng.Y - moveSize.Height);
            return LatLngAndOsmPointConverter.ConverOsmPointInLatLng(osmPointFromLatLngFromMoveSize,zoom);
        }

        /// <summary>
        /// получение широты и долготы точки от базовой широты и долготы относительно смещения панели карты
        /// </summary>
        /// <param name="moveSize"></param>
        /// <param name="zoom"></param>
        /// <param name="baseLat"></param>
        /// <param name="baseLng"></param>
        /// <returns></returns>
        public virtual LatLng GetLatLngFromBaseLatLng(Size moveSize, int zoom, LatLng baseLatLng) {
            return this.GetLatLngFromBaseLatLng(moveSize, zoom, baseLatLng.Lat, baseLatLng.Lng);
        }

        
        /// <summary>
        /// получение координат на панели приложения
        /// </summary>
        /// <param name="mapPanеBounds"></param>
        /// <param name="zoom"></param>
        /// <param name="currentLat"></param>
        /// <param name="currentLng"></param>
        /// <param name="baseLat"></param>
        /// <param name="baseLng"></param>
        /// <returns></returns>
        public virtual Point GetLocationInMapPaneFromLatLng(Rectangle mapPanеBounds, int zoom, double currentLat, double currentLng,double baseLat,double baseLng) {
            //получение координат приложения на панели карты от широты и долготы точки относительно базовой широты и долготы.
            //получаем базовую точку в координатах osm от базовых широты и долготы 
            //(подразумевается, что базовые координаты (широта и долгота) расположены в центре панели карты.
            PointD osmBasePointFromLatLng = LatLngAndOsmPointConverter.ConvertLatLngToOsmPoint(baseLat, baseLng, zoom);
            //получаем заданную точку в координатах osm 
            PointD osmCurrentPointFromLatLng = LatLngAndOsmPointConverter.ConvertLatLngToOsmPoint(currentLat,currentLng, zoom);
            //получаем смещение в пикселях относительно базовой координаты
            Size offsetCurrentOsmFromBase = new Size((int)Math.Floor(osmBasePointFromLatLng.X - osmCurrentPointFromLatLng.X),
                (int)Math.Floor( osmBasePointFromLatLng.Y - osmCurrentPointFromLatLng.Y));
            //получаем координаты центра панели карты
            Point centerMapPanePoint = this.getPointOfCenterMapPaneBounds(mapPanеBounds);
            //получаем координаты точки на панели карты
            Point p = new Point(centerMapPanePoint.X - offsetCurrentOsmFromBase.Width, centerMapPanePoint.Y - offsetCurrentOsmFromBase.Height);
            return p;
        }

        /// <summary>
        /// получение координат на панели приложения
        /// </summary>
        /// <param name="mapPanBounds"></param>
        /// <param name="zoom"></param>
        /// <param name="currentLat"></param>
        /// <param name="currentLng"></param>
        /// <param name="baseLatLng"></param>
        /// <returns></returns>
        public virtual Point GetLocationInMapPaneFromLatLng(Rectangle mapPanBounds, int zoom, double currentLat, double currentLng, LatLng baseLatLng) {
            return this.GetLocationInMapPaneFromLatLng(mapPanBounds, zoom, currentLat, currentLng, baseLatLng.Lat, baseLatLng.Lng);
        }

        /// <summary>
        /// получение координат на панели приложения
        /// </summary>
        /// <param name="mapPanBounds"></param>
        /// <param name="zoom"></param>
        /// <param name="currentLatLng"></param>
        /// <param name="baseLatLng"></param>
        /// <returns></returns>
        public virtual Point GetLocationInMapPaneFromLatLng(Rectangle mapPanBounds, int zoom, LatLng currentLatLng, LatLng baseLatLng) {
            return this.GetLocationInMapPaneFromLatLng(mapPanBounds,  zoom, currentLatLng.Lat, currentLatLng.Lng, baseLatLng);
        }

        #region Helpers
        /// <summary>
        /// получение количества тайлов относительно центрального тайла для заполнения панели карты
        /// </summary>
        /// <param name="centerTileBounds"></param>
        /// <param name="mapPaneBounds"></param>
        /// <param name="countTilesTop"></param>
        /// <param name="countTilesLeft"></param>
        /// <param name="countTilesBottom"></param>
        /// <param name="countTilesRight"></param>
        private void getCountTilesForMapPane(Rectangle centerTileBounds, Rectangle mapPaneBounds, out int countTilesTop, out int countTilesLeft, out int countTilesBottom, out int countTilesRight) {
           countTilesLeft = 0;
            countTilesTop = 0;
            countTilesRight = 0;
            countTilesBottom = 0;
            
            //определяем количество влево.
            int currentPoint = centerTileBounds.Left;
            while (currentPoint > mapPaneBounds.Left) {
                countTilesLeft++;
                currentPoint -= TILE_SIZE;
            }
            //определяем количество вверх
            currentPoint = centerTileBounds.Top;
            while (currentPoint > mapPaneBounds.Top) {
                countTilesTop++;
                currentPoint -= TILE_SIZE;
            }
            //определяем количесвто вправо
            currentPoint = centerTileBounds.Right;
            while (currentPoint < mapPaneBounds.Right) {
                countTilesRight++;
                currentPoint += TILE_SIZE;
            }
            //опеределяем количество вниз
            currentPoint = centerTileBounds.Bottom;
            while (currentPoint < mapPaneBounds.Bottom) {
                countTilesBottom++;
                currentPoint += TILE_SIZE;
            }
        }
        /// <summary>
        /// получаем координаты центра области панели с картой
        /// </summary>
        /// <param name="mapPaneBounds"></param>
        /// <returns></returns>
        protected virtual Point getPointOfCenterMapPaneBounds(Rectangle mapPaneBounds) {
            return new Point(mapPaneBounds.Left + (mapPaneBounds.Width / 2), mapPaneBounds.Top + (mapPaneBounds.Height / 2));
        }

        /// <summary>
        /// получаем координаты верхнего левого угла панели с картой
        /// </summary>
        /// <param name="mapPaneBounds"></param>
        /// <returns></returns>
        protected virtual Point getLeftTopMapPaneBounds(Rectangle mapPaneBounds) {
            return mapPaneBounds.Location;
        }

        #endregion



        

        

        
    }
}
