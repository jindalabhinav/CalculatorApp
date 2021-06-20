using System.Collections.Generic;

namespace SaxoInterview2.Domain
{
    public interface ICalculatorFacade
    {
        List<string> RunCommands(List<string> commands);
        List<string> RunCommandsParallelly(List<string> commands);
    }
}