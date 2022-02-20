using MarsRoverLander.Enums;
using System.Collections.Generic;

namespace MarsRoverLander.Models
{
    public class Rover
    {
        public CompassPoint Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<RoverCommand> RoverCommandList { get; set; }
        public int Number { get; set; }

        public void Turn(RoverCommand command, int count)
        {
            if (command == RoverCommand.Right)
            {
                Direction = (CompassPoint)(((int)(Direction + count)) % 4);
            }
            else if (command == RoverCommand.Left)
            {
                Direction = (CompassPoint)(((int)(Direction - count + 4)) % 4);
            }
        }

        public void Move()
        {
            switch (Direction)
            {
                case CompassPoint.North:
                    Y++;
                    break;
                case CompassPoint.East:
                    X++;
                    break;
                case CompassPoint.South:
                    Y--;
                    break;
                case CompassPoint.West:
                    X--;
                    break;
                default:
                    break;
            }
        }
    }
}