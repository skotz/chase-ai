using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.GUI
{
    class HexTile
    {
        public PointF[] Points { get; private set; }

        public GraphicsPath Path { get; private set; }
        
        public RectangleF BoundingBox { get; private set; }

        public HexTile(PointF[] points, GraphicsPath path, float x, float y, float width, float height)
        {
            Points = points;
            Path = path;
            BoundingBox = new RectangleF(x, y, width, height);
        }

        public bool IsPointInHex(PointF point)
        {
            bool result = false;
            int index = Points.Count() - 1;
            for (int i = 0; i < Points.Count(); i++)
            {
                if (Points[i].Y < point.Y && Points[index].Y >= point.Y || Points[index].Y < point.Y && Points[i].Y >= point.Y)
                {
                    if (Points[i].X + (point.Y - Points[i].Y) / (Points[index].Y - Points[i].Y) * (Points[index].X - Points[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                index = i;
            }
            return result;
        }
    }
}
