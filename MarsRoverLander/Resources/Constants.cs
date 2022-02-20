using System.Text.RegularExpressions;

namespace MarsRoverLander.Resources
{
    public static class Constants
    {
        #region INPUT TYPE
        public const string INPUT_TYPE_CONSOLE = "C";
        public const string INPUT_TYPE_FILE = "F";
        #endregion

        #region INPUT COMMAND
        public const string INPUT_END_SYMBOL = "X";
        public const string LEFT = "L";
        public const string RIGHT = "R";
        public const string MOVE = "M";
        #endregion

        #region COMPASS POINT
        public const string NORTH = "N";
        public const string EAST = "E";
        public const string WEST = "W";
        public const string SOUTH = "S";
        #endregion

        #region VALIDATION PATTERN
        public const RegexOptions REGEX_OPTIONS = RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
        public const string VALIDATION_PATTERN_PLATEU_SIZE = @"^[0-9]{0,4}\s[0-9]{0,4}$"; // min [0 0] // max [9999 9999]
        public const string VALIDATION_PATTERN_DIRECTION_COMMAND = @"^[LRM]{0,5000}$"; // LRMLMRLM
        public const string VALIDATION_PATTERN_INPUT_TYPE = @"^[CF]$"; // C
        public const string VALIDATION_PATTERN_ROVER_COORDINATE = @"^[0-9]{0,4}\s[0-9]{0,4}\s[NEWS]$"; // min [0 0 N] // max [9999 9999 N]
        public const char SPLIT_CHARACTER = ' ';
        #endregion

        public const string FILE_NAME = "mars.txt";
    }
}
