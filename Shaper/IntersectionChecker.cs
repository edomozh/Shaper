using Shaper.Extensions;
using Shaper.Shapes;
using Shaper.Structs;

namespace Shaper
{
    public class IntersectionChecker
    {
        private readonly Dictionary<(Type, Type), Func<Shape, Shape, bool>> IntersectionRules = new();
        private readonly Dictionary<(Type, Type), Func<Shape, Shape, bool>> InAreaRules = new();

        public IntersectionChecker()
        {
            UpdateIntersectionRule(typeof(Circle), typeof(Circle), CircleCircleIntersectioRule);
            UpdateIntersectionRule(typeof(Circle), typeof(Line), CircleLinesIntersectionRule);
            UpdateIntersectionRule(typeof(Circle), typeof(Rectangle), CircleLinesIntersectionRule);
            UpdateIntersectionRule(typeof(Circle), typeof(Triangle), CircleLinesIntersectionRule);

            UpdateIntersectionRule(typeof(Line), typeof(Line), LinesIntersectionRule);
            UpdateIntersectionRule(typeof(Line), typeof(Rectangle), LinesIntersectionRule);
            UpdateIntersectionRule(typeof(Line), typeof(Triangle), LinesIntersectionRule);

            UpdateIntersectionRule(typeof(Rectangle), typeof(Rectangle), LinesIntersectionRule);
            UpdateIntersectionRule(typeof(Rectangle), typeof(Triangle), LinesIntersectionRule);

            UpdateIntersectionRule(typeof(Triangle), typeof(Triangle), LinesIntersectionRule);


            UpdateAreaRule(typeof(Circle), typeof(Circle), PointInCircleRule);
            UpdateAreaRule(typeof(Circle), typeof(Line), PointInCircleRule);
            UpdateAreaRule(typeof(Circle), typeof(Rectangle), PointInCircleRule);
            UpdateAreaRule(typeof(Circle), typeof(Triangle), PointInCircleRule);

            UpdateAreaRule(typeof(Rectangle), typeof(Line), PointInRectangleRule);
            UpdateAreaRule(typeof(Rectangle), typeof(Rectangle), PointInRectangleRule);
            UpdateAreaRule(typeof(Rectangle), typeof(Triangle), PointInRectangleRule);

            UpdateAreaRule(typeof(Line), typeof(Line), PointInCircleRule);
            UpdateAreaRule(typeof(Line), typeof(Triangle), PointInCircleRule);

            UpdateAreaRule(typeof(Triangle), typeof(Triangle), PointInTriangleRule);
        }

        public void UpdateIntersectionRule(Type type1, Type type2, Func<Shape, Shape, bool> checker)
        {
            IntersectionRules[(type1, type2)] = checker;
            IntersectionRules[(type2, type1)] = (s1, s2) => checker(s2, s1);
        }

        public void UpdateAreaRule(Type type1, Type type2, Func<Shape, Shape, bool> checker)
        {
            InAreaRules[(type1, type2)] = checker;
            InAreaRules[(type2, type1)] = (s1, s2) => checker(s2, s1);
        }

        public bool CheckIntersection(Shape shape1, Shape shape2)
        {
            if (!shape1.Box.Intersects(shape2.Box))
                return false;

            if (InAreaRules.TryGetValue((shape1.GetType(), shape2.GetType()), out Func<Shape, Shape, bool>? inAreaRule))
                if (inAreaRule(shape1, shape2))
                    return true;

            if (IntersectionRules.TryGetValue((shape1.GetType(), shape2.GetType()), out Func<Shape, Shape, bool>? complexRule))
                return complexRule(shape1, shape2);

            throw new ArgumentException($"No intersection rule available for the {shape1.GetType()} and {shape2.GetType()}");

        }

        #region RULEFUNCTIONS

        private bool CircleCircleIntersectioRule(Shape circle1, Shape circle2)
        {
            if (circle1 is not Circle s1 || circle2 is not Circle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            var distance = s1.Center.GetDistance(s2.Center);

            return s1.Radius + s2.Radius >= distance;
        }

        private bool CircleLinesIntersectionRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Circle circle1)
                return shape2.GetEdges().Any(l => CircleLineIntersection(l, circle1));

            if (shape2 is Circle circle2)
                return shape1.GetEdges().Any(l => CircleLineIntersection(l, circle2));

            throw new ArgumentException($"Can't cast Shape to concrete implementation.");
        }

        private static bool LinesIntersectionRule(Shape shape1, Shape shape2)
        {
            var lines1 = shape1.GetEdges();
            var lines2 = shape2.GetEdges();

            foreach (var (p1, p2) in lines1)
                foreach (var (p3, p4) in lines2)
                    if (LinesIntersect(p1, p2, p3, p4))
                        return true;

            return false;
        }

        public static bool PointInCircleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Circle circle1)
                return shape2.Points.Any(p => PointInCircle(p, circle1));

            if (shape2 is Circle circle2)
                return shape1.Points.Any(p => PointInCircle(p, circle2));

            return false;
        }

        public static bool PointInRectangleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Rectangle rect1)
                return shape2.Points.Any(p => PointInRectangle(p, rect1));

            if (shape2 is Rectangle rect2)
                return shape1.Points.Any(p => PointInRectangle(p, rect2));

            return false;
        }

        public static bool PointInTriangleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Triangle triangle1)
                return shape2.Points.Any(p => PointInTriangle(p, triangle1));

            if (shape2 is Triangle triangle2)
                return shape1.Points.Any(p => PointInTriangle(p, triangle2));

            return false;
        }

        #endregion RULEFUNCTIONS

        #region LINEINTERSECTION MATH

        public static bool CircleLineIntersection((Point p1, Point p2) line, Circle circle)
        {
            var center = circle.Center;
            var radius = circle.Radius;
            var a = line.p1;
            var b = line.p2;

            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            var A = dx * dx + dy * dy;

            var B = 2 * (dx * (a.X - center.X) + dy * (a.Y - center.Y));

            var C = (a.X - center.X) * (a.X - center.X) + (a.Y - center.Y) * (a.Y - center.Y) - radius * radius;

            var discriminant = B * B - 4 * A * C;

            if (discriminant < 0)
            {
                // Line segment and circle do not intersect
                return false;
            }
            else
            {
                // Calculate intersection points (if any)
                var t1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                var t2 = (-B - Math.Sqrt(discriminant)) / (2 * A);

                // Check if intersection points are on the line segment
                if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
            var val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // collinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        static bool LinesIntersect(Point p1, Point q1, Point p2, Point q2)
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

        #endregion LINEINTERSECTION MATH

        #region INAREA MATH

        private static double Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        private static bool PointInTriangle(Point point, Triangle triangle)
        {
            Point v1 = triangle.A;
            Point v2 = triangle.B;
            Point v3 = triangle.C;

            double d1, d2, d3;
            bool has_neg, has_pos;

            d1 = Sign(point, v1, v2);
            d2 = Sign(point, v2, v3);
            d3 = Sign(point, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        private static bool PointInRectangle(Point point, Rectangle rectangle)
        {
            Point v1 = rectangle.TopLeft;

            return (point.X >= v1.X && point.X <= v1.X + rectangle.Width) &&
                   (point.Y >= v1.Y && point.Y <= v1.Y + rectangle.Height);
        }

        public static bool PointInCircle(Point point, Circle circle)
        {
            var distance = point.GetDistance(circle.Center);
            return distance <= circle.Radius;
        }

        #endregion INAREA MATH
    }
}
