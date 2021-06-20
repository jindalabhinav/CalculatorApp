using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SaxoInterview2
{
    public interface ICommand
    {
        string Execute(string input);
    }

    public class AddCommand : ICommand
    {
        public string Execute(string input)
        {
            string[] parameters = input.Split();
            string operation = parameters[0];
            string[] numbers = parameters[1].Split(',');

            int output = 0;

            foreach (string number in numbers)
            {
                output += Convert.ToInt32(number);
            }

            return output.ToString();
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Sample Command
            // Add 1,2,4,5;
        }

        // From somewhere
        string command = "Add 1,2,3,4,5";
    }

    public class Calculator
    {
        private readonly ICommand calculate;

        // scalable design for 100s of commands without switch

        // AddCommand - take a ref of ICommand


        public async Task<List<string>> ExecuteCommands(List<string> commands)
        {
            foreach(string command in commands)
            {
                ICommand commandObject = new AddCommand();
                commandObject.Execute(command);

                // client implementing the ICommand c

                var commandCalc = CalculateObjectFactory(operation);
                string output = commandCalc.Execute(numbers);
            }
        }

        private ICommand CalculateObjectFactory(string operation)
        {
            switch (operation)
            {
                case "Add":
                    return new AddCommand();
                    break;
            }
        }

        // Add 1,2,3,4,5
        public static async Task<string> ExecuteAsync(string input)
        {
            string output = Execute(input);

            await Task.Delay(1000);
            return output.ToString();
        }

        public static async Task<List<string>> ExecuteBatch(List<string> input)
        {
            var output = new List<string>();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(input, async inputCommand =>
            {
                output.Add(await ExecuteAsync(inputCommand));
            });

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            return output.ToList();
        }

        public static string Execute(string input)
        {
            string[] parameters = input.Split();
            string operation = parameters[0];
            string[] numbers = parameters[1].Split(',');

            int output = 0;

            switch (operation)
            {
                case "Add":
                    foreach (string number in numbers)
                    {
                        output += Convert.ToInt32(number);
                    }
                    break;

                case "Mult":
                    output = 1;
                    foreach (string number in numbers)
                    {
                        output *= Convert.ToInt32(number);
                    }
                    break;
            }

            return output.ToString();
        }
    }
}
