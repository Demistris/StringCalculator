using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new List<char> { ',', '\n'};

            if(numbers.StartsWith("//"))
            {
                var splitOnFirstNewLine = numbers.Split(new[] { '\n'}, 2);
                var customDelimeter = splitOnFirstNewLine[0].Replace("//", string.Empty).Single();

                delimiters.Add(customDelimeter);
                numbers = splitOnFirstNewLine[1];
            }

            var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            for (int i = 0; i < splitNumbers.Count; i++)
            {
                if (splitNumbers[i] > 1000)
                {
                    splitNumbers[i] = 0;
                }
            }

            var negativeNumbers = splitNumbers.Where(x => x < 0).ToList();

            if(negativeNumbers.Any())
            {
                throw new Exception("Negatives not allowed: " + String.Join(",", negativeNumbers));
            }

            return splitNumbers.Sum();
        }
    }
}
