using Shaper.Interfaces;
using Shaper.Shapes;
using System.Collections.Concurrent;

namespace Shaper
{
    public class ForegroundShapesChecker
    {
        private readonly IIntersectionChecker _checker;

        public ForegroundShapesChecker(IIntersectionChecker intersectionChecker)
        {
            _checker = intersectionChecker;
        }

        public event Action<object, Shape, int>? NewShapeFound = null;
        public event Action<object, int>? Progress = null;

        /// <summary>
        /// Find foreground shapes.
        /// </summary>
        /// <param name="shapes">Shapes should be ordered by drawind order.</param>
        /// <param name="amountToFind"></param>
        /// <param name="minArea"></param>
        /// <returns>Set of indexes of foreground shapes</returns>
        public IEnumerable<int> FindForegroundShapes(
            List<Shape> shapes,
            int amountToFind = int.MaxValue,
            int minArea = int.MinValue)
        {
            if (amountToFind < 0)
                throw new ArgumentOutOfRangeException("Amount must me positive number.");

            if (shapes == null)
                throw new ArgumentNullException();

            if (amountToFind == 0)
                return Array.Empty<int>();

            var result = new List<int>();
            var passed = new List<int>();

            for (var i = shapes.Count - 1; i >= 0; i--)
            {
                var thereIsIntersection = false;

                foreach (var p in result)
                {
                    if (_checker.CheckIntersection(shapes[i], shapes[p]))
                    {
                        thereIsIntersection = true;
                        break;
                    }
                }

                if (!thereIsIntersection)
                {
                    foreach (var p in passed)
                    {
                        if (_checker.CheckIntersection(shapes[i], shapes[p]))
                        {
                            thereIsIntersection = true;
                            break;
                        }
                    }
                }

                if (!thereIsIntersection && minArea <= shapes[i].GetArea())
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

        /// <summary>
        /// Find foreground shapes.
        /// </summary>
        /// <param name="shapes">Key must be an order of drawing. From 0 to N.</param>
        /// <param name="amountToFind"></param>
        /// <param name="minArea"></param>
        /// <returns>Set of indexes of foreground shapes</returns>
        public async Task<IEnumerable<int>> FindForegroundShapesAsync(
            ConcurrentDictionary<int, Shape> shapes,
            int amountToFind = int.MaxValue,
            int minArea = int.MinValue)
        {
            return await Task.Run(() => FindForegroundShapesSync(shapes, amountToFind, minArea));
        }

        public IEnumerable<int> FindForegroundShapesSync(
              ConcurrentDictionary<int, Shape> shapes,
              int amountToFind = int.MaxValue,
              int minArea = int.MinValue)
        {
            if (amountToFind < 0)
                throw new ArgumentOutOfRangeException("Amount must me positive number.");

            if (shapes == null)
                throw new ArgumentNullException();

            if (amountToFind == 0)
                return Array.Empty<int>();

            var result = new List<int>();
            var passed = new List<int>();

            for (var i = shapes.Count - 1; i >= 0; i--)
            {
                var thereIsIntersection = false;

                foreach (var p in result)
                {
                    if (_checker.CheckIntersection(shapes[i], shapes[p]))
                    {
                        thereIsIntersection = true;
                        break;
                    }
                }

                if (!thereIsIntersection)
                {
                    foreach (var p in passed)
                    {
                        if (_checker.CheckIntersection(shapes[i], shapes[p]))
                        {
                            thereIsIntersection = true;
                            break;
                        }
                    }
                }

                if (!thereIsIntersection && minArea <= shapes[i].GetArea())
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
    }
}