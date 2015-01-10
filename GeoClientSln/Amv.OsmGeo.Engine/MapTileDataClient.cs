using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.ComponentModel;
using Amv.Geo.Core;


namespace Amv.OsmGeo.HttpDataLayer
{
    /// <summary>
    /// обертка для класса WebClient
    /// </summary>
    public class WebClientWrapper:IDisposable
    {           
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        protected Uri _uri;
        protected WebClient _webClient;

        public WebClientWrapper(string url) {
            //проверяем url
            try {
                this._uri = new Uri(url);
            }
            catch {
                throw new ArgumentException(string.Format("Формат строки запроса не правильный:{0}", url));
            }
            this._webClient = new WebClient();
            this._webClient.Proxy = WebRequest.GetSystemWebProxy();
            this._webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
            this._webClient.UseDefaultCredentials = true;
            this._webClient.Headers.Add("User-Agent", @"Mozilla/4.0 (compatible; MSIE 10.0; Windows NT 5.2; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3; .NET4.0C; .NET4.0E)");
            this._webClient.DownloadProgressChanged+=_webClient_DownloadProgressChanged;
            
        }

        private void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            this.OnDownloadProgressChanged(e);
        }

        protected  void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e) {
            if (this.DownloadProgressChanged != null) {
                this.DownloadProgressChanged(this, e);
            }
        }

        public virtual void CancelAsync() {
            this._webClient.CancelAsync();
        }
     
        public virtual void Dispose() {
            this._webClient.DownloadProgressChanged -= this._webClient_DownloadProgressChanged;
            this._webClient.Dispose();

        }
    }

    public class DataWebClient : WebClientWrapper
    {
        public event DownloadDataCompletedEventHandler DownloadDataCompleted;  
        public DataWebClient(string url):base(url) {
            this._webClient.DownloadDataCompleted += _webClient_DownloadDataCompleted;
        }

        public override void Dispose() {
            this._webClient.DownloadDataCompleted -= this._webClient_DownloadDataCompleted;
            base.Dispose();
        }

        private void _webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            this.OnDownloadDataCompleted( e);
        }

        protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e){
            if (this.DownloadDataCompleted != null) this.DownloadDataCompleted(this, e);
        }
        /// <summary>
        /// загрузка бинарных данных в синхронном режиме cо скрытием ошибок
        /// </summary>
        /// <returns></returns>
        public byte[] TryDownloadData(out Exception e) {
            byte[] dnlData = null;
            e = null;
            try {
                dnlData = this.DownloadData();
            }
            catch (WebException we) {
                e = we;
            }
            catch (Exception ex) {
                e = ex;
            }
            return dnlData;
        }

        /// <summary>
        /// загрузка бинарных данных в синхронном режиме
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadData() {

            byte[] dnlData = this._webClient.DownloadData(this._uri);
            return dnlData;
        }

        /// <summary>
        /// загрузка бинарных данных в асинхронном режиме
        /// </summary>
        public void DownloadDataAsync() {
            this._webClient.DownloadDataAsync(this._uri);
        }
    }

    public class FileWebClient : WebClientWrapper
    {
        public event AsyncCompletedEventHandler DownloadFileCompleted;

        public FileWebClient(string url):base(url) {
            this._webClient.DownloadFileCompleted +=_webClient_DownloadFileCompleted;
        }

        public override void Dispose() {
            this._webClient.DownloadFileCompleted -= _webClient_DownloadFileCompleted;
            base.Dispose();
        }

        private void _webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            this.OnDownloadFileCompleted(e);
        }
        protected void OnDownloadFileCompleted(AsyncCompletedEventArgs e) {
            if (this.DownloadFileCompleted != null) {
                this.DownloadFileCompleted(this, e);
            }
            //base.OnDownloadFileCompleted(e);
        }

        public void TryDownloadFile(string filePath) {
            try {
                this.DownloadFile(filePath);
            }
            catch (WebException we) {
                var e = we;
            }
        }

        public void DownloadFile(string filePath) {

            this._webClient.DownloadFile(this._uri, filePath);

        }

        public void DownloadFileAsync(string filePath) {
            this._webClient.DownloadFileAsync(this._uri, filePath);
        }
    }

    public class ContentWebClient : WebClientWrapper
    {
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        public ContentWebClient(string url): base(url) {
            this._webClient.DownloadStringCompleted+=_webClient_DownloadStringCompleted;
        }

        public override void Dispose() {
            this._webClient.DownloadStringCompleted -= _webClient_DownloadStringCompleted;
            base.Dispose();
        }

        private void _webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
            this.OnDownloadStringCompleted(e);
            
        }

        protected void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e) {
            if (this.DownloadStringCompleted != null) {
                this._webClient.DownloadStringCompleted -= this._webClient_DownloadStringCompleted;
                this.DownloadStringCompleted(this, e);
            }
        }

        /// <summary>
        /// загрузка текстовых данных с указанием типа контента и кодировки
        /// в синхронном режиме
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public string TryDownloadContent(string contentType) {
            this._webClient.Headers.Add("Content-Type", contentType);
            string dnlString = null;
            try {
                dnlString = this.DownloadContent(contentType);
            }
            catch (WebException we) {
                var e = we;
            }
            return dnlString;
        }

        /// <summary>
        /// загрузка текстовых данных с указанием типа контента и кодировки
        /// в синхронном режиме
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public string DownloadContent(string contentType) {
            this._webClient.Headers.Add("Content-Type", contentType);

            string dnlString = this._webClient.DownloadString(this._uri);

            return dnlString;
        }

        /// <summary>
        /// загрузка текстовых данных в асинхронном режиме
        /// </summary>
        /// <param name="contentType"></param>
        public void DownloadContentAsync(string contentType) {
            this._webClient.Headers.Add("Content-Type", contentType);
            this._webClient.DownloadStringAsync(this._uri);
        }
        
        /// <summary>
        /// загрузка html кода в кодировке utf8 в синхронном режиме
        /// </summary>
        /// <returns></returns>
        public string DownloadHtmlUtf8() {
            return this.DownloadContent("text/html;charset=utf-8");
        }

        /// <summary>
        /// загрузка html кода в кодировке utf8 в асинхронном режиме
        /// </summary>
        public void DownloadHtmlAsyncUtf8() {
            this.DownloadContentAsync("text/html;charset=utf-8");
        }

        /// <summary>
        /// загрузка xml кода в кодировке utf8 в асинхронном режиме
        /// </summary>
        public void DownloadXmlAsyncUtf8() {
            this.DownloadContentAsync("text/xml;charset=utf-8");
        }

        /// <summary>
        /// загрузка xml кода в кодировке utf8 в синхронном режиме
        /// </summary>
        /// <returns></returns>
        public string DownloadXmlUtf8() {
            return this.DownloadContent("text/xml;charset=utf-8");
        }

    }

    public class MapTileDataClient : DataWebClient
    {
        public MapTileBase MapTile { get; private set; }
        public Action<MapTileBase> DataMapTileComplete { get; private set; }
        public MapTileDataClient(MapTileBase tile,Action<MapTileBase> dataMapTileComplete)
            : base(tile.TileUrl) {
                this.MapTile = tile;
                this.DataMapTileComplete = dataMapTileComplete;
        }

        protected override void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e) {
            
            try {
                if (e.Cancelled) {
                    this.MapTile.MapTileDataState = MapTileDataState.Cancel;
                    this.DataMapTileComplete(this.MapTile);
                }
                else {
                    this.MapTile.DataBinary = e.Result;
                    this.MapTile.MapTileDataState = MapTileDataState.Success;
                    this.DataMapTileComplete(this.MapTile);
                }
                //base.OnDownloadDataCompleted(e);
            }
            catch (Exception ex) {
                var exc = ex;
                this.MapTile.MapTileDataState = MapTileDataState.Error;
                this.DataMapTileComplete(this.MapTile);
            }
            finally {
                this.Dispose();
                
            }

        }
        
    }
}
