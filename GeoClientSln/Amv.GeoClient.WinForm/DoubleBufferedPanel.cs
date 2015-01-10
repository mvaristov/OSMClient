using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Amv.Geo.Core;
using System.IO;

namespace Amv.GeoClient.WinForms
{
    /// <summary>
    /// панель с применяемыми стилями.
    /// </summary>
    public class DoubleBufferedPanel : Panel
    {
        
        protected override void OnCreateControl() {
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }
        

    }
}
