using Shaper.Shapes;

namespace Shaper
{
    public class ShapeSeeker
    {
        public event Action<object, Shape, int> NewShapeFound = null;
        public event Action<object, int> Progress = null;

        public IEnumerable<int> FindAllForegroundShapes(int amountToFind, List<Shape> shapes)
        {
            if (amountToFind < 0)
                throw new ArgumentOutOfRangeException("Amount must me positive number.");

            if (shapes == null)
                throw new ArgumentNullException();

            if (amountToFind == 0)
                return Array.Empty<int>();

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
                    NewShapeFound?.Invoke(this, shapes[i], i);
                }
                else
                {
                    passed.Add(i);
                }

                Progress?.Invoke(this, result.Count * 100 / amountToFind);

                if (result.Count >= amountToFind)
                {
                    break;
                }
            }

            return result;
        }

        public async Task<IEnumerable<int>> FindAllForegroundShapesAsync(int amountToFind, List<Shape> shapes)
        {
            return await Task.Run(() => FindAllForegroundShapes(amountToFind, shapes));
        }
    }
}