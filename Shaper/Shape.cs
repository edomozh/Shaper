using System.Drawing;

namespace Shaper
{
    public abstract class Shape
    {
        protected Shape(Point position)
        {
            Position = position;
        }

        Point Position { get; set; }
    }
}
