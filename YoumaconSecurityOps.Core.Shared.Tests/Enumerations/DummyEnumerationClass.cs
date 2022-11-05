using TheOmenDen.Shared.Enumerations;

namespace YoumaconSecurityOps.Core.Shared.Tests.Enumerations
{
    internal sealed record DummyEnumerationClass: EnumerationBase<DummyEnumerationClass>
    {
        private static int _count;

        public DummyEnumerationClass(string name, int id) 
            : base(name, id)
        {
            _count++;
        }

        public static DummyEnumerationClass Test1 = new ("Test1", 1);

        public static DummyEnumerationClass Test2 = new ("Test2", 2);

        public static DummyEnumerationClass Test3 = new ("Test3", 3);

        public static DummyEnumerationClass Test4 = new ("Test4", 4);

        public static int CountOf()
        {
            return _count;
        }
    }
}
