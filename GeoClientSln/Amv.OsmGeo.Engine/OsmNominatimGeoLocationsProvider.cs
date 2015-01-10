using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amv.Geo.Core;
using System.Web;
using System.Net;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// поставщик данных по геолокациям от сервиса Nominatim
    /// </summary>
    public class OsmNominatimGeoLocationsProvider:IGeoLocationProvider
    {
        private const string URL_SEARCH_TEMPLATE = "http://nominatim.openstreetmap.org/search?q={0}&format=xml&polygon=1&addressdetails=1";

        /// <summary>
        /// используемый парсер xml данных
        /// </summary>
        private IGeoLocationParser _geoParser;
        /// <summary>
        /// используемый загрузчик данных по геолокации
        /// </summary>
        private OsmGeoLocationsInfoClient _contentWebClient;
 
        /// <summary>
        /// конструктор
        /// </summary>
        public OsmNominatimGeoLocationsProvider() {
            this._geoParser = new OsmNominatimGeoLocationsParser();
        }

        /// <summary>
        /// получение доступных мест геолокации в синхронном режиме
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public IEnumerable<GeoLocationBase> GetAvialableGeoLocations(string searchQuery) {
          
            ContentWebClient cwc = new ContentWebClient(this.prepareSearchUrl(searchQuery));
            string xml = cwc.DownloadXmlUtf8();
            IEnumerable<GeoLocationBase> geoLocations = this._geoParser.ParseXmlGeoData(xml);
            return geoLocations;
        }

        /// <summary>
        /// получение данных геолокации в асинхронном режиме
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="completeAction"></param>
        public void GetAvialableGeoLocationsAsync(string searchQuery,Action<IEnumerable<GeoLocationBase>,Exception> completeAction) {
            if (this._contentWebClient != null) return;
            this._contentWebClient = new OsmGeoLocationsInfoClient(this.prepareSearchUrl(searchQuery),completeAction);
            this._contentWebClient.DownloadXmlAsyncUtf8();
            this._contentWebClient.DownloadStringCompleted += cwc_DownloadStringCompleted;
        }

        /// <summary>
        /// отмена асинхронного запроса для получения данных геолокации
        /// </summary>
        public void CancelSearchAsync() {
            if (this._contentWebClient != null) {
                this._contentWebClient.CancelAsync();
            }
        }

        /// <summary>
        /// обработка получения данных геолокации в асинх режиме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cwc_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e) {
            OsmGeoLocationsInfoClient osmGeoClient = sender as OsmGeoLocationsInfoClient;
            try {
                if (!e.Cancelled) {
                    string xml = e.Result;
                    //парсим данные
                    IEnumerable<GeoLocationBase> geoLocations = this._geoParser.ParseXmlGeoData(xml);
                    osmGeoClient.CompleteAction(geoLocations,null);
                }
            }
            catch (Exception ex) {
                osmGeoClient.CompleteAction(null, ex);
            }
            finally {
                osmGeoClient.Dispose();
                this._contentWebClient = null;
            }

           // return geoLocations.Cast<T>();
        }

        /// <summary>
        /// подготовка строки запроса для url
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string prepareSearchUrl(string query) {
            query = HttpUtility.UrlEncode(query);
            return string.Format(URL_SEARCH_TEMPLATE, query);
        }


        
    }
}
