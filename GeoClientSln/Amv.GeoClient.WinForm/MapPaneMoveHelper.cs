using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Amv.GeoClient.WinForms
{
    /// <summary>
    /// помощник для передвижения карты по панели карты
    /// </summary>
    public class MapPaneMoveHelper
    {
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public MapPaneMoveHelper() {
           
        }

        /// <summary>
        /// признак что левая кнопка мыши выжата
        /// </summary>
        public bool IsMouseDown { get; private set; }
        /// <summary>
        /// признак что карта должна перетаскиваться
        /// </summary>
        public bool IsPaneMove { get; private set; }
        /// <summary>
        /// начальная точка на панели когда началось перетаскивание
        /// </summary>
        public Point PointStartMove { get; private set; }
        /// <summary>
        /// текущая точка перетаскивания 
        /// </summary>
        public Point CurrentMove { get; private set; }

        /// <summary>
        /// запоминаемое смещение, при окончании перетаскивания
        /// </summary>
        private Size _memMoveSize = Size.Empty;
        /// <summary>
        /// предыдущее смещение
        /// </summary>
        private Size _prevMoveSize = Size.Empty;
        /// <summary>
        /// шаг мониторинга перетаскивания карты
        /// </summary>
        private int _moveStep = 1;
        /// <summary>
        /// предыдущее смещение по X
        /// </summary>
        private int _prevStepX;
        /// <summary>
        /// предыдущее смещение по Y
        /// </summary>
        private int _prevStepY;

        /// <summary>
        /// начало перетаскивания
        /// </summary>
        /// <param name="startPointMove"></param>
        public void StartMove(Point startPointMove) {
            this.IsMouseDown = true;
            this.PointStartMove = startPointMove;
            this._prevStepY = this.PointStartMove.Y;
            this._prevStepX = this.PointStartMove.X;
        }

        /// <summary>
        /// собственно перетаскивание
        /// </summary>
        /// <param name="currentPointMove"></param>
        /// <param name="mapPaneBounds"></param>
        public void Moving(Point currentPointMove,Rectangle mapPaneBounds) {
            if (this.IsMouseDown && mapPaneBounds.Contains(currentPointMove)) {
                this.CurrentMove = currentPointMove;
                this.IsPaneMove = true;
            }
            else {
                this.EndMove();
            }
        }

        /// <summary>
        /// окончание перетаскивания
        /// </summary>
        public void EndMove() { 
            if (this.IsPaneMove) {
                this.IsPaneMove = false;
                this._memMoveSize = MoveSize;  
            }
            this.IsMouseDown = false;
            this.ResetMove();
           
        }

        /// <summary>
        /// размер перетаскивания от начала
        /// </summary>
        public Size MoveSize {
            get {
                if (this.IsMouseDown) {
                    if (Math.Abs((this.CurrentMove.X - _prevStepX)) > _moveStep || Math.Abs((this.CurrentMove.Y - _prevStepY)) > _moveStep) {
                        this._prevMoveSize = new Size(this._memMoveSize.Width + (this.CurrentMove.X - this.PointStartMove.X),
                        this._memMoveSize.Height + (this.CurrentMove.Y - this.PointStartMove.Y));
                        this._prevStepX = this.CurrentMove.X;
                        this._prevStepY = this.CurrentMove.Y;
                    }
                    return this._prevMoveSize;
                }
                else {
                    return this._memMoveSize;
                }
            }
        }

        /// <summary>
        /// сброс все значений перетаскивания на начальные
        /// </summary>
        public void ResetMove() {
            this.IsMouseDown = false;
            this.IsPaneMove = false;
            this.PointStartMove = Point.Empty;
            this.CurrentMove = Point.Empty;
            this._memMoveSize = Size.Empty;
            this._prevMoveSize = Size.Empty;
            _prevStepX = 0;
            _prevStepY = 0;

        }
     
       


    }
}
