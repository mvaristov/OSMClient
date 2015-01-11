using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Amv.Geo.Core;
using Amv.OsmGeo.MapLayer;
using Amv.OsmGeo.HttpDataLayer;
using Amv.GeoClient.WinForms.Properties;
using System.Net;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Amv.GeoClient.WinForms
{
    /// <summary>
    /// основная форма приложения (угааа)
    /// </summary>
    public partial class MainForm : Form
    {
        private object _sync = new object();
        /// <summary>
        /// поставщик работы с картой геолокации
        /// </summary>
        private IGeoMapLayer _geoMapLayer;
        /// <summary>
        /// поставщик получения данных для отображения карты
        /// </summary>
        private IGeoDataLayer _geoDataLayer; 
        /// <summary>
        /// поставщик получения доступных местоположений
        /// </summary>
        private IGeoLocationProvider _locationProvider;
        /// <summary>
        /// помощник при перетаскивании карты.
        /// </summary>
        private MapPaneMoveHelper _movePaneHelper;
        /// <summary>
        /// выбранное в списке место для отображения на карте
        /// </summary>
        private GeoLocationBase _selectedPlaceInfo;
        /// <summary>
        /// начальные координаты, которые отображались в центре карты. Используются для передвижения карты.
        /// </summary>
        private LatLng _startMoveLatLng;
        /// <summary>
        /// текущая широта и долгота, отображаемая в центре картыи
        /// </summary>
        private LatLng _currentLatLng;
        /// <summary>
        /// невидимое положение панели информации о местоположения 
        /// </summary>
        private Point _unvisibleLocationMessagePanel = new Point(-1000, -1000);
      
        /// <summary>
        /// конструктор формы
        /// </summary>
        public MainForm() {
          
            InitializeComponent();
            //определяем основные службы для работы с геолокацией
            //поставщик информации доступных мест на карте по строке запроса.
            this._locationProvider = new OsmNominatimGeoLocationsProvider();
            //реализует бизнес слой для получения тайлов запрашиваемой карты
            this._geoMapLayer =new YandexMapLayer();//new OsmMapLayer();
            //слой получения данных для отрисовки карты
            this._geoDataLayer = new HttpDataLayer();
            //помощник перетаскивания карты
            this._movePaneHelper = new MapPaneMoveHelper();
            //скрываем панель информации.
            this.pnlLocationInfo.Location = this._unvisibleLocationMessagePanel;
            

#if DEBUG 
           this.txtSearchLoaction.Text = "Череповец";
#endif

        }

        /// <summary>
        /// обработка события кнопки поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e) {
            string query = this.txtSearchLoaction.Text;
            if (string.IsNullOrWhiteSpace(query)) {
                MessageBox.Show("Введите название объекта поиска", "Внимание",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
                this.progressBar1.Style = ProgressBarStyle.Marquee;
                this.progressBar1.Visible = true;
                this.btnCancelSearch.Visible = true;
                this.btnSearch.Visible = false;
               this._locationProvider.GetAvialableGeoLocationsAsync(query, (geoLocations,error) => {

                   //выполняется после загрузки
                   this.lviAvialableLocations.Items.Clear();
                   this.progressBar1.Style = ProgressBarStyle.Blocks;
                   this.btnCancelSearch.Visible = false;
                   this.btnSearch.Visible = true;
                   this.progressBar1.Visible = false;
                   //проверяем статус загрузки.
                   if (error != null) {
                       //при получении данных произошли ошибки.
                       Exception innerEX = error.InnerException as WebException;
                       string messageError = String.Empty;
                       if (innerEX != null) {
                           if (innerEX is WebException) {
                               messageError = string.Format("Ошибка доступа к сервису геолокации.\r\n{0}", innerEX.Message);
                           }
                           else {
                               messageError = string.Format("Внутренняя получения данных геолокации.\r\n{0}", innerEX.Message);
                           }
                       }
                       else {
                           messageError = string.Format("Ошибка получения данных геолокации.\r\n{0}",error.Message);

                       }
                       MessageBox.Show(messageError, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       return;
                   }
                   if (geoLocations.Count() == 0) {
                       MessageBox.Show("Ничего не найдено. Измените название объекта поиска", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       return;
                   }
                   //здесь значит загрузили какие - то данные
                   foreach (GeoLocationBase pi in geoLocations) {
                       //заполняем список
                       ListViewItem lvi = new ListViewItem(pi.DisplayName);
                       lvi.ToolTipText = pi.DisplayName;
                       lvi.SubItems.Add(pi.Type);
                       lvi.SubItems.Add(pi.Class);
                       lvi.Tag = pi;
                       this.lviAvialableLocations.Items.Add(lvi);
                   }
                });
               
        }
        /// <summary>
        /// обрабтка события кнопки отмена поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSearch_Click(object sender, EventArgs e) {
            this._locationProvider.CancelSearchAsync();
            this.progressBar1.Style = ProgressBarStyle.Blocks;
            this.progressBar1.Visible = false;
            this.btnSearch.Visible = true;
            this.btnCancelSearch.Visible = false;

        }

        /// <summary>
        /// обработка события нажатия кнопки enter в поле поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchLoaction_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                this.btnSearch_Click(this.btnSearch, new EventArgs());
            }
        }
       
        /// <summary>
        /// обработка события выделения места геолокации в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lviAvialableLocations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected) {
                //рисуем карту.
                this._selectedPlaceInfo = e.Item.Tag as GeoLocationBase; 
                this._movePaneHelper.ResetMove();
                if (this._selectedPlaceInfo != null) {
                    //запоминаем текущие широту и долготу
                    this._currentLatLng = new LatLng(this._selectedPlaceInfo.LatLng.Lat, this._selectedPlaceInfo.LatLng.Lng);
                    //определяем начальный масштаб
                    this._selectedPlaceInfo.CalcInitZoom();
                    //сбрасываем кешированные тайлы
                    this._geoDataLayer.ClearCacheTiles();
                    //инициализируем панель информации
                    this.pnlLocationInfo.Message = string.Format("{0}\r\n[{1}-{2}]", this._selectedPlaceInfo.DisplayName, this._selectedPlaceInfo.LatLng.Lat, this._selectedPlaceInfo.LatLng.Lng);
                    this.pnlLocationInfo.Location = this._unvisibleLocationMessagePanel;
                    this.pnlLocationInfo.Visible = true;    
                }
                //перерисовываем карту, через событие OnPaint
                this.pnlMap.Invalidate();
            }
        }

        /// <summary>
        /// очистка панели карты
        /// </summary>
        private void ClearPaneMap() {
            using (Graphics gr = this.pnlMapBackground.CreateGraphics()) {
                gr.Clear(this.pnlMapBackground.BackColor);
            }
        }


        private Point _selectedPlacePanePoint;
        private GeoLocatinMarker _selectedPlacePaneMarker;
        private FullDrawMapMarker _fullMapMarker;
        private CurrentLatLngMapMarker _centerLatLngMarker;
        private GeoLocationPoligon _selectedPlacePoligon;
        private PanelInfoMapMarker _selectedPlaceInfoMarker;
        /// <summary>
        /// создаем карту
        /// </summary>
        private void CreateMap() {
            if (this._selectedPlaceInfo == null) return;
            //расчет необходимых тайлов для отображения карты
            IEnumerable<MapTileBase> mapTiles = this._geoMapLayer.GetMapTilesForPlaceInfo(this.pnlMap.ClientRectangle,this._selectedPlaceInfo.Zoom,
                this._currentLatLng);
            //Debug.Assert(false, "Create map");
            Debug.WriteLine(string.Format("map:{0}-{1}",this._currentLatLng.Lat,this._currentLatLng.Lng));
            //определяем координаты заданного места для панели карты
            this._selectedPlacePanePoint = this._geoMapLayer.GetLocationInMapPaneFromLatLng(this.pnlMap.ClientRectangle,this._selectedPlaceInfo.Zoom,
                this._selectedPlaceInfo.LatLng, this._currentLatLng);
            //определяем маркеры которые будут отрисованы на карте
            this._selectedPlacePaneMarker = new GeoLocatinMarker(this._selectedPlacePanePoint, Resources.marker_icon.Size);
            this._selectedPlaceInfoMarker = new PanelInfoMapMarker(this._selectedPlacePanePoint, this.pnlLocationInfo.Size);
            this._fullMapMarker = new FullDrawMapMarker(new Point(0, 0), this.pnlMap.Size);
            this._centerLatLngMarker = new CurrentLatLngMapMarker(this.pnlMap.ClientRectangle.LeftBottom(),new Size(this.pnlMap.Width,15),this._currentLatLng);
            this._selectedPlacePoligon = new GeoLocationPoligon();
            this._selectedPlacePoligon.CalcAppPointFromLatLng(this._geoMapLayer, this.pnlMap.ClientRectangle, this._selectedPlaceInfo.Zoom, this._selectedPlaceInfo.GeoPoligon, this._currentLatLng);
            Graphics graphics = null;
            //загрузка данных для тайлов.
            this._geoDataLayer.GetMapTilesDataAsync(mapTiles, (tile) => {
                this.Invoke((Action)(() => {
                    //создаем графикс 
                    if (graphics == null) {
                        // Использую объект графикс через CreateGraphics.
                        //пробовал через OnPaint и Invalidate. Сильно все моргает при перетаскивании.
                        //использование признака DoubleBuffer не помогает
                        graphics = this.pnlMap.CreateGraphics();
                    }    
                    //отрисовываем тайл на панели карты
                    this.drawTile(tile,graphics);
                    if (tile.MapTileDataState == MapTileDataState.Success) {
                        //отрисовываем маркеры на карте, после того, как отрисуются содержащие его тайлы.
                        if (!this._movePaneHelper.IsPaneMove) {
                            //проверяем, что можно рисовать маркеры
                            if (_selectedPlacePoligon.ValidForDraw(tile.AppPaneBounds)) {
                                this.drawLocationPoligon(_selectedPlacePoligon.AppMapPanePoints, graphics);
                            }
                            if (_selectedPlacePaneMarker.ValidForDraw(tile.AppPaneBounds)) {
                                this.drawMarker(_selectedPlacePaneMarker.AppMarkerBounds, graphics);
                            }
                            if (_selectedPlaceInfoMarker.ValidForDraw(tile.AppPaneBounds)) {
                                this.drawInfoPanel(_selectedPlaceInfoMarker.AppMarkerBounds);
                            }

                        }
                        else {
                            //скрываем панель информации о местоположении при перетаскивании
                            this.pnlLocationInfo.Location = this._unvisibleLocationMessagePanel;
                        }
                        if (_centerLatLngMarker.ValidForDraw(tile.AppPaneBounds)) {
                            //рисуем текущую широту и долготу 
                            this.drawCurrentBaseLatLng(_centerLatLngMarker.BuildLatLngString(), _centerLatLngMarker.AppMarkerBounds, graphics);
                        }

                        if (_fullMapMarker.ValidForDraw(tile.AppPaneBounds)) {    
                            //если здесь, то отрисовались все тайлы, уничтожаем графикс
                            if (graphics != null) {
                                graphics.Dispose();
                                graphics = null;
                            }
                        }
                    }
                }));
            });      
        }
      
        /// <summary>
        /// отрисовываем тайл
        /// </summary>
        /// <param name="tile"></param>
        private void drawTile(MapTileBase tile, Graphics graphics) {
            if (tile.MapTileDataState == MapTileDataState.Success) {
                
                using (MemoryStream ms = new MemoryStream(tile.DataBinary)) {
                    using (Bitmap btmTile = new Bitmap(ms)) {
                        graphics.DrawImageUnscaled(btmTile, tile.AppPaneCoords);
                        graphics.DrawRectangle(new Pen(Color.Black), tile.AppPaneBounds);
                        graphics.DrawString(string.Format("{0}/{1}",tile.TileCoords.X, tile.TileCoords.Y), this.Font, Brushes.Black, tile.AppPaneCoords);
                    }
                }
            }
            else {
                graphics.FillRectangle(new SolidBrush(this.pnlMap.BackColor), tile.AppPaneBounds);
                if (tile.MapTileDataState != MapTileDataState.Cancel) {
                    StringFormat sf = new StringFormat(StringFormatFlags.LineLimit);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    string text = (tile.MapTileDataState == MapTileDataState.Downloading) ? "загрузка..." : "ошибка загрузки...";
                    graphics.DrawString(text, this.Font, Brushes.Black, tile.AppPaneBounds, sf);
                }
            }
        }
    
        /// <summary>
        /// рисуем текущие координаты, которые должны быть в центре карты
        /// </summary>
        /// <param name="baseLatLngStr"></param>
        /// <param name="bounds"></param>
        /// <param name="graphics"></param>
        private void drawCurrentBaseLatLng(string baseLatLngStr,Rectangle bounds,Graphics graphics) {
            graphics.DrawString(baseLatLngStr, this.pnlMap.Font, Brushes.Black, bounds.Location);
        }

        /// <summary>
        /// отрисовываем маркер
        /// </summary>
        /// <param name="boundsMarker"></param>
        private void drawMarker(Rectangle boundsMarker,Graphics graphics) {      
            graphics.DrawImageUnscaled(Resources.marker_icon, boundsMarker);
        }

        /// <summary>
        /// отрисовываем панель информации о показываемом на карте место положении
        /// </summary>
        /// <param name="infoPanelBounds"></param>
        public void drawInfoPanel(Rectangle infoPanelBounds) {
            //TODO: реализовать отрисовку панели информации на карте, через графикс   
            this.pnlLocationInfo.Location = infoPanelBounds.Location;
        }

        /// <summary>
        /// рисуем очертания объекта геолокации, если заданы
        /// </summary>
        /// <param name="points"></param>
        /// <param name="graphics"></param>
        public void drawLocationPoligon(List<Point> points, Graphics graphics) {
            if (points != null && points.Count > 2) {
                graphics.DrawLines(new Pen(Color.Orange,3), points.ToArray());
            }
        }

        /// <summary>
        /// общие мероприятия при измении масштаба карты
        /// </summary>
        private bool preZooming() {
            if (this._selectedPlaceInfo == null) return false;
            //сбрасываем помощника перетаскивания карты
            this._movePaneHelper.ResetMove();
            //очищаем кэш тайлов
            //this._geoDataLayer.ClearCacheTiles();
            //чистим панель карты от предыдущих отрисованных тайлов
            //this.ClearPaneMap();
            return true;
        }
        /// <summary>
        /// обработка события увеличения масштаба карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoomUp_Click(object sender, EventArgs e) {
            if (this.preZooming()) {
                //увеличиваем значения зума на 1.
                this._selectedPlaceInfo.Zoom++;
                this.lblPlaceDisplayName.Text = this._selectedPlaceInfo.Zoom.ToString();
                //заново создаем карту
                //this.CreateMap();
                this.pnlMap.Invalidate();
            }
        }

        /// <summary>
        /// уменьшаем масштаб карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoomDown_Click(object sender, EventArgs e) {
            if (this.preZooming()) {
                //уменьшаем значение зума на 1
                this._selectedPlaceInfo.Zoom--;
                this.lblPlaceDisplayName.Text = this._selectedPlaceInfo.Zoom.ToString();
                //пересоздаем карту
                //this.CreateMap();
                this.pnlMap.Invalidate();
            }
        }

        /// <summary>
        /// инициализируем перетаскивание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlMap_MouseDown(object sender, MouseEventArgs e) {
            if (this._selectedPlaceInfo != null&&e.Button==MouseButtons.Left) {
                this._startMoveLatLng =new LatLng( this._currentLatLng.Lat,this._currentLatLng.Lng);
                this._movePaneHelper.StartMove(e.Location);
            }

        }
       
        /// <summary>
        /// если перетаскивание инициализировано, начинаем изменять координаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlMap_MouseMove(object sender, MouseEventArgs e) {
            if (this._selectedPlaceInfo != null && this._movePaneHelper.IsMouseDown) {
                this._movePaneHelper.Moving(e.Location, this.pnlMap.ClientRectangle);
                //получаем текущие широту и долготу, которые отображаются в центре панели карты
                this._currentLatLng = this._geoMapLayer.GetLatLngFromBaseLatLng(this._movePaneHelper.MoveSize, this._selectedPlaceInfo.Zoom,this._startMoveLatLng);
                Debug.WriteLine(string.Format("currLatLng=[{0}:{1}],moveSize=[{2}:{3}],startLatLng=[{4}:{5}]",
                    this._currentLatLng.Lat, this._currentLatLng.Lng, this._movePaneHelper.MoveSize.Width, this._movePaneHelper.MoveSize.Height,
                    this._startMoveLatLng.Lat, this._startMoveLatLng.Lng));
                //пересоздаем карту.
                this.CreateMap();

            }
        }
     
        /// <summary>
        /// заканчиваем перетаскивание карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlMap_MouseUp(object sender, MouseEventArgs e) {
            if (_selectedPlaceInfo != null) {   
                
                this._movePaneHelper.EndMove();
                this.CreateMap();
            }
        }

        /// <summary>
        /// обрабатываем событие, когда карту необходимо отрисовать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlMap_Paint(object sender, PaintEventArgs e) {
            //Внимание!!!. На панели карты не должно быть подчиненных контролов.
            //Периодически из - за этого происходит блокировка приложения, т.к перерисовка
            //подчинненых контролов вызывает перерисовку самой карты. А карта соответственно
            //опять перерисовку контрола. И все приехали.
            //Поэтому использовал под панелью карты вторую панель подложку с одинаковыми размерами и
            //местоположением и размещал контролы на ней и измененял z порядок, чтобы они размещались 
            //над панелью карты.
            this.CreateMap();
        }

        /// <summary>
        /// обработка события закрытия панели информации об отображаемом место положении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lLblClosePnlInfoLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.pnlLocationInfo.Visible = false;
        }

        


        

       

       
    }
}
