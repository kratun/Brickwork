﻿// <copyright file="ErrMsg.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Common.Constants
{
    /// <summary>
    /// Static Class ErrMsg contains all error messages.
    /// </summary>
    public static class ErrMsg
    {
        /// <summary>
        /// Error: There are not allowed charecters in N-th line.
        /// </summary>
        public const string NotAllowedCharacterInLine = "Error: There are not enougth or not allowed charecters in line. You should write {0} numbers between {1} and {2}";

        /// <summary>
        /// Error: There are not allowed charecters in N-th line.
        /// </summary>
        public const string OutOfRangeNumberInLine = "Error: Some of the entered numbers are out of range. Please write {0} numbers between {1} and {2}";

        /// <summary>
        /// Error: Brick part does not match with the above ones.
        /// </summary>
        public const string BrickPartAboveMustMatch = "Error: Incorrect brick number {0} at position {1}. The number of this position must be {2}.";

        /// <summary>
        /// Error: Brick part does not match with the above ones.
        /// </summary>
        public const string BrickPartWrongPosition = "Error: Incorrect brick number {0} at position {1}.";

        /// <summary>
        /// Error: The above row contains brick part on the position witch not match.
        /// </summary>
        public const string BrickPartWrongPositionAboveRow = "Error: Incorrect brick number {0} at position {1}. It must be at position {2}.";

        /// <summary>
        /// Error: The row contains brick part on a wrong position.
        /// </summary>
        public const string BrickPartWrongPositionSameRow = "Error: Incorrect brick number at position {0}. The number must be {1}.";

        /// <summary>
        /// Error: The brick with that Id has all parts.
        /// </summary>
        public const string BrickWithThatIdHasAllParts = "Error: Incorrect brick number at position {0}. The brick with Part number: {1} has all needed parts.";

        /// <summary>
        /// Error: Brick part does not match.
        /// </summary>
        public const string BrickPartMissMatch = "Error: Incorrect brick number {0} at position {1}. Please write correct one.";

        /// <summary>
        /// Error: Please write width and height of the layer in pattern "width, height".
        /// </summary>
        public const string LayerDimentionException = "Error: Please write width and height of the layer in pattern \"rows columns\"";

        /// <summary>
        /// Error: Width must be an integer between MIN and MAX and can not be greater then height - "width, height".
        /// </summary>
        public const string NotCorrectRow = "Error: Entered numbers must be a whole even positive number less than {0} - \"rows columns\"";

        /// <summary>
        /// Error: Height must be an integer between MIN and MAX.
        /// </summary>
        public const string NotCorrectColumn = "Error: Columns must be a whole even positive number less than {0} - \"rows columns\"";

        /// <summary>
        /// Error: The line must contains digits equal to the Layer Width digits (1 for green or 0 for red) without space between.
        /// </summary>
        public const string NotCorrectDigitsCount = "Error: The line {0} - \"{1}\" must contains {2} numbers separated by space";

        /// <summary>
        /// Error: Layer is full. It is not possible to enter more rows.
        /// </summary>
        public const string LayerIsFull = "Error: Layer is full. It is not possible to enter more rows.";
    }
}
