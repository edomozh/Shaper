using Shaper.Shapes;

namespace Shaper
{
    public class Generator
    {
        private readonly Random _random = new Random();

        public IEnumerable<Shape> GetShapes(
            int count = 10,
            int surfaceWidth = 600,
            int surfaceHeight = 400,
            int minSize = 10,
            int maxSize = 100,

            bool triangles = true,
            bool rectangles = true,
            bool circles = true,
            bool lines = true)
        {
            var shapes = new List<Shape>();

            for (int i = 0; i < count;)
            {
                if (i++ < count && circles) CreateCircle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (i++ < count && lines) CreateLine(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (i++ < count && triangles) CreateTriangle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (i++ < count && rectangles) CreateRectangle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
            }

            return shapes;
        }

        private void CreateRectangle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int width = _random.Next(minSize, maxSize);
            int height = _random.Next(minSize, maxSize);
            int x = _random.Next(0, surfaceWidth - width);
            int y = _random.Next(0, surfaceHeight - height);

            shapes.Add(new Rectangle(x, y, width, height));
        }

        private void CreateTriangle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int sideLength = _random.Next(minSize, maxSize);
            int x1 = _random.Next(0, surfaceWidth - sideLength);
            int y1 = _random.Next(0, surfaceHeight - sideLength);
            int x2 = x1 + sideLength;
            int y2 = y1;
            int x3 = x1 + (sideLength / 2);
            int y3 = y1 + (int)(Math.Sqrt(3) / 2 * sideLength);

            var triangle = new Triangle(x1, y1, x2, y2, x3, y3);

            shapes.Add(triangle);
        }

        private void CreateLine(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            var x1 = _random.Next(0, surfaceWidth);
            var y1 = _random.Next(0, surfaceHeight);
            var x2 = x1 + _random.Next(minSize, maxSize);
            var y2 = y1 + _random.Next(minSize, maxSize);

            shapes.Add(new Line(x1, y1, x2, y2));
        }

        private void CreateCircle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int radius = _random.Next(minSize / 2, maxSize / 2);
            int x = _random.Next(surfaceWidth);
            int y = _random.Next(surfaceHeight);
            var circle = new Circle(x, y, radius);

            shapes.Add(circle);
        }
    }
}