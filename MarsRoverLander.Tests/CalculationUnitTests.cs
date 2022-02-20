using MarsRoverLander.Enums;
using MarsRoverLander.Logic;
using MarsRoverLander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoverLander.Tests
{
    public class CalculationUnitTests
    {
        [Theory]
        [InlineData("3 5 E","RLMR")]
        public void CalculateRover_ShouldAssertTrue_WhenInputValid(string roverCoordinateInput, string roverCommandInput)
        {
            var expectedRover = new Rover
            {
                X = 3,
                Y = 5,
                Direction = CompassPoint.East,
                RoverCommandList = new List<RoverCommand> {RoverCommand.Right, RoverCommand.Left, RoverCommand.Move, RoverCommand.Right }
            };

            var inputLogic = new InputLogic();
            var result = inputLogic.CalculateRover(roverCoordinateInput, roverCommandInput);

            Assert.Equal(expectedRover.X, result.X);
            Assert.Equal(expectedRover.Y, result.Y);
            Assert.Equal(expectedRover.RoverCommandList, result.RoverCommandList);
            Assert.Equal(expectedRover.Direction, result.Direction);
        }

        [Theory]
        [InlineData("3 5")]
        public void CalculatePlateauSize_ShouldAssertTrue_WhenInputValid(string plateauSizeString)
        {
            var expectedPlateau = new Plateau(3, 5);

            var inputLogic = new InputLogic();
            var result = inputLogic.CalculatePlateauSize(plateauSizeString);

            Assert.Equal(expectedPlateau.X, result.X);
            Assert.Equal(expectedPlateau.Y, result.Y);
            Assert.Equal(expectedPlateau.TotalGridCount, result.TotalGridCount);
        }
    }
}
