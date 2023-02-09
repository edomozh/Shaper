namespace Shaper.Shapes
{
    internal class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double x, double y, double radius) : base(x, y)
        {
            Radius = radius;
        }

        public override bool Intersection(Shape other)
        {
            throw new NotImplementedException();
        }

        public override (double x, double y, double width, double height) GetBoundingBox()
        {
            double x = Origin.X - Radius;
            double y = Origin.Y - Radius;
            double width = 2 * Radius;
            double height = 2 * Radius;
            return (x, y, width, height);
        }
    }
}
