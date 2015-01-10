using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// интерфейс который должен реализовать провайдер предоставляющий данные по местоположениям
    /// </summary>
    public interface IGeoLocationProvider
    {
        /// <summary>
        /// получение всех доступных мест геолокации по запросу в синхронном режиме
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        IEnumerable<GeoLocationBase> GetAvialableGeoLocations(string searchQuery);

        /// <summary>
        /// отмена асинхронного запроса получения данных геолокации
        /// </summary>
        void CancelSearchAsync();

        /// <summary>
        /// получение мест геолокации в асинхронном режиме
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="completeAction"></param>
        void GetAvialableGeoLocationsAsync(string searchQuery,Action<IEnumerable<GeoLocationBase>,Exception> completeAction) ;
    }
}
