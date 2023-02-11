using Shaper.Shapes;

namespace Shaper
{
    public class IntersectionChecker
    {
        private readonly Dictionary<(Type, Type), Func<Shape, Shape, bool>> IntersectionRules = new();
        private readonly Dictionary<(Type, Type), Func<Shape, Shape, bool>> InAreaRules = new();

        public IntersectionChecker()
        {
            CreateOrUpdateIntersectionRule(typeof(Circle), typeof(Circle), CircleCircleIntersection);
            CreateOrUpdateIntersectionRule(typeof(Circle), typeof(Line), CircleLinesIntersectionRule);
            CreateOrUpdateIntersectionRule(typeof(Circle), typeof(Rectangle), CircleLinesIntersectionRule);
            CreateOrUpdateIntersectionRule(typeof(Circle), typeof(Triangle), CircleLinesIntersectionRule);

            CreateOrUpdateIntersectionRule(typeof(Line), typeof(Line), LinesIntersection);
            CreateOrUpdateIntersectionRule(typeof(Line), typeof(Rectangle), LinesIntersection);
            CreateOrUpdateIntersectionRule(typeof(Line), typeof(Triangle), LinesIntersection);

            CreateOrUpdateIntersectionRule(typeof(Rectangle), typeof(Rectangle), LinesIntersection);
            CreateOrUpdateIntersectionRule(typeof(Rectangle), typeof(Triangle), LinesIntersection);

            CreateOrUpdateIntersectionRule(typeof(Triangle), typeof(Triangle), LinesIntersection);


            CreateOrUpdateInAreaRule(typeof(Circle), typeof(Circle), PointInCircleRule);
            CreateOrUpdateInAreaRule(typeof(Circle), typeof(Line), PointInCircleRule);
            CreateOrUpdateInAreaRule(typeof(Circle), typeof(Rectangle), PointInCircleRule);
            CreateOrUpdateInAreaRule(typeof(Circle), typeof(Triangle), PointInCircleRule);

            CreateOrUpdateInAreaRule(typeof(Rectangle), typeof(Line), PointInRectangleRule);
            CreateOrUpdateInAreaRule(typeof(Rectangle), typeof(Rectangle), PointInRectangleRule);
            CreateOrUpdateInAreaRule(typeof(Rectangle), typeof(Triangle), PointInRectangleRule);

            CreateOrUpdateInAreaRule(typeof(Line), typeof(Line), PointInCircleRule);
            CreateOrUpdateInAreaRule(typeof(Line), typeof(Triangle), PointInCircleRule);

            CreateOrUpdateInAreaRule(typeof(Triangle), typeof(Triangle), PointInTriangleRule);
        }

        public void CreateOrUpdateIntersectionRule(Type type1, Type type2, Func<Shape, Shape, bool> checker)
        {
            IntersectionRules[(type1, type2)] = checker;
            IntersectionRules[(type2, type1)] = (s1, s2) => checker(s2, s1);
        }

        public void CreateOrUpdateInAreaRule(Type type1, Type type2, Func<Shape, Shape, bool> checker)
        {
            InAreaRules[(type1, type2)] = checker;
            InAreaRules[(type2, type1)] = (s1, s2) => checker(s2, s1);
        }

        public bool CheckIntersection(Shape shape1, Shape shape2)
        {
            if (InAreaRules.TryGetValue((shape1.GetType(), shape2.GetType()), out Func<Shape, Shape, bool>? inAreaRule))
                if (inAreaRule(shape1, shape2))
                    return true;

            if (IntersectionRules.TryGetValue((shape1.GetType(), shape2.GetType()), out Func<Shape, Shape, bool>? complexRule))
                return complexRule(shape1, shape2);

            throw new ArgumentException($"No intersection rule available for the {shape1.GetType()} and {shape2.GetType()}");

        }

        #region RULEFUNCTIONS

