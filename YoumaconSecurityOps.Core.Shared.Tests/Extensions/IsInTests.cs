using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptionless;
using Shouldly;
using Xunit;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Core.Shared.Tests.Extensions
{

    public class IsInTests
    {
        [Fact]
        public void IsIn_ShouldReturnTrueWhenValueIsInCollection()
        {
            //ARRANGE
            var collection = Enumerable.Range(1, RandomData.GetInt(20, 500));

            var testValue = 2;

            //ACT
            var result = testValue.IsIn(collection);

            //ASSERT
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsIn_ShouldReturnFalseWhenValueIsNotInCollection()
        {
            //ARRANGE
            var collection = Enumerable.Range(1, RandomData.GetInt(20, 500));

            var testValue = 0;

            //ACT
            var result = testValue.IsIn(collection);

            //ASSERT
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsNotIn_ShouldReturnTrueWhenValueIsNotInCollection()
        {
            //ARRANGE
            var collection = Enumerable.Range(1, RandomData.GetInt(20, 500));

            var testValue = 0;

            //ACT
            var result = testValue.IsNotIn(collection);

            //ASSERT
            result.ShouldBeTrue();
        }

        [Fact]
        public void IsNotIn_ShouldReturnFalseWhenValueIsInCollection()
        {
            //ARRANGE
            var collection = Enumerable.Range(1, RandomData.GetInt(20, 500));

            var testValue = 2;

            //ACT
            var result = testValue.IsNotIn(collection);

            //ASSERT
            result.ShouldBeFalse();
        }
    }
}
