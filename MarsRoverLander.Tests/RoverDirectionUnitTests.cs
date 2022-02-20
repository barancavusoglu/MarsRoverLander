using MarsRoverLander.Enums;
using MarsRoverLander.Models;
using Xunit;

namespace MarsRoverLander.Tests
{
    public class RoverDirectionUnitTests
    {
        [Theory]
        [InlineData(RoverCommand.Left, 2, CompassPoint.North, CompassPoint.South)]
        [InlineData(RoverCommand.Right, 9, CompassPoint.North, CompassPoint.East)]
        [InlineData(RoverCommand.Left, 0, CompassPoint.North, CompassPoint.North)]
        public void ValidateInputType_ShouldAssertTrue_WhenInputValid(RoverCommand command, int count, CompassPoint initialDirection, CompassPoint expectedDirection)
        {
            var rover = new Rover();
            rover.Direction = initialDirection;
            rover.Turn(command, count);

            Assert.Equal(expectedDirection, rover.Direction);
        }
    }
}
