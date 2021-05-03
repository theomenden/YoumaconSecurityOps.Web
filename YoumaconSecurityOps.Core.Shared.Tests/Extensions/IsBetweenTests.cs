using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptionless;
using Xunit;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Core.Shared.Tests.Extensions
{

    public class IsBetweenTests
    {
        [Fact]
        public void IsBetween_ShouldReturnTrueWhenValueFallsBetweenRange()
        {
            //ARRANGE
            var lowerBound = -100;
            
            var upperBound = 100;

            var testValue = RandomData.GetInt(-99, 99);
            
            //ACT
            var result = testValue.IsBetween(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeTrue();

        }

        [Fact]
        public void IsBetween_ShouldReturnFalseWhenValueFallsOutsideRange()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = 101;

            //ACT
            var result = testValue.IsBetween(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeFalse();

        }

        [Fact]
        public void IsBetweenExclusiveLowerBound_ShouldReturnTrueWhenValueFallsBetweenRange()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = RandomData.GetInt(-99, 99);

            //ACT
            var result = testValue.IsBetweenExclusiveLowerBound(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeTrue();

        }

        [Fact]
        public void IsBetweenExclusiveLowerBound_ShouldReturnFalseWhenValueFallsOnLowerBound()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = -100;

            //ACT
            var result = testValue.IsBetweenExclusiveLowerBound(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeFalse();

        }

        [Fact]
        public void IsBetweenExclusiveUpperBound_ShouldReturnTrueWhenValueFallsBetweenRange()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = RandomData.GetInt(-99, 99);

            //ACT
            var result = testValue.IsBetweenExclusiveUpperBound(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeTrue();

        }

        [Fact]
        public void IsBetweenExclusiveUpperBound_ShouldReturnFalseWhenValueFallsOnUpperBound()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = 100;

            //ACT
            var result = testValue.IsBetweenExclusiveUpperBound(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeFalse();

        }

        [Fact]
        public void IsBetweenExclusiveBounds_ShouldReturnTrueWhenValueFallsBetweenRange()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue = RandomData.GetInt(-99, 99);

            //ACT
            var result = testValue.IsBetweenExclusiveBounds(lowerBound, upperBound);


            //ASSERT
            result.ShouldBeTrue();

        }

        [Fact]
        public void IsBetweenExclusiveBounds_ShouldReturnFalseWhenValueIsAnEndpoint()
        {
            //ARRANGE
            var lowerBound = -100;

            var upperBound = 100;

            var testValue1 = -100;

            var testValue2 = 100;

            //ACT
            var result1 = testValue1.IsBetweenExclusiveBounds(lowerBound, upperBound);

            var result2 = testValue2.IsBetweenExclusiveBounds(lowerBound, upperBound);

            //ASSERT
            result1.ShouldBeFalse();
            result2.ShouldBeFalse();
        }
    }
}
