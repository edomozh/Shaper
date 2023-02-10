namespace Shaper.Shapes
{
    public class Line : Shape
    {
        public Line(double x, double y, double x1, double y1)
        {
            Points.Add(new Point(x, y));
            Points.Add(new Point(x1, y1));
        }

        public override Box GetBoundingBox()
        {
            double x = Math.Min(Points[0].X, Points[1].X);
            double y = Math.Min(Points[0].Y, Points[1].Y);
            double width = Math.Abs(Points[1].X - Points[0].X);
            double height = Math.Abs(Points[1].Y - Points[0].Y);
            return new Box(x, y, width, height);
        }
    }
}
