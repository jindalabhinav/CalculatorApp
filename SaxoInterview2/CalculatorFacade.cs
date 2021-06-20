using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaxoInterview2.Domain;

namespace SaxoInterview2
{
    public class CalculatorFacade
    {
        /// <summary>
        /// Runs command in series and returns the output
        /// </summary>
        /// <param name="commands">Command to execute. E.g. - Add 1,2,3,4</param>
        /// <returns>Output of the command run. E.g. - 10</returns>
        public static List<string> RunCommands(List<string> commands)
        {
            var output = new List<string>();

            foreach (var command in commands)
            {
                (string operation, string input) = GetCommandDetails(command);
                var commandHandler = CommandHandlerFactory.Get(operation);
                var result = commandHandler.Execute(input);
                output.Add(result);
            }

            return output;
        }

        private static (string operation, string input) GetCommandDetails(string command)
        {
            string[] parameters = command.Split();
            string operation = parameters[0];
            string input = parameters[1];
            return (operation.ToLower(), input.ToLower());
        }

        /// <summary>
        /// Runs command in Parallel, and gaurantees correct order of output.
        /// KnownBug - Count of a CommandType may be incorrect since parallely run code works on an unordered collection of data inputs
        /// </summary>
        /// <param name="commands">Command to execute. E.g. - Add 1,2,3,4</param>
        /// <returns>Output of the command run. E.g. - 10</returns>
        public static List<string> RunCommandsParallel(List<string> commands)
        {
            var unorderedOutput = new Dictionary<int, string>();
            var indexedCommands = GenerateIndexedCommands(commands);

            Parallel.ForEach(indexedCommands, command =>
            {
                (string operation, string input) = GetCommandDetails(command.Value);
                var commandHandler = CommandHandlerFactory.Get(operation);
                var result = commandHandler.Execute(input);
                unorderedOutput.Add(command.Key, result);
            });
            var output = SortUnorderedCommandResults(unorderedOutput);
            return output;
        }

        private static Dictionary<int, string> GenerateIndexedCommands(List<string> commands)
        {
            var indexedCommands = new Dictionary<int, string>();
            int index = 0;
            foreach(var command in commands)
            {
                indexedCommands.Add(index++, command);
            }
            return indexedCommands;
        }

        private static List<string> SortUnorderedCommandResults(Dictionary<int, string> unorderedOutput)
        {
            return unorderedOutput.OrderBy(o => o.Key).Select(x => x.Value).ToList();
        }


        #region Obsolete Methods - Created during the Interview
        [Obsolete]
        public static async Task<string> ExecuteAsync(string input)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string output = Execute(input);
            await Task.Delay(1000);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");

            return output.ToString();
        }

        [Obsolete]
        // KnownBug - Sometimes the count of output collection is less than the input collection size
        public static async Task<List<string>> ExecuteBatch(List<string> commands)
        {
            var results = new List<(long, string)>();
            var output = new List<string>();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(commands, (command, state, i) =>
            {
                results.Add((i, Execute(command)));
            });

            var orderedResults = results.OrderBy(o => o.Item1);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            return orderedResults.Select(x => x.Item2).ToList();
        }

        [Obsolete]
        public static string Execute(string input)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

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
            Thread.Sleep(1000);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");

            return output.ToString();
        }
        #endregion
    }
}