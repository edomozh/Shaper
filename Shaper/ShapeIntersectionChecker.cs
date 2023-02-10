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

        public bool IsPointInRadius(Point point, Circle circle)
        {
            double distance = GetDistanse(point, circle.Points[0]);
            return distance <= circle.Radius;
        }

        static bool OnSegment(Point p, Point q, Point r)
        {
            // Given three collinear points p, q, r, the function checks if
            // point q lies on line segment 'pr'
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
        }

        static int Orientation(Point p, Point q, Point r)
        {
            // To find orientation of ordered triplet (p, q, r).
            // The function returns following values
            // 0 --> p, q and r are collinear
            // 1 --> Clockwise
            // 2 --> Counterclockwise
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
            // for details of below formula.
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // collinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        static bool IsIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // The main function that returns true if line segment 'p1q1'
            // and 'p2q2' intersect.
            // Find the four orientations needed for general and special cases
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4) return true;

            // Special Cases
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are collinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are collinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are collinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        private double Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        private bool PointInTriangle(Point point, Point v1, Point v2, Point v3)
        {
            double d1, d2, d3;
            bool has_neg, has_pos;

            d1 = Sign(point, v1, v2);
            d2 = Sign(point, v2, v3);
            d3 = Sign(point, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
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

            Point p1 = s1.Points[0];
            Point q1 = s1.Points[1];
            Point p2 = s2.Points[0];
            Point q2 = s2.Points[1];

            return IsIntersect(p1, q1, p2, q2);
        }

        private bool LineRectangleIntersection(Shape line, Shape rectangle)
        {
            if (line is not Line s1 || rectangle is not Rectangle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            double minX = s2.Points[0].X;
            double maxX = minX + s2.Width;
            double minY = s2.Points[0].Y;
            double maxY = minY + s2.Height;

            var lines = new (Point p1, Point p2)[] {
                (new Point(minX, minY), new Point(maxX, minY)), // top
                (new Point(minX, minY), new Point(minX, maxY)), // left
                (new Point(minX, maxY), new Point(maxX, maxY)), // bottom
                (new Point(maxX, minY), new Point(maxX, maxY)), // right
            };

            var p3 = s1.Points[0];
            var p4 = s1.Points[1];

            foreach (var (p1, p2) in lines)
                if (IsIntersect(p1, p2, p3, p4))
                    return true;

            return false;
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

            return rectangle1.BoundingBoxIntersection(rectangle2.Box);
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

            var lines1 = new (Point p1, Point p2)[] {
                (s1.Points[0], s1.Points[1]),
                (s1.Points[1], s1.Points[2]),
                (s1.Points[2], s1.Points[0])
            };

            var lines2 = new (Point p3, Point p4)[] {
                (s2.Points[0], s2.Points[1]),
                (s2.Points[1], s2.Points[2]),
                (s2.Points[2], s2.Points[0])
            };

            foreach (var (p1, p2) in lines1)
                foreach (var (p3, p4) in lines2)
                    if (IsIntersect(p1, p2, p3, p4))
                        return true;

            return false;
        }

        #endregion RULEFUNCTIONS
    }
}
