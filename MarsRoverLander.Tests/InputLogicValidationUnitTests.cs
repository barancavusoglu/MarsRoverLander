using MarsRoverLander.Enums;
using MarsRoverLander.Logic;
using Xunit;

namespace MarsRoverLander.Tests
{
    public class InputLogicValidationUnitTests
    {
        [Theory]
        [InlineData("C")]
        [InlineData("F")]
        [InlineData("f")]
        public void ValidateInputType_ShouldAssertTrue_WhenInputValid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.InputType);
            Assert.True(result);
        }

        [Theory]
        [InlineData("C ")]
        [InlineData(" F")]
        [InlineData("X")]
        public void ValidateInputType_ShouldAssertFalse_WhenInputInvalid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.InputType);
            Assert.False(result);
        }

        [Theory]
        [InlineData("RRMLRM")]
        [InlineData("mRlMrlm")]
        [InlineData("R")]
        public void ValidateRoverCommand_ShouldAssertTrue_WhenInputValid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.RoverCommand);
            Assert.True(result);
        }

        [Theory]
        [InlineData("RRMLXRM")]
        [InlineData("R ")]
        public void ValidateRoverCommand_ShouldAssertFalse_WhenInputInvalid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.RoverCommand);
            Assert.False(result);
        }

        [Theory]
        [InlineData("5 5 N")]
        [InlineData("0 0 S")]
        [InlineData("3 5 W")]
        [InlineData("9999 9999 E")]
        public void ValidateRoverCoordinate_ShouldAssertTrue_WhenInputValid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.RoverCoordinate);
            Assert.True(result);
        }

        [Theory]
        [InlineData("5 5N")]
        [InlineData("00S")]
        [InlineData("3 5 W ")]
        [InlineData("10000 9999 E")]
        public void ValidateRoverCoordinate_ShouldAssertFalse_WhenInputInvalid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.RoverCoordinate);
            Assert.False(result);
        }

        [Theory]
        [InlineData("5 5")]
        [InlineData("0 0")]
        [InlineData("3 5")]
        [InlineData("9999 9999")]
        public void ValidatePlateuSize_ShouldAssertTrue_WhenInputValid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.PlateauSize);
            Assert.True(result);
        }

        [Theory]
        [InlineData("5")]
        [InlineData("")]
        [InlineData("5 5 ")]
        [InlineData("99991 9999")]
        public void ValidatePlateuSize_ShouldAssertFalse_WhenInputInvalid(string input)
        {
            var inputLogic = new InputLogic();
            var result = inputLogic.Validate(input, ValidationType.PlateauSize);
            Assert.False(result);
        }
    }
}
