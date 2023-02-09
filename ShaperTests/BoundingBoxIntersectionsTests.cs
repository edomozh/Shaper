using Shaper.Shapes;

namespace ShaperTests
{
    public class BoundingBoxIntersectionsTests
    {
        static List<Shape> source = new List<Shape>()
        {
             new Circle(1,1, 1),
             new Line(1,1, 1,2),
             new Triangle(1,1, 2,2, 3,3),
             new Rectangle(1,1, 2,2),

             new Circle(1,1, 1),
             new Line(1,1, 1,2),
             new Triangle(1,1, 2,2, 3,3),
             new Rectangle(1,1, 2,2),
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BoundingBoxIntersection_BoundingBox_Created()
        {
            source.ForEach(s => s.GetBoundingBox());
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Triangle_Intersects()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Circle_Intersects()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Line_Intersects()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Rectangle_Intersects()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Triangle_NoIntersection()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Circle_NoIntersection()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Line_NoIntersection()
        {
            Assert.Pass();
        }

        [Test]
        public void BoundingBoxIntersection_Rectangle_NoIntersection()
        {
            Assert.Pass();
        }
    }
}