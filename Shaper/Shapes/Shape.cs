namespace Shaper.Shapes
{
    public abstract class Shape
    {
        public ulong DrawOrder { get; set; }

        public Point Origin { get; set; }

        protected Shape(double x, double y)
        {
            Origin = new Point(x, y);
        }

        public abstract bool Intersection(Shape box2);

        public abstract (double x, double y, double width, double height) GetBoundingBox();

        public bool BoundingBoxIntersection(Shape shape1, Shape shape2)
        {
            var box1 = shape1.GetBoundingBox();
            var box2 = shape2.GetBoundingBox();

            return box1.x < box2.x + box2.width &&
                   box1.x + box1.width > box2.x &&
                   box1.y < box2.y + box2.height &&
                   box1.y + box1.height > box2.y;
        }
    }
}
