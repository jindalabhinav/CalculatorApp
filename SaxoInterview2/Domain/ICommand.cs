namespace SaxoInterview2.Domain
{
    public interface ICommand
    {
        string Execute(string inputNumbers);
        int GetExecutionCount();
    }
}