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
        public void GetShapes_Triangles_AllSidesShorterThenMaxSize()
        {
            var maxSize = 50;
            var generator = new Generator();
            var shapes = generator.GetShapes(count: 10000, maxSize: maxSize, rectangles: false, lines: false, circles: false);

            Assert.That(shapes.Any(s => s is not Triangle), Is.EqualTo(false));

            Assert.That(shapes.Any(s => s.GetLines().Any(line =>
                {
                    var x = line.p1.X - line.p2.X;
                    var y = line.p1.Y - line.p2.Y;
                    return Math.Sqrt(x * x + y * y) > maxSize;
                }
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