using MarsRoverLander.Enums;
using MarsRoverLander.Logic;
using MarsRoverLander.Models;
using System.Collections.Generic;
using Xunit;

namespace MarsRoverLander.Tests
{
    public class LandRoverUnitTests
    {
        [Fact]
        public void CalculateRover_ShouldAssertTrue()
        {
            var rover = new Rover
            {
                X = 3,
                Y = 5,
                Direction = CompassPoint.North,
                RoverCommandList = new List<RoverCommand> { RoverCommand.Left, RoverCommand.Move, RoverCommand.Right, RoverCommand.Move, RoverCommand.Move }
            };

            var expectedRover = new Rover
            {
                X = 2,
                Y = 7,
                Direction = CompassPoint.North
            };

            var landingLogic = new LandingLogic();
            var result = landingLogic.LandRover(rover);

            Assert.Equal(expectedRover.X, result.X);
            Assert.Equal(expectedRover.Y, result.Y);
            Assert.Equal(expectedRover.Direction, result.Direction);
        }
    }
}
