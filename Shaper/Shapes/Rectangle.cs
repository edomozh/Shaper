using Shaper.Interfaces;
using Shaper.Structs;

namespace Shaper.Shapes
{
    public class Rectangle : Shape, IRectangle
    {
        public Point TopLeft => Points[0];

        public double Width { get; set; }

        public double Height { get; set; }

        public Rectangle(double x, double y, double width, double height)
        {
            Height = height;
            Width = width;

            double minX = x;
            double minY = y;
            double maxX = x + width;
            double maxY = y + height;

            // We should carefully add points to follow figure shape
            Points.Add(new Point(minX, minY));  // top left
            Points.Add(new Point(maxX, minY));  // top right
            Points.Add(new Point(maxX, maxY));  // bottom right
            Points.Add(new Point(minX, maxY));  // bottom left
        }

        public override Box GetBoundingBox()
        {
            return new Box(Points[0].X, Points[0].Y, Width, Height);
        }

        public override int GetArea()
        {
            return (int)(Height * Width);
        }
    }
}
