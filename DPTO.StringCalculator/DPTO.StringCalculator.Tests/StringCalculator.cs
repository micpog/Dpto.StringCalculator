using NUnit.Framework;
using System;
using System.Linq;

namespace DPTO.StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculator
    {
        [Test]
        public void Add_Should_ReturnZeroFromEmptyString()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Add("");

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [TestCase("10")]
        [TestCase("12")]
        public void Add_Should_ReturnSimpleNumber(string singleNumber)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Add(singleNumber);

            // Assert
            Assert.That(result, Is.EqualTo(int.Parse(singleNumber)));
        }

        [Test]
        [TestCase(new[] { 7, 4, 5, 6, 1, 2 })]
        [TestCase(new[] { 1, 6, 5, 6, 1 })]
        [TestCase(new[] { 1, 2, 0, 100 })]
        [TestCase(new[] { 1, 2, 0, 100, 20, 6, 800, 564, 1, 32 })]
        public void Add_Should_SumCommaSeparatedNumbers(int[] numbers)
        {
            // Arrange
            var calculator = new Calculator();
            var stringNumber = numbers.Aggregate("", (current, number) => current + $"{number},");

            // Act
            var result = calculator.Add(stringNumber);

            // Assert
            Assert.That(result, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase(new[] { 7, 4, 5, 6, 1, 2 })]
        [TestCase(new[] { 1, 6, 5, 6, 1 })]
        [TestCase(new[] { 1, 2, 0, 100 })]
        public void Add_Should_SumNewLineSeparetedNumbers(int[] numbers)
        {
            // Arrange
            var calculator = new Calculator();
            var stringNumber = numbers.Aggregate("", (current, number) => current + $"{number}\\n");

            // Act
            var result = calculator.Add(stringNumber);

            // Assert
            Assert.That(result, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase(";", 1, 2, 3)]
        [TestCase(":", 3, 4, 5)]
        [TestCase("`", 1, 4, 7)]
        public void Add_Should_SupportDifferentDelimiterSeparetedNumbers(string delimiter, int firstNumber, int secondNumber, int thirdNumber)
        {
            // Arrange
            var calculator = new Calculator();
            var stringNumber = "//" + delimiter + @"\n" + $"{firstNumber}" + delimiter + $"{secondNumber}" + delimiter + $"{thirdNumber}";

            // Act
            var result = calculator.Add(stringNumber);

            // Assert
            Assert.That(result, Is.EqualTo(firstNumber + secondNumber + thirdNumber));
        }

        [Test]
        [TestCase(new [] { -100, 20, -1 })]
        [TestCase(new [] { 0, 20, -1 })]
        [TestCase(new [] { -100, -20, -1 })]
        public void Add_Should_ThrowException_WhenAddingNegativeNumbers(int [] numbers)
        {
            // Arrange
            var calculator = new Calculator();
            var stringNumbers = $"{numbers[0]},{numbers[1]},{numbers[2]}";
            var negativeList = numbers.Where(n => n < 0).ToList();

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                calculator.Add(stringNumbers);
            });

            var negativeNumbers = string.Join(", ", negativeList);
            var exceptionMessage = $"Negatives ({negativeNumbers}) not allowed.";

            // Assert
            Assert.That(exception.Message == exceptionMessage);
        }
    }
}
