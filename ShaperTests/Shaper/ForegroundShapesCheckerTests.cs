using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class ForegroundShapesSeekerTests
    {
        ForegroundShapesChecker seeker;
        Generator generator;

        [SetUp]
        public void Setup()
        {
            seeker = new ForegroundShapesSeeker();
            generator = new Generator();
        }

        [Test]
        public void FindAllForegroundShapes()
        {
            var shapes = generator.GetShapes(10, 100, 100, 5, 10);
            int expectedAmountToFind = 3;

            var result = seeker.FindAllForegroundShapes(expectedAmountToFind, shapes);

            Assert.That(result.Count(), Is.EqualTo(expectedAmountToFind));
        }

        [Test]
        public void FindAllForegroundShapes_ReturnsEmptyList_WhenAmountToFindIsZero()
        {
            var shapes = generator.GetShapes(10, 100, 100, 5, 10);
            int expectedAmountToFind = 0;

            var result = seeker.FindAllForegroundShapes(expectedAmountToFind, shapes);

            Assert.That(result.Count(), Is.EqualTo(expectedAmountToFind));
        }

        [Test]
        public void FindAllForegroundShapes_ReturnsEmptyList_WhenShapesIsEmpty()
        {
            var shapes = new List<Shape>();
            int expectedAmountToFind = 3;

            var result = seeker.FindAllForegroundShapes(expectedAmountToFind, shapes);

            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}