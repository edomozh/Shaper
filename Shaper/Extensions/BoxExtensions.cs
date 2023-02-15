using Shaper.Structs;

namespace Shaper.Extensions
{
    public static class BoxExtensions
    {
        public static bool Intersects(this Box box1, Box box)
        {
            return box1.X <= box.X + box.Width &&
                   box1.X + box1.Width >= box.X &&
                   box1.Y <= box.Y + box.Height &&
                   box1.Y + box1.Height >= box.Y;
        }
    }
}
