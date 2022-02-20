using MarsRoverLander.Enums;
using MarsRoverLander.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverLander.Logic
{
    public class LandingLogic
    {
        List<Rover> landedRoverList = new List<Rover>();

        public void CalcuateLandings(Plateau plateau, List<Rover> roverList)
        {
            Console.WriteLine();
            foreach (var rover in roverList)
            {
                LandRover(rover);
                WriteRover(rover, plateau);
            }
            Console.ReadLine();
        }

        public Rover LandRover(Rover rover)
        {
            int moveIndex = -1;
            do
            {
                moveIndex = rover.RoverCommandList.IndexOf(RoverCommand.Move);

                var rightCount = 0;
                var leftCount = 0;

                if (moveIndex != -1) // Move index found
                {
                    rightCount = rover.RoverCommandList.Take(moveIndex).Where(x => x == RoverCommand.Right).Count();
                    leftCount = rover.RoverCommandList.Take(moveIndex).Where(x => x == RoverCommand.Left).Count();
                }
                else if (moveIndex == -1) // Move index not found
                {
                    rightCount = rover.RoverCommandList.Where(x => x == RoverCommand.Right).Count();
                    leftCount = rover.RoverCommandList.Where(x => x == RoverCommand.Left).Count();
                }

                var turnResult = rightCount - leftCount;
                if (turnResult > 0) // Turn right
                {
                    turnResult %= 4;
                    if (turnResult != 0)
                    {
                        rover.Turn(RoverCommand.Right, turnResult);
                    }
                }
                else if (turnResult < 0) // Turn left
                {
                    turnResult = Math.Abs(turnResult) % 4;
                    if (turnResult != 0)
                    {
                        rover.Turn(RoverCommand.Left, turnResult);
                    }
                }

                if (moveIndex != -1) // Move index found
                {
                    rover.Move();
                    rover.RoverCommandList.RemoveRange(0, moveIndex + 1);
                }

            } while (moveIndex != -1);

            return rover;
        }

        private void WriteRover(Rover rover, Plateau plateau)
        {
            bool crashed = false, outOfBounds = false;
            var crashRover = landedRoverList.FirstOrDefault(x => x.X == rover.X && x.Y == rover.Y);
            if (crashRover != null)
            {
                crashed = true;
                Console.WriteLine($"Rover {rover.Number} crashed onto Rover {crashRover.Number} in coordinates {rover.X} {rover.Y} {rover.Direction.ToString()[0]}");
            }

            if (rover.X < 0 || rover.X > plateau.X || rover.Y < 0 || rover.Y > plateau.Y)
            {
                outOfBounds = true;
                Console.WriteLine($"Rover {rover.Number} is out of bounds in coordinates {rover.X} {rover.Y} {rover.Direction.ToString()[0]}");
            }

            if (!crashed && !outOfBounds)
            {
                Console.WriteLine($"{rover.X} {rover.Y} {rover.Direction.ToString()[0]}");
            }
            landedRoverList.Add(rover);
        }
    }
}
