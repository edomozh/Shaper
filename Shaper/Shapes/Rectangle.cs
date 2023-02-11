using Shaper.Structs;

namespace Shaper.Shapes
{
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double x, double y, double width, double height)
        {
            Points.Add(new Point(x, y));
            Height = height;
            Width = width;

            double minX = Points[0].X;
            double minY = Points[0].Y;
            double maxX = minX + Width;
            double maxY = minY + Height;

            Points.Add(new Point(maxX, minY));  // top right
            Points.Add(new Point(minX, maxY));  // bottom left
            Points.Add(new Point(maxX, maxY));  // bottom right
        }

        public override Box GetBoundingBox()
        {
            return new Box(Points[0].X, Points[0].Y, Width, Height);
        }
    }
}
