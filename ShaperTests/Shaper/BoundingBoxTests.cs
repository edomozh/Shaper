using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class BoundingBoxIntersectionsTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void BoundingBox_IntersectsByCorner()
        {
            var rect1 = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(100, 100, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.True);
        }

        [Test]
        public void BoundingBox_Intersects()
        {
            var rect1 = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(50, 50, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.True);
        }

        [Test]
        public void BoundingBox_IntersectsByEdge()
        {
            var rect1 = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(100, 0, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.True);
        }

        [Test]
        public void BoundingBox_IntersectsByAll()
        {
            var rect1 = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(0, 0, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.True);
        }

        [Test]
        public void BoundingBox_NoIntersects()
        {
            var rect1 = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(500, 500, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.False);
        }

        [Test]
        public void BoundingBox_NoIntersects2()
        {
            var rect1 = new Rectangle(101, 0, 100, 100);
            var rect2 = new Rectangle(0, 0, 100, 100);

            Assert.That(rect1.BoundingBoxIntersection(rect2.Box), Is.False);
        }
    }
}