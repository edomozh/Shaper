using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    public class IntersectionTests
    {
        IntersectionChecker _checker = new IntersectionChecker();

        [SetUp]
        public void Setup()
        {
        }

        #region CIRCLES

        [Test]
        public void Circles_Intersection()
        {
            var figure1 = new Circle(40, 40, 40);
            var figure2 = new Circle(100, 100, 45);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void Circles_NoIntersection()
        {
            var figure1 = new Circle(40, 40, 40);
            var figure2 = new Circle(100, 100, 40);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CircleLine_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Line(0, 160, 160, 0);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CircleLine_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Line(0, 180, 180, 0);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CircleTriangle_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Triangle(0, 160, 160, 0, 1000, 1000);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CircleTriangle_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Triangle(0, 180, 180, 0, 1000, 1000);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void CircleRectangle_Intersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Rectangle(60, 100, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void CircleRectangle_NoIntersection()
        {
            var figure1 = new Circle(50, 50, 50);
            var figure2 = new Rectangle(86, 86, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }


        #endregion CIRCLES

        #region LINES

        [Test]
        public void Lines_Intersection()
        {
            var figure1 = new Line(50, 50, 100, 100);
            var figure2 = new Line(50, 40, 90, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void Lines_NoIntersection()
        {
            var figure1 = new Line(0, 0, 100, 100);
            var figure2 = new Line(0, 1, 100, 101);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void LineTriangle_Intersection()
        {
            var figure1 = new Line(50, 0, 1000, 1000);
            var figure2 = new Triangle(0, 0, 0, 10, 1000, 10);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void LineTriangle_NoIntersection()
        {
            var figure1 = new Line(10, 0, 1000, 9);
            var figure2 = new Triangle(0, 0, 0, 10, 1000, 10);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        [Test]
        public void LineRectangle_Intersection()
        {
            var figure1 = new Line(10, 10, 1000, 1000);
            var figure2 = new Rectangle(0, 0, 50, 50);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void LineRectangle_NoIntersection()
        {
            var figure1 = new Line(55, 10, 1000, 1000);
            var figure2 = new Rectangle(0, 0, 50, 50);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion LINES

        #region RECTANGLES

        [Test]
        public void Rectangles_Intersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(50, 50, 150, 150);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void Rectangles_NoIntersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(200, 0, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }


        [Test]
        public void RectangleTriangle_Intersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Triangle(25, 25, 30, 30, 40, 15);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void RectangleTriangle_NoIntersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Triangle(125, 125, 130, 130, 140, 115);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion RECTANGLES

        #region TRIANGLES

        [Test]
        public void Triangles_Intersection()
        {
            var figure1 = new Triangle(0, 0, 0, 10, 100, 10);
            var figure2 = new Triangle(50, 100, 50, 0, 60, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
        }

        [Test]
        public void Triangles_NoIntersection()
        {
            var figure1 = new Triangle(0, 0, 100, 0, 100, 10);
            var figure2 = new Triangle(0, 5, 0, 15, 100, 15);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
        }

        #endregion TRIANGLES

    }
}