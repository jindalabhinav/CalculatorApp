using System;
using System.Collections.Generic;
using SaxoInterview2.Domain;

namespace SaxoInterview2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Commands to execute. E.g. Add 2,4,5");
            Console.WriteLine("(Write command 'exit' to finish)");

            var inputs = new List<string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("exit")) break;
                inputs.Add(input);
            }
            if (inputs.Count == 0)
            {
                Console.WriteLine("Enter atleast 1 command");
                Console.ReadLine();
                return;
            }

            ICalculatorFacade calculator = new CalculatorFacade();
            var output = new List<string>();

            try
            {
                output = calculator.RunCommands(inputs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred, details: {0}", ex.Message);
                Console.ReadLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Computed Results are:");
            foreach (var result in output)
                Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}