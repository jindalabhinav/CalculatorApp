using System;

namespace SaxoInterview2.Domain
{
    public class AddCommand : ICommand, IDisposable
    {
        int count = 0;

        private AddCommand()
        {

        }

        private static AddCommand _instance;

        public static AddCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AddCommand();
            }
            return _instance;
        }

        public string Execute(string inputNumbers)
        {
            string[] numbers = inputNumbers.Split(',');
            int output = 0;

            foreach (string number in numbers)
            {
                output += Convert.ToInt32(number);
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
