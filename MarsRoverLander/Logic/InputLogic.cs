using MarsRoverLander.Enums;
using MarsRoverLander.Models;
using MarsRoverLander.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MarsRoverLander.Logic
{
    public class InputLogic
    {
        public void GetInputType()
        {
            var inputType = GetConsoleInput(ValidationType.InputType, Messages.ENTER_INPUT_TYPE);

            if (inputType.Equals(Constants.INPUT_TYPE_CONSOLE, StringComparison.InvariantCultureIgnoreCase)) // TODO 
            {
                ReadConsole();
            }
            else if (inputType.Equals(Constants.INPUT_TYPE_FILE, StringComparison.InvariantCultureIgnoreCase))
            {
                ReadFile();
            }
        }

        public void ReadConsole()
        {
            Console.WriteLine();
            var plateauSizeInput = GetConsoleInput(ValidationType.PlateauSize, Messages.ENTER_PLATEAU_SIZE);
            var plateau = CalculatePlateauSize(plateauSizeInput);

            Console.WriteLine();
            Console.WriteLine(Messages.THERE_ARE_NUM_GRIDS_AVAILABLE.Replace(Messages.NUM_SELECTOR, plateau.TotalGridCount.ToString()));
            Console.WriteLine(Messages.PRESS_X_FOR_ENDING);

            var roverList = new List<Rover>();
            var landingLogic = new LandingLogic();

            do
            {
                try
                {
                    var roverCoordinateInput = GetConsoleInput(ValidationType.RoverCoordinate, Messages.ENTER_ROVER_COORDINATES.Replace(Messages.NUM_SELECTOR, (roverList.Count + 1).ToString()), true);
                    var roverCommandInput = GetConsoleInput(ValidationType.RoverCommand, Messages.ENTER_ROVER_COMMANDS.Replace(Messages.NUM_SELECTOR, (roverList.Count + 1).ToString()), true);
                    var rover = CalculateRover(roverCoordinateInput, roverCommandInput);

                    rover.Number = roverList.Count + 1;
                    roverList.Add(rover);
                }
                catch (EndInputException)
                {
                    break;
                }
            } while (roverList.Count < plateau.TotalGridCount);

            landingLogic.CalcuateLandings(plateau, roverList);
        }

        public string GetConsoleInput(ValidationType validationType, string message, bool cancellationEnabled = false)
        {
            bool isValid;
            do
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();

                if (cancellationEnabled && input.Equals(Constants.INPUT_END_SYMBOL, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new EndInputException();
                }

                isValid = Validate(input, validationType);

                if (isValid)
                    return input;

            } while (!isValid);
            return string.Empty;
        }

        public string GetFileLine(ValidationType validationType, string input)
        {
            var isValid = Validate(input, validationType);
            if (isValid)
                return input;

            throw new InputFormatException();
        }

        public bool Validate(string input, ValidationType validationType)
        {
            var validationPattern = string.Empty;

            switch (validationType)
            {
                case ValidationType.PlateauSize:
                    validationPattern = Constants.VALIDATION_PATTERN_PLATEU_SIZE;
                    break;
                case ValidationType.RoverCommand:
                    validationPattern = Constants.VALIDATION_PATTERN_DIRECTION_COMMAND;
                    break;
                case ValidationType.InputType:
                    validationPattern = Constants.VALIDATION_PATTERN_INPUT_TYPE;
                    break;
                case ValidationType.RoverCoordinate:
                    validationPattern = Constants.VALIDATION_PATTERN_ROVER_COORDINATE;
                    break;
                default:
                    break;
            }

            Regex regex = new Regex(validationPattern, Constants.REGEX_OPTIONS);
            return regex.IsMatch(input);
        }

        public void ReadFile()
        {
            string[] lines = File.ReadAllLines(Constants.FILE_NAME);

            try
            {
                var plateauSizeInput = GetFileLine(ValidationType.PlateauSize, lines[0]);

                var plateau = CalculatePlateauSize(plateauSizeInput);
                var roverList = new List<Rover>();

                var landingLogic = new LandingLogic();

                for (int i = 1; i < lines.Length; i += 2)
                {
                    var roverCoordinateInput = GetFileLine(ValidationType.RoverCoordinate, lines[i]);
                    var roverCommandInput = GetFileLine(ValidationType.RoverCommand, lines[i + 1]);

                    var rover = CalculateRover(roverCoordinateInput, roverCommandInput);
                    rover.Number = roverList.Count + 1;
                    roverList.Add(rover);
                }

                landingLogic.CalcuateLandings(plateau, roverList);
            }
            catch (InputFormatException)
            {
                Console.WriteLine(Messages.FILE_FORMAT_ERROR);
            }
        }

        public Plateau CalculatePlateauSize(string plateauSizeString)
        {
            string[] xy = plateauSizeString.Split(Constants.SPLIT_CHARACTER);
            var plateau = new Plateau(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
            return plateau;
        }

        public Rover CalculateRover(string roverCoordinateInput, string roverCommandInput)
        {
            var rover = new Rover();

            #region Coordinates
            string[] coordinatesAndDirection = roverCoordinateInput.Split(Constants.SPLIT_CHARACTER);
            rover.X = Convert.ToInt32(coordinatesAndDirection[0]);
            rover.Y = Convert.ToInt32(coordinatesAndDirection[1]);
            var directionChar = coordinatesAndDirection[2];

            switch (directionChar.ToUpperInvariant())
            {
                case Constants.NORTH:
                    rover.Direction = CompassPoint.North;
                    break;
                case Constants.EAST:
                    rover.Direction = CompassPoint.East;
                    break;
                case Constants.WEST:
                    rover.Direction = CompassPoint.West;
                    break;
                case Constants.SOUTH:
                    rover.Direction = CompassPoint.South;
                    break;
                default:
                    break;
            }
            #endregion

            #region Commands
            rover.RoverCommandList = new List<RoverCommand>();

            foreach (var command in roverCommandInput)
            {
                switch (command.ToString().ToUpperInvariant())
                {
                    case Constants.LEFT:
                        rover.RoverCommandList.Add(RoverCommand.Left);
                        break;
                    case Constants.RIGHT:
                        rover.RoverCommandList.Add(RoverCommand.Right);
                        break;
                    case Constants.MOVE:
                        rover.RoverCommandList.Add(RoverCommand.Move);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return rover;
        }
    }
}
