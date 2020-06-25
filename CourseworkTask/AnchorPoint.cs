using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkTask
{
    public class AnchorPoint
    {
        public const int Radius = 5;

        private readonly int _number;
        private readonly Point _location;
        private readonly CodeBlock _pointer;

        public AnchorPoint(int number, Point location, CodeBlock pointer)
        {
            _number = number;
            _location = location;
            _pointer = pointer;
        }
    }
}
