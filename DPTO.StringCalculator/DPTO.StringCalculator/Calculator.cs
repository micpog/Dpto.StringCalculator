using System;
using System.Collections.Generic;
using System.Linq;

namespace DPTO.StringCalculator
{
    public class Calculator
    {
        private const string EndCharacters = @"\n";
        private const string StartCharacters = "//";

        private readonly List<int> _negatives = new List<int>();

        private string[] _defaultDelimiters = { ",", @"\n" };

        public int Add(string numbers)
        {
            return string.IsNullOrEmpty(numbers) 
                ? 0 
                : GetSum(numbers);
        }

        private int GetSum(string numbers)
        {
            if (HasSpecificDelimiter(numbers))
            {
                var delimiter = GetDelimiter(numbers);
                ExtendDefaultDelimiters(delimiter);
            }

            var sum = SumSplittedNumbers(numbers);
            if (HasNegativeNumbers())
            {
                throw new InvalidOperationException($"Negatives ({string.Join(", ", _negatives)}) not allowed.");
            }

            return sum;
        }

        private void ExtendDefaultDelimiters(string delimiter)
        {
            var delimitersList = _defaultDelimiters.ToList();
            delimitersList.Add(delimiter);
            _defaultDelimiters = delimitersList.ToArray();
        }

        private int SumSplittedNumbers(string numbers)
        {
            var sum = numbers.Split(_defaultDelimiters, StringSplitOptions.RemoveEmptyEntries).Sum(s =>
            {
                var number = ParseToInt(s);
                if (IsNegative(number))
                {
                    _negatives.Add(number);
                }

                return number;
            });
            return sum;
        }

        private int ParseToInt(string s)
        {
            int number;
            int.TryParse(s, out number);
            return number;
        }

        private string GetDelimiter(string numbers)
        {
            var delimiterFrom = numbers.IndexOf(StartCharacters, StringComparison.Ordinal) + StartCharacters.Length;
            var delimiterTo = numbers.IndexOf(EndCharacters, StringComparison.Ordinal);

            return numbers.Substring(delimiterFrom, delimiterTo - delimiterFrom);
        }

        private bool HasSpecificDelimiter(string numbers) => numbers.StartsWith(StartCharacters);

        private bool HasNegativeNumbers() => _negatives.Count != 0;

        private bool IsNegative(int number) => number < 0;
    }
}