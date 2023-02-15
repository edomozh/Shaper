namespace Shaper.Extensions
{
    using Shaper.Structs;

    public static class PointExtension
    {
        public static double GetDistance(this Point start, Point dest)
        {
            var x = start.X - dest.X;
            var y = start.Y - dest.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}
