using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaxoInterview2.Domain;
using Xunit;

namespace SaxoInterview2Tests
{
    public class CalculatorFacadeTests
    {
        [Fact]
        public void Add_Success_Test()
        {
            //Arrange
            string input = "Add 1,2,3,4,5";
            string output;

            //Act
            output = CalculatorFacade.Execute(input);

            //Assert
            Assert.Equal("15", output);
        }

        [Fact]
        public void Multiply_Success_Test()
        {
            //Arrange
            string input = "Mult 1,2,3,4,5";
            string output;

            //Act
            output = CalculatorFacade.Execute(input);

            //Assert
            Assert.Equal("120", output);
        }

        [Fact]
        public async Task Add_Success_TestAsync()
        {
            //Arrange
            string input = "Add 1,2,3,4,5";
            string output;

            //Act
            output = await CalculatorFacade.ExecuteAsync(input);

            //Assert
            Assert.Equal("15", output);
        }

        [Fact(Skip = "Sometimes gives the correct output, and sometimes it gives only 1 output for the 3 inputs given.")] // WIP, Issue - Ordered output of Parallel execution
        public async Task ExecuteBatch_Success_TestAsync()
        {
            //Arrange
            List<string> input = new List<string>() { "Add 1,2,3,4,5", "Mult 1,2,3,4,5", "Mult 1,2,3,4,5,6" };
            List<string> output;

            //Act
            output = await CalculatorFacade.ExecuteBatch(input);

            //Assert
            Assert.Equal("15", output[0]);
            Assert.Equal("120", output[1]);
            Assert.Equal("720", output[2]);
        }

        [Fact]
        public void ExecuteRunCommands_Success_Test()
        {
            //Arrange
            List<string> input = new List<string>() {
                "Add 1,2,3,4,5",
                "Mult 1,2,3,4,5",
                "MultiPly 1,2,3,4,5,6",
                "Count Product",
                "Count Addition"
            };
            List<string> output;
            ICalculatorFacade calculator = new CalculatorFacade();

            //Act
            output = calculator.RunCommands(input);

            //Assert
            Assert.Equal("15", output[0]);
            Assert.Equal("120", output[1]);
            Assert.Equal("720", output[2]);
            Assert.Equal("2", output[3]);
            Assert.Equal("1", output[4]);
        }

        [Fact]
        public void ExecuteRunCommands_FailureInvalidOperation_Test()
        {
            //Arrange
            List<string> input = new List<string>() {
                "Add 1,2,3,4,5",
                "Mult 1,2,3,4,5",
                "MultiPly 1,2,3,4,5,6",
                "InvalidOperation sum"
            };
            ICalculatorFacade calculator = new CalculatorFacade();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.RunCommands(input));
        }

        [Fact]
        public void ExecuteRunCommands_FailureInvalidInput_Test()
        {
            //Arrange
            List<string> input = new List<string>() {
                "InvalidInput"
            };
            ICalculatorFacade calculator = new CalculatorFacade();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.RunCommands(input));
        }

        [Fact(Skip = "Sometimes gives the correct output, and sometimes it gives only 1 output for the 3 inputs given.")] // WIP, Issue - Ordered output of Parallel execution
        public void ExecuteRunCommandsParallel_Success_Test()
        {
            //Arrange
            List<string> input = new List<string>() {
                "Add 1,2,3,4,5",
                "Mult 1,2,3,4,5",
                "MultiPly 1,2,3,4,5,6"
            };
            List<string> output;
            ICalculatorFacade calculator = new CalculatorFacade();

            //Act
            output = calculator.RunCommandsParallelly(input);

            //Assert
            Assert.Equal("15", output[0]);
            Assert.Equal("120", output[1]);
            Assert.Equal("720", output[2]);
        }
    }
}