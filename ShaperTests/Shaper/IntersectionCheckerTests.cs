using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class IntersectionCheckerTests
    {
        IntersectionChecker _checker = new IntersectionChecker();

        [SetUp]
        public void Setup()
        {
        }

        #region CIRCLES

        [Test]
        public void CheckIntersection_Circles_Intersection()
        {
            var figure1 = new Circle(40, 40, 40);
            var figure2 = new Circle(100, 100, 45);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_Circles_NoIntersection()
        {
            var figure1 = new Circle(40, 40, 40);
            var figure2 = new Circle(100, 100, 40);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CheckIntersection_CircleLine_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Line(0, 160, 160, 0);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleVerticalLine_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Line(99, 0, 99, 99);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleRightRectangle_Intersection()
        {
            var figure1 = new Circle(235, 220, 150);
            var figure2 = new Rectangle(379, 142, 300, 300);

            var rLines = figure2.GetLines().Select(l => new Line(l.p1.X, l.p1.Y, l.p2.X, l.p2.Y)).ToList();

            var i0 = _checker.CheckIntersection(figure1, rLines[0]);
            var i1 = _checker.CheckIntersection(figure1, rLines[1]);
            var i2 = _checker.CheckIntersection(figure1, rLines[2]);
            var i3 = _checker.CheckIntersection(figure1, rLines[3]); 

            Assert.That(rLines.Any(l => _checker.CheckIntersection(figure1, l)), Is.True);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleRightLine_Intersection()
        {
            var figure1 = new Circle(235, 220, 150); // maxX = 385
            var figure2 = new Line(379, 142, 379, 442);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleLine_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Line(0, 180, 180, 0);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CheckIntersection_CircleTriangle_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Triangle(0, 160, 160, 0, 1000, 1000);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleTriangle_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Triangle(0, 180, 180, 0, 1000, 1000);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CheckIntersection_CircleRectangle_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Rectangle(60, 0, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_CircleRectangle_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Rectangle(87, 87, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }


        #endregion CIRCLES

        #region LINES

        [Test]
        public void CheckIntersection_Lines_Intersection()
        {
            var figure1 = new Line(50, 50, 100, 100);
            var figure2 = new Line(50, 40, 90, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_Lines_NoIntersection()
        {
            var figure1 = new Line(0, 0, 100, 100);
            var figure2 = new Line(0, 1, 100, 101);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CheckIntersection_LineTriangle_Intersection()
        {
            var figure1 = new Line(50, 0, 1000, 1000);
            var figure2 = new Triangle(0, 0, 0, 10, 1000, 10);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_LineTriangle_NoIntersection()
        {
            var figure1 = new Line(10, 0, 1000, 9);
            var figure2 = new Triangle(0, 0, 0, 10, 1000, 10);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CheckIntersection_LineRectangle_Intersection()
        {
            var figure1 = new Line(10, 10, 1000, 1000);
            var figure2 = new Rectangle(0, 0, 50, 50);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_LineRectangle_NoIntersection()
        {
            var figure1 = new Line(55, 10, 1000, 1000);
            var figure2 = new Rectangle(0, 0, 50, 50);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion LINES

        #region RECTANGLES

        [Test]
        public void CheckIntersection_Rectangles_Intersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(50, 50, 150, 150);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_Rectangles_NoIntersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(200, 0, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }


        [Test]
        public void CheckIntersection_RectangleTriangle_Intersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Triangle(25, 25, 30, 30, 40, 15);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_RectangleTriangle_NoIntersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Triangle(125, 125, 130, 130, 140, 115);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion RECTANGLES

        #region TRIANGLES

        [Test]
        public void CheckIntersection_Triangles_Intersection()
        {
            var figure1 = new Triangle(0, 0, 0, 10, 100, 10);
            var figure2 = new Triangle(50, 100, 50, 0, 60, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CheckIntersection_Triangles_NoIntersection()
        {
            var figure1 = new Triangle(0, 0, 100, 0, 100, 10);
            var figure2 = new Triangle(0, 5, 0, 15, 100, 15);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion TRIANGLES

    }
}