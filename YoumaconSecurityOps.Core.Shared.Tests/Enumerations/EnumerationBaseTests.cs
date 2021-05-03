using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using YoumaconSecurityOps.Core.Shared.Enumerations;

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
            var results = EnumerationBase.GetAll<DummyEnumerationClass>().Count();
            
            //ASSERT
            results.ShouldBe(expectedCount);
        }
    }
}
