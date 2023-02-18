using Shaper;
using Shaper.Shapes;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class ForegroundShapesCheckerTests
    {
        private static List<Shape> shapes = new();

        private IntersectionChecker intersectionChecker;
        private ForegroundShapesChecker seeker;
        private ShapesGenerator generator;

        [SetUp]
        public void Setup()
        {
            intersectionChecker = new IntersectionChecker();
            seeker = new ForegroundShapesChecker(intersectionChecker);
            generator = new ShapesGenerator();

            if (shapes.Count < 1)
                shapes = generator.GetShapes(short.MaxValue, short.MaxValue, short.MaxValue);
        }

        #region FindForegroundShapes

        [Test]
        public void FindForegroundShapes_Find3Shapes()
        {
            var shapes = generator.GetShapes(10, 100, 100, 5, 10);
            int expectedAmountToFind = 3;

            var result = seeker.FindForegroundShapes(shapes, expectedAmountToFind);

            Assert.That(result.Count(), Is.EqualTo(expectedAmountToFind));
        }

        [Test]
        public void FindForegroundShapes_ReturnsEmptyList_WhenAmountToFindIsZero()
        {
            var shapes = generator.GetShapes(10, 100, 100, 5, 10);
            int expectedAmountToFind = 0;

            var result = seeker.FindForegroundShapes(shapes, expectedAmountToFind);

            Assert.That(result.Count(), Is.EqualTo(expectedAmountToFind));
        }

        [Test]
        public void FindForegroundShapes_ReturnsEmptyList_WhenShapesIsEmpty()
        {
            var shapes = new List<Shape>();
            int expectedAmountToFind = 3;

            var result = seeker.FindForegroundShapes(shapes, expectedAmountToFind);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        #endregion FindForegroundShapes

        #region FindForegroundShapesAsync

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_HighLoad0()
        {
            var tasks = new List<Task>
            {
                Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);
                    Assert.That(result.Count(), Is.GreaterThan(0));
                }),

                Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);
                    Assert.That(result.Count(), Is.GreaterThan(0));
                })
            };

            await Task.WhenAll(tasks);
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_HighLoad1()
        {
            var tasks = new List<Task>
            {
                Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);
                    Assert.That(result.Count(), Is.GreaterThan(0));
                }),

                Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);
                    Assert.That(result.Count(), Is.GreaterThan(0));
                })
            };

            await Task.WhenAll(tasks);
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_NullListPassed()
        {
            await Task.Run(() =>
             {
                 var task = seeker.FindForegroundShapesAsync(null!);

                 Assert.ThrowsAsync<ArgumentNullException>(() => task);
             });
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_EmptyListPassed()
        {
            var result = await seeker.FindForegroundShapesAsync(new List<Shape>(), 10);

            Assert.That(result, Is.Empty);
        }

        #endregion FindForegroundShapesAsync

    }
}