using Shaper.Shapes;

namespace Shaper
{
    public class ShapeSeeker
    {

        public IEnumerable<int> FindAllForegroundShapes(int amountToFind, List<Shape> shapes)
        {
            var checker = new IntersectionChecker();

            var result = new List<int>();
            var passed = new List<int>();

            for (var i = shapes.Count - 1; i >= 0; i--)
            {
                var thereIsIntersection = false;

                foreach (var p in result)
                {
                    if (checker.CheckIntersection(shapes[i], shapes[p]))
                    {
                        thereIsIntersection = true;
                        break;
                    }
                }

                if (!thereIsIntersection)
                {
                    foreach (var p in passed)
                    {
                        if (checker.CheckIntersection(shapes[i], shapes[p]))
                        {
                            thereIsIntersection = true;
                            break;
                        }
                    }
                }


                if (!thereIsIntersection)
                {
                    result.Add(i);
                }
                else
                {
                    passed.Add(i);
                }

                if (result.Count >= amountToFind)
                {
                    break;
                }
            }

            return result;
        }

        public async Task<IEnumerable<Shape>> FindAllForegroundShapesAsync(int amountToFind, IEnumerable<Shape> shapes)
        {
            throw new NotImplementedException();
        }
    }
}