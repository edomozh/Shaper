using Shaper.Interfaces;
using Shaper.Structs;

namespace Shaper.Shapes
{
    public class Triangle : Shape, ITriangle
    {
        public Triangle(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            Points.Add(new Point(aX, aY));
            Points.Add(new Point(bX, bY));
            Points.Add(new Point(cX, cY));
        }

        public Point A => Points[0];

        public Point B => Points[1];

        public Point C => Points[2];

        public override Box GetBoundingBox()
        {
            double x = Math.Min(Math.Min(Points[0].X, Points[1].X), Points[2].X);
            double y = Math.Min(Math.Min(Points[0].Y, Points[1].Y), Points[2].Y);
            double width = Math.Max(Math.Max(Points[0].X, Points[1].X), Points[2].X) - x;
            double height = Math.Max(Math.Max(Points[0].Y, Points[1].Y), Points[2].Y) - y;
            return new Box(x, y, width, height);
        }
    }
}
