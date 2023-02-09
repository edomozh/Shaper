namespace Shaper.Shapes
{
    internal class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double x, double y, double width, double height) : base(x, y)
        {
            Height = height;
            Width = width;
        }

        public override bool Intersection(Shape other)
        {
            throw new NotImplementedException();
        }

        public override (double x, double y, double width, double height) GetBoundingBox()
        {
            return (Origin.X, Origin.Y, Width, Height);
        }
    }
}
