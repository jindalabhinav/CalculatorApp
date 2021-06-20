using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaxoInterview2;
using Xunit;

namespace SaxoInterview2Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_Success_Test()
        {
            //Arrange
            string input = "Add 1,2,3,4,5";
            string output;

            //Act
            output = Calculator.Execute(input);

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
            output = Calculator.Execute(input);

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
            output = await Calculator.ExecuteAsync(input);

            //Assert
            Assert.Equal("15", output);
        }

        [Fact]
        public async Task ExecuteBatch_Success_TestAsync()
        {
            //Arrange
            List<string> input = new List<string>() { "Add 1,2,3,4,5", "Mult 1,2,3,4,5" };
            List<string> output;

            //Act
            output = await Calculator.ExecuteBatch(input);

            //Assert
            Assert.Equal("15", output[0]);
            Assert.Equal("120", output[1]);
        }
    }
}