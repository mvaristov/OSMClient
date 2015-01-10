using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Amv.Geo.Core
{
    /// <summary>
    /// обертка для инта. Необходимо для инкремена в списке.
    /// </summary>
    public class WrapInt
    {
        public WrapInt() {
        }
        public WrapInt(int val) {
            this.Int = val;
        }
        public int Int { get; set; }
    }
    /// <summary>
    /// вспомогательный класс для рисования на карте доп объектов. 
    /// </summary>
    public abstract class MapMarkerBase
    {
        /// <summary>
        /// коллекция всех ширин всех пересекающихся с маркером тайлов карты
        /// </summary>
        private SortedList<int, WrapInt> _intersectedWidth;
        /// <summary>
        /// коллекция всех высот для пересекающихся с маркером тайлов карты
        /// </summary>
        private SortedList<int, WrapInt> _intersectedHeight;

        /// <summary>
        /// размер рисуемого маркера
        /// </summary>
        public Size Size { get; protected set; }
        /// <summary>
        /// базовая точка на карте относительно которой расчитывается положение маркера
        /// </summary>
        public Point AppLocationPoint { get; private set; }
        /// <summary>
        /// расчитываемое положение маркера отностительно базовой точки на карте
        /// </summary>
        public Point AppMarkerPoint { get; protected set; }
        /// <summary>
        /// область рисования маркера
        /// </summary>
        public Rectangle AppMarkerBounds { get; protected set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="appMarkerPoint"></param>
        /// <param name="size"></param>
        public MapMarkerBase(Point appMarkerPoint, Size size) {
            this._intersectedWidth = new SortedList<int, WrapInt>();
            this._intersectedHeight = new SortedList<int, WrapInt>();
            this.Size = size;
            this.AppLocationPoint = appMarkerPoint;
           
            //получаем координаты на панели
            this.CalcMarkerAppPaneBounds();
        }

        /// <summary>
        /// проверка признака, что маркер можно рисовать на карте. (Проверяет, что тайлы содержащие тайл уже отрисованы)
        /// </summary>
        /// <param name="appTileBounds"></param>
        /// <returns></returns>
        public virtual bool ValidForDraw(Rectangle appTileBounds) {
            if (AppMarkerBounds.IsEmpty) throw new InvalidOperationException("AppMarkerBounds не должно быть пустым. Необходимо расчитать его в методе CalcMarkerAppPaneBounds");
           
            Rectangle sectionRectangle = Rectangle.Intersect(this.AppMarkerBounds, appTileBounds);
            //проверяем есть ли пересечение
            if (!sectionRectangle.IsEmpty) {
                //должны забить в списки все квадраты, которые содержаться в области маркера
                if (!this._intersectedHeight.ContainsKey(sectionRectangle.X)) {
                    this._intersectedHeight.Add(sectionRectangle.X,new WrapInt(sectionRectangle.Height));
                }
                else {
                    //по координате Х данные уже есть, поэтому суммируем
                    this._intersectedHeight[sectionRectangle.X].Int += sectionRectangle.Height; ;
                }
                if (!this._intersectedWidth.ContainsKey(sectionRectangle.Y)) {
                    this._intersectedWidth.Add(sectionRectangle.Y, new WrapInt(sectionRectangle.Width));
                }
                else {
                    //по координате Y данные есть, суммируем
                    this._intersectedWidth[sectionRectangle.Y].Int += sectionRectangle.Width;
                }
            }

            //проверяем списки, все ли квадраты заполнены
            foreach (var item in this._intersectedHeight) {
                if (item.Value.Int<this.Size.Height) {
                    //квадраты заполнены не все на хер, выходим с "нет"
                    return false;                 
                }
            }
            foreach(var item in this._intersectedWidth){
                if (item.Value.Int < this.Size.Width) {
                    return false;                  
                }
            }
            //если, здесь то все тайлы содержащие маркер должны быть отрисованы
            this._intersectedWidth.Clear();
            this._intersectedHeight.Clear();
            return true;
        }

        /// <summary>
        ///расчет положения маркера на панели (вызывается в конструкторе)
        /// </summary>
        protected abstract void CalcMarkerAppPaneBounds();



    }
}
