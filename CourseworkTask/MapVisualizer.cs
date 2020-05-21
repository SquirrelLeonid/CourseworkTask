using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkTask
{
    public class MapVisualizer
    {
        //pixels
        private const int LineInterval = 20;
        //0.1f = 10% increase or decrease
        private const float ResizeDelta = 0.1f;

        private readonly Pen _linePen;

        public MapVisualizer()
        {
            _linePen = new Pen(Color.LightBlue, 1);
        }

        public void Redraw(Panel map)
        {
            Graphics g = map.CreateGraphics();
            int linesCount = map.Width / LineInterval + 1;
            for (int i = 0; i < linesCount; i++)
            {
                int currentCoordinate = LineInterval * i;
                g.DrawLine(_linePen, currentCoordinate, 0, currentCoordinate, map.Height);
                g.DrawLine(_linePen, 0, currentCoordinate, map.Width, currentCoordinate);
            }
        }

        public void Resize(Panel map, bool increase)
        {
            float ratio = increase ? (1 + ResizeDelta) : 1 - (ResizeDelta);
            map.Scale(new SizeF(ratio,ratio));
            Redraw(map);
        }
    }
}
