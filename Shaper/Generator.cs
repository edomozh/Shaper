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
            throw new NotImplementedException();
        }

        private void CreateTriangle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            throw new NotImplementedException();
        }

        private void CreateLine(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            throw new NotImplementedException();
        }

        private void CreateCircle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            throw new NotImplementedException();
        }
    }
}