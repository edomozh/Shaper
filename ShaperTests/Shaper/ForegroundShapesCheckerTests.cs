using Shaper;
using Shaper.Shapes;
using System.Collections.Concurrent;

namespace ShaperTests.Shaper
{
    [TestFixture]
    public class ForegroundShapesCheckerTests
    {
        private static ConcurrentDictionary<int, Shape> shapes = new();

        private IntersectionChecker intersectionChecker;
        private ForegroundShapesChecker seeker;
        private ShapesGenerator generator;

        [SetUp]
        public void Setup()
        {
            intersectionChecker = new IntersectionChecker();
            seeker = new ForegroundShapesChecker(intersectionChecker);
            generator = new ShapesGenerator();

            var i = 0;
            if (shapes.Count < 1)
            {
                var generated = generator.GetShapes(10000);
                shapes = new ConcurrentDictionary<int, Shape>(
                    generated.ToDictionary(s => i++, s => s));
            }
        }

        #region FindForegroundShapes

        [Test]
        public void FindForegroundShapes()
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
        public async Task FindForegroundShapesAsync_HighLoad()
        {
            var tasks = new List<Task>();
            for (var i = 1; i <= 1000; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);

                    Assert.That(result.Count(), Is.GreaterThan(0));
                }));
            }

            await Task.WhenAll(tasks);
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_HighLoad1()
        {
            var tasks = new List<Task>();
            for (var i = 1; i <= 1000; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);

                    Assert.That(result.Count(), Is.GreaterThan(0));
                }));
            }

            await Task.WhenAll(tasks);
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_HighLoad2()
        {
            var tasks = new List<Task>();
            for (var i = 1; i <= 1000; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await seeker.FindForegroundShapesAsync(shapes);
                    Assert.That(result.Count(), Is.GreaterThan(0));
                }));
            }

            await Task.WhenAll(tasks);
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_NullList()
        {
            await Task.Run(() =>
             {
                 var task = seeker.FindForegroundShapesAsync(null!);

                 Assert.ThrowsAsync<ArgumentNullException>(() => task);
             });
        }

        [Test]
        [Parallelizable]
        public async Task FindForegroundShapesAsync_EmptyList()
        {
            var result = await seeker.FindForegroundShapesAsync(new ConcurrentDictionary<int, Shape>(), 10);

            Assert.That(result, Is.Empty);
        }

        #endregion FindForegroundShapesAsync

    }
}