using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amv.Geo.Core;

namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// обертка для web client для получения данных геолокации
    /// </summary>
    public class OsmGeoLocationsInfoClient:ContentWebClient
    {
        /// <summary>
        /// обратная акшион при асинхронной загрузке
        /// </summary>
        public Action<IEnumerable<GeoLocationBase>,Exception> CompleteAction { get; private set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="url"></param>
        /// <param name="completeAction"></param>
        public OsmGeoLocationsInfoClient(string url,Action<IEnumerable<GeoLocationBase>,Exception> completeAction)
            : base(url) {
                this.CompleteAction = completeAction;
        }
    }
}
