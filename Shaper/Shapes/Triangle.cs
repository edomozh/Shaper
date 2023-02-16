using Shaper.Extensions;
using Shaper.Interfaces;
using Shaper.Structs;

namespace Shaper.Shapes
{
    public class Triangle : Shape, ITriangle
    {
        public Point A => Points[0];

        public Point B => Points[1];

        public Point C => Points[2];

        public Triangle(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            Points.Add(new Point(aX, aY));
            Points.Add(new Point(bX, bY));
            Points.Add(new Point(cX, cY));
        }

        public override Box GetBoundingBox()
        {
            double x = Math.Min(Math.Min(Points[0].X, Points[1].X), Points[2].X);
            double y = Math.Min(Math.Min(Points[0].Y, Points[1].Y), Points[2].Y);
            double width = Math.Max(Math.Max(Points[0].X, Points[1].X), Points[2].X) - x;
            double height = Math.Max(Math.Max(Points[0].Y, Points[1].Y), Points[2].Y) - y;
            return new Box(x, y, width, height);
        }

        public override int GetArea()
        {
            // Calculate the length of each side of the triangle
            var sideA = A.GetDistance(B);
            var sideB = B.GetDistance(C);
            var sideC = C.GetDistance(A);

            // Calculate the semiperimeter of the triangle
            var semiperimeter = (sideA + sideB + sideC) / 2;

            // Calculate the area of the triangle using Heron's formula
            int area = (int)Math.Sqrt(semiperimeter * (semiperimeter - sideA) * (semiperimeter - sideB) * (semiperimeter - sideC));

            return area;
        }
    }
}
