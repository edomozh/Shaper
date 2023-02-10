namespace Shaper.Shapes
{
    public abstract class Shape
    {
        public uint DrawOrder { get; set; }

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
    }
}
