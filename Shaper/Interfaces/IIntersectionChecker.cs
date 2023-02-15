using Shaper.Shapes;

namespace Shaper.Interfaces
{
    public interface IIntersectionChecker
    {
        public bool CheckIntersection(Shape shape1, Shape shape2);
    }
}
