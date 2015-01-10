using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Amv.Geo.Core;
using System.Runtime.Serialization.Json;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// парсер xml данных полученных от сервиса Nominatim
    /// </summary>
    public class OsmNominatimGeoLocationsParser:IGeoLocationParser
    {
        /// <summary>
        /// парсим xml данные геолокации
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable<GeoLocationBase> ParseXmlGeoData(string xml) {

            XElement xmlGeo = XElement.Parse(xml);
            var cs = from c in xmlGeo.Elements("place")
                     select c;
            List<OsmGeoLocation> geoLocations = new List<OsmGeoLocation>();
            foreach (var item in cs) {
               OsmGeoLocation geo= this.ParseLocation(item);
               geoLocations.Add(geo);
            }
            return geoLocations;
        }

        #region Helpers

        private OsmGeoLocation ParseLocation (XElement locationElement) {
            OsmGeoLocation locationInfo = new OsmGeoLocation();
            foreach (var attr in locationElement.Attributes()) {
                if (!string.IsNullOrWhiteSpace(attr.Value)) {
                    this.parseLocationAttributte(locationInfo, attr);
                }
            }
            this.parseLocationChildsNodes(locationInfo, locationElement);
            return locationInfo;
        }

        private void parseLocationAttributte(OsmGeoLocation locationInfo, XAttribute attr) {
            if (attr.Name == "place_id") {
                locationInfo.ID = attr.Value;
            }
            if (attr.Name == "osm_type") {
                locationInfo.OsmType = attr.Value;
            }
            if (attr.Name == "osm_id") {
                locationInfo.OsmID = attr.Value;
            }
            if (attr.Name == "place_rank") {
                locationInfo.Rank = Convert.ToInt32(attr.Value);
            }
            if (attr.Name == "boundingbox") {
                //TODO:отпарсить boundingbox. 
               

            }
            if (attr.Name == "polygonpoints") {
                //получаем 
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(List<float[]>));
                string s = attr.Value.Replace("\"", "");

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(attr.Value))) {
                    //ms.
                    List<float[]> poligonPoints = (List<float[]>)dcjs.ReadObject(ms);
                    foreach (float[] point in poligonPoints) {
                        locationInfo.GeoPoligon.Add(new LatLng(point[1],point[0]));
                    }

                }
            }
            if (attr.Name == "lat") {
                locationInfo.LatLng.Lat = Convert.ToDouble(attr.Value.Replace('.', ','));
            }
            if (attr.Name == "lon") {
                locationInfo.LatLng.Lng = Convert.ToDouble(attr.Value.Replace('.', ','));
            }
            if (attr.Name == "display_name") {
                locationInfo.DisplayName = attr.Value;
            }
            if (attr.Name == "class") {
                locationInfo.Class = attr.Value;
            }
            if (attr.Name == "type") {
                locationInfo.Type = attr.Value;
            }
            if (attr.Name == "importance") {
                locationInfo.Importance = Convert.ToSingle(attr.Value.Replace('.', ','));
            }
            if (attr.Name == "icon") {
                locationInfo.IconUrl = attr.Value;
            }
        }

        private void parseLocationChildsNodes(OsmGeoLocation locationInfo, XElement locationContainer) {
            foreach (var childNode in locationContainer.Elements()) {
                //TODO:отпарсить подчинненные ноды
            }
        }

        #endregion
    }
}
