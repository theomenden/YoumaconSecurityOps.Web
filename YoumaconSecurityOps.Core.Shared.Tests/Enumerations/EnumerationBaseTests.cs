using Shouldly;
using Xunit;

namespace YoumaconSecurityOps.Core.Shared.Tests.Enumerations
{
    public class EnumerationBaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllEnumerationValuesForType()
        {
            //ARRANGE
            var expectedCount = DummyEnumerationClass.CountOf();
            
            //ACT
            var results = DummyEnumerationClass.ReadOnlyEnumerationList.Count;
            
            //ASSERT
            results.ShouldBe(expectedCount);
        }
    }
}
