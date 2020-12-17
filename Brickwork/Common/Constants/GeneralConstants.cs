// <copyright file="GeneralConstants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Common.Constants
{
    /// <summary>
    /// Static Class GeneralConstants contains all constants except error constants.
    /// </summary>
    public static class GeneralConstants
    {
        /// <summary>
        /// MinBrickNumber is an integer and hold constant for minimum brick number.
        /// </summary>
        public const int MinBrickNumber = 1;

        // Layer dimensions

        /// <summary>
        /// EnterLayerDimensions is an string for message: "Enter layer dimensions "row, column": ".
        /// </summary>
        public const string EnterLayerDimensions = "Enter layer dimensions \"rows and columns\" separated by space (both must be an even positive number less than {0}): ";

        /// <summary>
        /// EnterLayer is an string for message: "Please enter on each next {X-th} lines, {Y-th} digits({GreenNumber} for green or {RedNumber} for red)".
        /// </summary>
        public const string EnterLayer = "Please enter on the next {0} lines, {1} brick. Each brick has two equal numbers. Possible numbers between {2} and {3}";

        /// <summary>
        /// EnterLayerRow is an string for message: "Enter line currentLine}: ".
        /// </summary>
        public const string EnterLayerRow = "Enter line {0}: ";

        /// <summary>
        /// LayerDimension is an integer and hold constant for count of the layer dimensions.
        /// </summary>
        public const int LayerDimension = 2;

        /// <summary>
        /// MinLayerSize is an integer and hold constant for minimum size of the layer.
        /// </summary>
        public const int MinLayerSize = 2;

        /// <summary>
        /// MaxLayerSize is an integer and hold constant for maximum size of the layer.
        /// </summary>
        public const int MaxLayerSize = 100;

        /// <summary>
        /// MaxBrickParts is an integer and hold constant for maximum brick parts.
        /// </summary>
        public const int MaxBrickParts = 2;

        /// <summary>
        /// MinBrickParts is an integer and hold constant for miniimum brick parts.
        /// </summary>
        public const int MinBrickParts = 1;

        /// <summary>
        /// EnterTargetConditions is a constant string for message: "All inputs are correct.".
        /// </summary>
        public const string CorrectArgsStr = "All inputs are correct.";

        /// <summary>
        /// ExpectedResult is a constant string used to show the result of the game.
        /// </summary>
        public const string ExpectedResult = "# expected result: ";

        // Want to proceed "Please wait for calculations!"

        /// <summary>
        /// WaitCalculations is a constant string for message: "Please wait calculations!".
        /// </summary>
        public const string WaitCalculations = "Please wait. Layer is under construction!";

        /// <summary>
        /// WantToProceedStr is a constant string for message: "Do you want to proceed? (Yes/No)".
        /// </summary>
        public const string WantToProceedStr = "Do you want to proceed? (Yes/No)";

        /// <summary>
        /// Yes is a constant string for answer: "yes".
        /// </summary>
        public const string Yes = "yes";

        /// <summary>
        /// No is a constant string for answer: "no".
        /// </summary>
        public const string No = "no";

        /// <summary>
        /// Repeat is a constant string used to start add rows lyaers from the beginning.
        /// </summary>
        public const string RepeatProcess = "repeat";

        /// <summary>
        /// Restart is a constant string to restart game.
        /// </summary>
        public const string RestartGame = "restart";

        /// <summary>
        /// End is a constant string to canceling game.
        /// </summary>
        public const string EndGame = "end";

        /// <summary>
        /// Repeat is a constant string if you want tot start game again.
        /// </summary>
        public const int StartPositionIndex = -1;
    }
}
