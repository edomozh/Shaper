using Shaper.Interfaces;
using Shaper.Structs;

namespace Shaper.Shapes
{
    public class Circle : Shape, ICircle
    {
        public Point Center => Points[0];

        public double Radius { get; set; }

        public Circle(double x, double y, double radius)
        {
            Points.Add(new Point(x, y));
            Radius = radius;
        }

        public override Box GetBoundingBox()
        {
            double x = Points.First().X - Radius;
            double y = Points.First().Y - Radius;
            double width = 2 * Radius;
            double height = 2 * Radius;
            return new Box(x, y, width, height);
        }

        public override int GetArea()
        {
            return (int)(Math.PI * Radius * Radius);
        }
    }
}
