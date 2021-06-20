using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaxoInterview2.Domain
{
    public class CountCommand : ICommand, IDisposable
    {
        int count = 0;
        private CountCommand()
        {

        }

        private static CountCommand _instance;

        public static CountCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CountCommand();
            }
            return _instance;
        }

        public string Execute(string input)
        {
            var commandHandler = CommandHandlerFactory.Get(input);
            return commandHandler.GetExecutionCount().ToString();
        }

        public int GetExecutionCount()
        {
            return count;
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
