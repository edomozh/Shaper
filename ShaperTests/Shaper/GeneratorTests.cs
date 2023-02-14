using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class GeneratorTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GetShapes_DefaultValues_ReturnsExpectedNumberOfShapes()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes();
            Assert.That(shapes.Count(), Is.EqualTo(10));
        }

        [Test]
        public void GetShapes_Triangles_AllTrianglesHasLongSide()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(maxSize: 50, rectangles: false, lines: false, circles: false);

            Assert.That(shapes.Any(s => s is not Triangle), Is.EqualTo(false));

            Assert.That(shapes.Any(s => s.GetLines().Any(l =>
                Math.Sqrt(Math.Pow(l.p1.X - l.p2.X, 2) + Math.Pow(l.p1.Y - l.p2.Y, 2)) > 50
                )), Is.EqualTo(false));

        }

        [Test]
        public void GetShapes_Max50_ReturnsShapesWithCorrectSizes()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(count: 50, maxSize: 50, lines: false, triangles: false);
            Assert.That(shapes.Any(s => s.Box.Width > 50 || s.Box.Height > 50), Is.False);
        }

        [Test]
        public void GetShapes_Min50_ReturnsShapesWithCorrectSizes()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(count: 50, minSize: 50, maxSize: 150, lines: false, triangles: false);

            Assert.That(shapes.Any(s => s.Box.Width < 50 && s.Box.Height < 50), Is.False);
        }

        [Test]
        public void GetShapes_CountIs5_ReturnsExpectedNumberOfShapes()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(count: 5);
            Assert.That(shapes.Count(), Is.EqualTo(5));
        }

        [Test]
        public void GetShapes_OnlyTriangles_ReturnsOnlyTriangles()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(triangles: true, rectangles: false, circles: false, lines: false);
            Assert.That(shapes.All(s => s is Triangle), Is.True);
        }

        [Test]
        public void GetShapes_OnlyRectangles_ReturnsOnlyRectangles()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(triangles: false, rectangles: true, circles: false, lines: false);
            Assert.That(shapes.All(s => s is Rectangle), Is.True);
        }

        [Test]
        public void GetShapes_OnlyCircles_ReturnsOnlyCircles()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(triangles: false, rectangles: false, circles: true, lines: false);
            Assert.That(shapes.All(s => s is Circle), Is.True);
        }

        [Test]
        public void GetShapes_OnlyLines_ReturnsOnlyLines()
        {
            var generator = new Generator();
            var shapes = generator.GetShapes(triangles: false, rectangles: false, circles: false, lines: true);
            Assert.That(shapes.All(s => s is Line), Is.True);
        }

    }
}