namespace Shaper.Shapes
{
    internal class Triangle : Shape
    {
        public Point Second { get; set; }
        public Point Third { get; set; }

        public Triangle(double x, double y, double secondX, double secondY, double thirdX, double thirdY) : base(x, y)
        {
            Second = new Point(secondX, secondY);
            Third = new Point(thirdX, thirdY);
        }

        public override bool Intersection(Shape other)
        {
            throw new NotImplementedException();
        }

        public override (double x, double y, double width, double height) GetBoundingBox()
        {
            double x = Math.Min(Math.Min(Origin.X, Second.X), Third.X);
            double y = Math.Min(Math.Min(Origin.Y, Second.Y), Third.Y);
            double width = Math.Max(Math.Max(Origin.X, Second.X), Third.X) - x;
            double height = Math.Max(Math.Max(Origin.Y, Second.Y), Third.Y) - y;
            return (x, y, width, height);
        }
    }
}
