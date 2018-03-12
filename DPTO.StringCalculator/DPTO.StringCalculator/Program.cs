using System;

namespace DPTO.StringCalculator
{
    class Program
    {
        static void Main()
        {
            var calculator = new Calculator();
            while (true)
            {
                var input = Console.ReadLine();
                var result = calculator.Add(input);
                Console.WriteLine(result);
            }
        }
    }
}
