using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amv.Geo.Core
{
    /// <summary>
    /// парсер информации о местах 
    /// </summary>
    public interface IGeoLocationParser
    {
        /// <summary>
        /// парсинг xml данных
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IEnumerable<GeoLocationBase> ParseXmlGeoData(string xml);
    }
}