        private bool CircleCircleIntersection(Shape circle1, Shape circle2)
        {
            if (circle1 is not Circle s1 || circle2 is not Circle s2)
                throw new ArgumentException($"Can't cast Shape to concrete implementation.");

            var distance = GetDistanse(s1.Points[0], s2.Points[0]);

            return s1.Radius + s2.Radius >= distance;
        }

        private bool CircleLinesIntersectionRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Circle c1) return shape2.GetLines().Any(l => CircleLineIntersection(l, c1));
            if (shape2 is Circle c2) return shape1.GetLines().Any(l => CircleLineIntersection(l, c2));
            throw new ArgumentException($"Can't cast Shape to concrete implementation.");
        }

        private static bool LinesIntersection(Shape shape1, Shape shape2)
        {
            var lines1 = shape1.GetLines();
            var lines2 = shape2.GetLines();

            foreach (var (p1, p2) in lines1)
                foreach (var (p3, p4) in lines2)
                    if (IsIntersect(p1, p2, p3, p4))
                        return true;

            return false;
        }

        public static bool PointInCircleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Circle c1) return shape2.Points.Any(p => PointInCircle(p, c1));
            if (shape2 is Circle c2) return shape1.Points.Any(p => PointInCircle(p, c2));
            return false;
        }

        public static bool PointInRectangleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Rectangle r1) return shape2.Points.Any(p => PointInRectangle(p, r1));
            if (shape2 is Rectangle r2) return shape1.Points.Any(p => PointInRectangle(p, r2));
            return false;
        }

        public static bool PointInTriangleRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Triangle t1) return shape2.Points.Any(p => PointInTriangle(p, t1));
            if (shape2 is Triangle t2) return shape1.Points.Any(p => PointInTriangle(p, t2));
            return false;
        }

        public static bool PointOnLineRule(Shape shape1, Shape shape2)
        {
            if (shape1 is Line l1) return shape2.Points.Any(p => PointOnLine(p, l1));
            if (shape2 is Line l2) return shape1.Points.Any(p => PointOnLine(p, l2));
            return false;
        }

        #endregion RULEFUNCTIONS

        #region LINEINTERSECTION MATH

        public static bool CircleLineIntersection((Point p1, Point p2) line, Circle circle)
        {
            Point d = new Point
            {
                X = line.p2.X - line.p1.X,
                Y = line.p2.Y - line.p1.Y
            };
            Point f = new Point
            {
                X = line.p1.X - circle.Points[0].X,
                Y = line.p1.Y - circle.Points[0].Y
            };

            double a = d.X * d.X + d.Y * d.Y;
            double b = 2 * (f.X * d.X + f.Y * d.Y);
            double c = f.X * f.X + f.Y * f.Y - circle.Radius * circle.Radius;

            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                return false;
            }

            discriminant = Math.Sqrt(discriminant);
            double t1 = (-b - discriminant) / (2 * a);
            double t2 = (-b + discriminant) / (2 * a);

            return (t1 >= 0 && t1 <= 1) || (t2 >= 0 && t2 <= 1);
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

        #endregion LINEINTERSECTION MATH

        #region INAREA MATH

        private static double Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        private static bool PointInTriangle(Point point, Triangle triangle)
        {
            Point v1 = triangle.Points[0];
            Point v2 = triangle.Points[1];
            Point v3 = triangle.Points[2];

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
            Point v1 = rectangle.Points[0];

            return (point.X >= v1.X && point.X <= v1.X + rectangle.Width) &&
                   (point.Y >= v1.Y && point.Y <= v1.Y + rectangle.Height);
        }

        private static double GetDistanse(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public static bool PointInCircle(Point point, Circle circle)
        {
            double distance = GetDistanse(point, circle.Points[0]);
            return distance <= circle.Radius;
        }

        public static bool PointOnLine(Point point, Line l)
        {
            Point start = l.Points[0];
            Point end = l.Points[1];

            double crossProduct = (end.Y - start.Y) * point.X - (end.X - start.X) * point.Y + end.X * start.Y - end.Y * start.X;
            return Math.Abs(crossProduct) < double.Epsilon;
        }

        #endregion INAREA MATH
    }
}
