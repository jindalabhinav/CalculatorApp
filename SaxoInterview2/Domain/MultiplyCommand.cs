using System;

namespace SaxoInterview2.Domain
{
    public class MultiplyCommand : ICommand, IDisposable
    {
        public int count = 0;

        private MultiplyCommand()
        {

        }

        private static MultiplyCommand _instance;

        public static MultiplyCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MultiplyCommand();
            }
            return _instance;
        }

        public string Execute(string inputNumbers)
        {
            string[] numbers = inputNumbers.Split(',');
            int output = 1;

            foreach (string number in numbers)
            {
                output *= Convert.ToInt32(number);
            }
            count++;

            return output.ToString();
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
