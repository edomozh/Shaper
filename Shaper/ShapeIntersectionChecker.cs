using Shaper.Shapes;

namespace Shaper
{
    public class ShapeIntersectionChecker
    {
        private readonly Dictionary<(Type, Type), Func<Shape, Shape, bool>> Rules = new();

        public ShapeIntersectionChecker()
        {
            AddRule(typeof(Circle), typeof(Circle), CircleCircleIntersection);
            AddRule(typeof(Circle), typeof(Line), CircleLineIntersection);
            AddRule(typeof(Circle), typeof(Rectangle), CircleRectangleIntersection);
            AddRule(typeof(Circle), typeof(Triangle), CircleTriangleIntersection);

            AddRule(typeof(Line), typeof(Line), LineLineIntersection);
            AddRule(typeof(Line), typeof(Rectangle), LineRectangleIntersection);
            AddRule(typeof(Line), typeof(Triangle), LineTriangleIntersection);

            AddRule(typeof(Rectangle), typeof(Rectangle), RectangleRectangleIntersection);
            AddRule(typeof(Rectangle), typeof(Triangle), RectangleTriangleIntersection);

            AddRule(typeof(Triangle), typeof(Triangle), TriangleTriangleIntersection);
        }

        public void AddRule(Type type1, Type type2, Func<Shape, Shape, bool> checker)
        {
            Rules[(type1, type2)] = checker;
            Rules[(type2, type1)] = (s1, s2) => checker(s2, s1);
        }

        public bool CheckIntersection(Shape shape1, Shape shape2)
        {
            if (Rules.TryGetValue((shape1.GetType(), shape2.GetType()), out Func<Shape, Shape, bool>? func))
            {
                return func(shape1, shape2);
            }
            else
            {
                throw new ArgumentException($"No intersection checker available for the {shape1.GetType()} and {shape2.GetType()}");
            }
        }

        private double GetDistanse(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        #region RULEFUNCTIONS

        private bool CircleCircleIntersection(Shape circle1, Shape circle2)
        {
            if (circle1 is not Circle s1 || circle2 is not Circle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            var distance = GetDistanse(s1.Points[0], s2.Points[0]);

            return s1.Radius + s2.Radius >= distance;
        }

        private bool CircleLineIntersection(Shape circle, Shape line)
        {
            if (circle is not Circle s1 || line is not Line s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool CircleRectangleIntersection(Shape circle, Shape rectangle)
        {
            if (circle is not Circle s1 || rectangle is not Rectangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool CircleTriangleIntersection(Shape circle, Shape triangle)
        {
            if (circle is not Circle s1 || triangle is not Triangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool LineLineIntersection(Shape line1, Shape line2)
        {
            if (line1 is not Line s1 || line2 is not Line s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool LineRectangleIntersection(Shape line, Shape rectangle)
        {
            if (line is not Line s1 || rectangle is not Rectangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool LineTriangleIntersection(Shape line, Shape triangle)
        {
            if (line is not Line s1 || triangle is not Triangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool RectangleRectangleIntersection(Shape rectangle1, Shape rectangle2)
        {
            if (rectangle1 is not Rectangle s1 || rectangle2 is not Rectangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool RectangleTriangleIntersection(Shape rectangle, Shape triangle)
        {
            if (rectangle is not Rectangle s1 || triangle is not Triangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        private bool TriangleTriangleIntersection(Shape triangle1, Shape triangle2)
        {
            if (triangle1 is not Triangle s1 || triangle2 is not Triangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            throw new NotImplementedException();
        }

        #endregion RULEFUNCTIONS
    }
}
