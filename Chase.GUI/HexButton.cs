using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chase.GUI
{
    class HexButton : Button
    {
        public Func<bool> DisplayTileLabel { get; set; }

        public PointF[] Points { get; set; }

        public HexButton()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //e.Graphics.DrawPolygon(Pens.Blue, Points);
            //e.Graphics.DrawLine(Pens.Red, new Point(0, 0), new Point(50, 60));

            if (DisplayTileLabel != null && DisplayTileLabel())
            {
                Font font = new Font("Tahoma", 8.0f);

                int index = int.Parse(Regex.Replace(Name, "[^0-9]", ""));
                string tile = Engine.Move.GetTileFromIndex(index);

                SizeF size = e.Graphics.MeasureString(tile, font);

                if (tile != "CH")
                {
                    e.Graphics.DrawString(tile, font, new SolidBrush(Color.DarkGray), new RectangleF(new PointF(Size.Width / 2 - size.Width / 2 - 2, 15), size));
                }
            }
        }
    }
}
