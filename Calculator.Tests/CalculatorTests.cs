using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void Add_AddsUpToTwoNumbers_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("10,90,10,20", 130)]
        public void Add_AddsUpToAnyNumber_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("10\n90,10\n20", 130)]
        public void Add_AddsNumberUsingNewLineDelimiter_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;2;4", 7)]
        public void Add_AddsNumberUsingCustomDelimiter_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("1,2,-1", "-1")]
        [InlineData("//;\n1;-2;-4", "-2,-4")]
        public void Add_ShouldThrowAnException_WhenNegativeNumbersAreUsed(string calculation, string negativeNumbers)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            Action action = () => sut.Add(calculation);

            //Assert
            action.Should().Throw<Exception>().WithMessage("Negatives not allowed: " + negativeNumbers);
        }

        [Theory]
        [InlineData("1001,2", 2)]
        [InlineData("1234,0", 0)]
        public void Add_IgnoreNumbersHigherThan1000_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }

        ////[***]\n1***2***3
        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[*]\n1*2*3", 6)]
        public void Add_AddsNumberAnyLengthDelimiter_WhenStringIsValid(string calculation, int expected)
        {
            //Arrange
            var sut = new Calculator();

            //Act
            var result = sut.Add(calculation);

            //Assert
            result.Should().Be(expected);
        }
    }
}
