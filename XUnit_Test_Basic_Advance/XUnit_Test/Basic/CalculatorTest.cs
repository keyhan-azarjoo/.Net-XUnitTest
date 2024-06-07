using System.Collections;
using FluentAssertions;
using Domain.FirstLearning;

namespace ProjectTest_XUnit.FirstLearning
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator;
        public CalculatorTest()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Sum_of_2_and_2_should_be_4_One()
        {
            var calculator = new Calculator();
            var result = calculator.Sum(2, 2);
            // as we dont want to use if statement a lot we instal FluentAssertions and use it instead of useing if statements. because it esear and better.
            if (result != 4)
            {
                throw new Exception($"TheS Sum(2,2) was expected to be 4 but it's {result}.");
            }
        }


        // This is a better and easier way to impliment the test.
        [Fact]
        public void Sum_of_2_and_2_should_be_4_Two()
        => new Calculator()
            .Sum(2, 2)
            .Should().Be(4);




        [Fact]
        public void Mines_of_10_and_3_should_be_7()
        {
            var result = _calculator.mines(10, 3);
            Assert.Equal(7, result);
        }





        // You can pass some parameter for test like here
        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(100, 50, 50)]
        [InlineData(56, 23, 19)]
        [InlineData(0, 2, -2)]
        public void Mines_of_a_and_b_should_be_cTheory(int left, int right, int expectedResult)
        {
            var result = _calculator.mines(left, right);
            Assert.Equal(expectedResult, result);
        }







        // For Skipping a test you can use Skip like here
        [Fact(Skip = "I want to ignore this test")]
        public void Mines_of_5_and_2_should_be_3()
        {
            var result = _calculator.mines(5, 2);
            Assert.Equal(20, result);
        }







        // You Can add some data as IEnumerable and use MemberData like here
        [Theory]
        [MemberData(nameof(TestData))]
        public void Add_of_a_and_b_should_be_c_Theory(int expected, params int[] valuesToAdd)
        {
            var result = _calculator.Sum(valuesToAdd[0], valuesToAdd[1]);
            Assert.Equal(expected, result);
        }
        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { 15, new int[] { 10, 5 } };
            yield return new object[] { 15, new int[] { 5, 5 } }; // should give error
            yield return new object[] { 15, new int[] { -10, 25 } };
            yield return new object[] { 15, new int[] { 10, 10 } }; // should give error
            yield return new object[] { 15, new int[] { 20, -5 } };

        }










        // You Can use a class to devide some data as IEnumerable and use ClassData like here
        [Theory]
        [ClassData(typeof(DivisionTestData))]
        public void division_of_a_and_b_should_be_c_Theory(int expected, params int[] valuesToAdd)
        {
            var result = _calculator.devide(valuesToAdd[0], valuesToAdd[1]);
            Assert.Equal(expected, result);
        }

    }



    public class DivisionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 30, new int[] { 60, 2 } };
            yield return new object[] { 0, new int[] { 0, 1 } };
            yield return new object[] { 15, new int[] { 10, 10 } };
            yield return new object[] { 1, new int[] { 50, 50 } };// should gave error
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}