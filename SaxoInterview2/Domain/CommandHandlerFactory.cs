using System;
using System.Collections.Generic;

namespace SaxoInterview2.Domain
{
    public static class CommandHandlerFactory
    {
        private static Dictionary<string, ICommand> operationCommandMapping = new Dictionary<string, ICommand>()
        {
            { "add", AddCommand.GetInstance() },
            { "addition", AddCommand.GetInstance() },
            { "sum", AddCommand.GetInstance() },
            { "mult", MultiplyCommand.GetInstance() },
            { "multiply", MultiplyCommand.GetInstance() },
            { "product", MultiplyCommand.GetInstance() },
            { "multiplication", MultiplyCommand.GetInstance() },
            { "count", CountCommand.GetInstance() }
        };

        public static ICommand Get(string operation)
        {
            if (operationCommandMapping.ContainsKey(operation))
            {
                return operationCommandMapping[operation];
            }
            else
            {
                throw new ArgumentException("This operation is not recognized/implemented yet.");
            }
        }
    }
}