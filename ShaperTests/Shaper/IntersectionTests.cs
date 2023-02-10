using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    public class IntersectionTests
    {
        ShapeIntersectionChecker _checker = new ShapeIntersectionChecker();

        [SetUp]
        public void Setup()
        {
        }

        #region CIRCLES

        [Test]
        public void Circles_Intersection()
        {
            var figure1 = new Circle(0, 0, 40);
            var figure2 = new Circle(60, 60, 45);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
            Assert.Pass();
        }

        [Test]
        public void Circles_NoIntersection()
        {
            var figure1 = new Circle(0, 0, 40);
            var figure2 = new Circle(60, 60, 40);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
            Assert.Pass();
        }

        #endregion CIRCLES

        #region LINES

        [Test]
        public void Lines_Intersection()
        {
            var figure1 = new Line(50, 50, 100, 100);
            var figure2 = new Line(50, 40, 90, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
            Assert.Pass();
        }

        [Test]
        public void Lines_NoIntersection()
        {
            var figure1 = new Line(0, 0, 100, 100);
            var figure2 = new Line(0, 1, 100, 101);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
            Assert.Pass();
        }

        #endregion LINES

        #region RECTANGLES

        [Test]
        public void Rectangles_Intersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(50, 50, 150, 150);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
            Assert.Pass();
        }

        [Test]
        public void Rectangles_NoIntersection()
        {
            var figure1 = new Rectangle(0, 0, 100, 100);
            var figure2 = new Rectangle(200, 0, 100, 100);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
            Assert.Pass();
        }

        #endregion RECTANGLES

        #region TRIANGLES

        [Test]
        public void Triangles_Intersection()
        {
            var figure1 = new Triangle(0, 0, 10, 20, 0, 20);
            var figure2 = new Triangle(0, 10, 10, 30, 5, 30);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.True);
            Assert.Pass();
        }

        [Test]
        public void Triangles_NoIntersection()
        {
            var figure1 = new Triangle(0, 0, 10, 20, 0, 20);
            var figure2 = new Triangle(0, 21, 20, 11, 20, 11);

            Assert.That(_checker.CheckIntersection(figure1, figure2), Is.False);
            Assert.Pass();
        }

        #endregion TRIANGLES

    }
}