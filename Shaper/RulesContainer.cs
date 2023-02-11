using Shaper.Shapes;

namespace Shaper
{
    internal class RulesContainer
    {
        private readonly Dictionary<(Type, Type), object> Rules = new();

        public void UpdateRule<T1, T2>(Func<T1, T2, bool> checker)
            where T1 : Shape
            where T2 : Shape
        {
            Rules[(typeof(T1), typeof(T2))] = checker;
            Rules[(typeof(T2), typeof(T1))] = (T2 t2, T1 t1) => checker(t1, t2);
        }

        public Func<T1, T2, bool> GetRule<T1, T2>()
            where T1 : Shape
            where T2 : Shape
        {
            var rule = Rules[(typeof(T1), typeof(T2))];

            if (rule is not Func<T1, T2, bool> rule1 || rule is not Func<T1, T2, bool> rule2)
                throw new ArgumentException($"Fail cast rule.");

            return rule1 ?? rule2;
        }

        public Func<Shape, Shape, bool> GetRule(Shape shape, Shape shape2)
        {
            var rule = Rules[(shape.GetType(), shape2.GetType())];

            if (rule is not Func<Shape, Shape, bool> rule1 || rule is not Func<Shape, Shape, bool> rule2)
                throw new ArgumentException($"Fail cast rule.");

            return rule1 ?? rule2;
        }
    }
}
