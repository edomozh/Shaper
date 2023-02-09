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
        }

        public override bool Intersection(Shape other)
        {
            throw new NotImplementedException();
        }

        public override Box GetBoundingBox()
        {
            return new Box(Points[0].X, Points[0].Y, Width, Height);
        }
    }
}
