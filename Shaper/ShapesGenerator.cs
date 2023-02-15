using Shaper.Shapes;

namespace Shaper
{
    public class ShapesGenerator
    {
        private readonly Random _random = new();

        public List<Shape> GetShapes(
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
            if (surfaceHeight < maxSize * 2 || surfaceWidth < maxSize * 2)
                throw new ArgumentException("Surface Width and Height should be at least twice greater then Max shape size.");

            if (!triangles && !rectangles && !circles && !lines)
                throw new ArgumentException("There is no selected shapes for prodauction.");

            if (minSize > maxSize)
                throw new ArgumentException("Max size must be greater or equal then min size.");

            if (count <= 0 || surfaceWidth <= 0 || surfaceHeight <= 0 || minSize < 0)
                throw new ArgumentException("All number parameters must be greater than zero.");

            var shapes = new List<Shape>();

            for (int i = 0; i < count;)
            {
                if (circles && i++ < count) CreateCircle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (lines && i++ < count) CreateLine(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (triangles && i++ < count) CreateTriangle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
                if (rectangles && i++ < count) CreateRectangle(shapes, minSize, maxSize, surfaceWidth, surfaceHeight);
            }

            return shapes;
        }

        private void CreateRectangle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int width = _random.Next(minSize, maxSize);
            int height = _random.Next(minSize, maxSize);
            int topLeftX = _random.Next(0, surfaceWidth - width);
            int topLeftY = _random.Next(0, surfaceHeight - height);

            shapes.Add(new Rectangle(topLeftX, topLeftY, width, height));
        }

        private void CreateTriangle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int longestSideLength = _random.Next(minSize, maxSize);

            // Generate a random starting point for the first side of the triangle
            int x1 = _random.Next(0 + longestSideLength, surfaceWidth - longestSideLength);
            int y1 = _random.Next(0 + longestSideLength, surfaceHeight - longestSideLength);

            // Generate random angle
            double angle1 = _random.NextDouble() * 2 * Math.PI;
            // Generate sharp angle (0 - 60)
            double angle2 = angle1 + _random.NextDouble();

            // Calculate the end point of the second side of the triangle
            int x2 = (int)(x1 + longestSideLength * Math.Cos(angle1));
            int y2 = (int)(y1 + longestSideLength * Math.Sin(angle1));

            double secondSideLenght = _random.Next(minSize, longestSideLength);

            // Calculate the end point of the third side of the triangle
            int x3 = (int)(x1 + secondSideLenght * Math.Cos(angle2));
            int y3 = (int)(y1 + secondSideLenght * Math.Sin(angle2));

            var triangle = new Triangle(x1, y1, x2, y2, x3, y3);

            shapes.Add(triangle);
        }

        private void CreateLine(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            var lineLength = _random.Next(minSize, maxSize);

            // Generate a random angle for the direction of the line
            double angle = _random.NextDouble() * 2 * Math.PI;

            // Generate a random starting point for the line
            int startX = _random.Next(lineLength, surfaceWidth - lineLength);
            int startY = _random.Next(lineLength, surfaceHeight - lineLength);

            // Calculate the end point of the line based on the angle and length
            int endX = (int)(startX + lineLength * Math.Cos(angle));
            int endY = (int)(startY + lineLength * Math.Sin(angle));

            shapes.Add(new Line(startX, startY, endX, endY));
        }

        private void CreateCircle(List<Shape> shapes, int minSize, int maxSize, int surfaceWidth, int surfaceHeight)
        {
            int radius = _random.Next(minSize / 2, maxSize / 2);
            int centerX = _random.Next(radius, surfaceWidth - radius);
            int centerY = _random.Next(radius, surfaceHeight - radius);
            var circle = new Circle(centerX, centerY, radius);

            shapes.Add(circle);
        }
    }
}