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
    /// панель отображения сообщения. Имеет свойство AutoHeight. Изменяет высоту в зависимости от текста 
    /// </summary>
    public partial class MessagePanel : UserControl
    {
       
        public MessagePanel() {
            //this.SetStyle(ControlStyles.UserPaint, true);
           // this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
           // this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //base.BackColor = Color.Transparent;
            //InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e) {
            //пишем сообщение в контроле
            if (!string.IsNullOrWhiteSpace(Text)) {
                string message = this.Text;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Near;
                //измеряем высоту строки
                int offsetText = 5;
                SizeF sizefString = this.CalcHeightMessage(this.Text, e.Graphics, this.ClientRectangle.Width-(offsetText*2));
                
                if (this.AutoHeigth) {
                    this.Height = (int)sizefString.Height + 1+_offsetTextTop+offsetText;   
                }
               
                RectangleF rect = new RectangleF(offsetText,_offsetTextTop, this.ClientRectangle.Width-(offsetText*2), this.ClientRectangle.Height-offsetText);
                e.Graphics.DrawString(message, this.Font, new SolidBrush(this.ForeColor),
                    rect, sf);

            }
            base.OnPaint(e);
        }

        private string _text;
    
        public new string Text {
            get { return _text; }
            set {
                this._text = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// сообщение для панели
        /// </summary>
        public string Message {
            get { return this.Text; }
            set { this.Text = value; }
        }

       /// <summary>
       /// признак, что у панели настраиваемая относительно размера текста высота
       /// </summary>
        public bool AutoHeigth {
            get { return this._autoHeight; }
            set { this._autoHeight = true; }
        }
        private bool _autoHeight = true;

        /// <summary>
        /// смещение текста от верха панели
        /// </summary>
        public int OffsetTextTop {
            get { return this._offsetTextTop; }
            set{
                this._offsetTextTop = value;
                this.Invalidate(false);
            }
        }
        private int _offsetTextTop = 0;

        /// <summary>
        /// расчет высоты текста
        /// </summary>
        /// <param name="message"></param>
        /// <param name="g"></param>
        /// <param name="widht"></param>
        /// <returns></returns>
        public SizeF CalcHeightMessage(string message, Graphics g, int widht) {
            return g.MeasureString(message, this.Font, widht);
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // MessagePanel
            // 
            this.Name = "MessagePanel";
            this.Size = new System.Drawing.Size(150, 95);
            this.ResumeLayout(false);

        }

        
    }
}
