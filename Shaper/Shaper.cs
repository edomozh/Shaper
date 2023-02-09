namespace Shaper
{
    public class Shaper
    {
        public IEnumerable<Shape> FindAllForegroundShapes(int amountToFind, IEnumerable<Shape> shapes)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Shape>> FindAllForegroundShapesAsync(int amountToFind, IEnumerable<Shape> shapes)
        {
            throw new NotImplementedException();
        }

        private static double GetDistance(Point point1, Point point2)
        {
            double xDiff = point2.X - point1.X;
            double yDiff = point2.Y - point1.Y;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }
    }
}