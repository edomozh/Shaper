namespace Shaper.Shapes
{
    internal class Line : Shape
    {
        Point Second { get; set; }

        public Line(double x, double y, double x1, double y1) : base(x, y)
        {
            Second = new Point(x1, y1);
        }

        public override bool Intersection(Shape other)
        {
            throw new NotImplementedException();
        }

        public override (double x, double y, double width, double height) GetBoundingBox()
        {
            double x = Math.Min(Origin.X, Second.X);
            double y = Math.Min(Origin.Y, Second.Y);
            double width = Math.Abs(Second.X - Origin.X);
            double height = Math.Abs(Second.Y - Origin.Y);
            return (x, y, width, height);
        }
    }
}
