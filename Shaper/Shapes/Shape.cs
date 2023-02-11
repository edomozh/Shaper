using Shaper.Structs;

namespace Shaper.Shapes
{
    public abstract class Shape
    {
        public List<Point> Points { get; set; } = new List<Point>();

        private Box? _box;
        public Box Box
        {
            get { return _box ??= GetBoundingBox(); }
            private set { _box = value; }
        }

        public abstract Box GetBoundingBox();

        public bool BoundingBoxIntersection(Box box)
        {
            return Box.X <= box.X + box.Width &&
                   Box.X + Box.Width >= box.X &&
                   Box.Y <= box.Y + box.Height &&
                   Box.Y + Box.Height >= box.Y;
        }

        public IEnumerable<(Point p1, Point p2)> GetLines()
        {
            if (Points.Count < 2)
                yield break;

            for (int i = 0; i < Points.Count - 1; i++)
                yield return new(Points[i], Points[i + 1]);

            yield return new(Points[Points.Count - 1], Points[0]);
        }
    }
}
