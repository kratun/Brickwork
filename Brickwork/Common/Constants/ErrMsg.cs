// <copyright file="ErrMsg.cs" company="PlaceholderCompany">
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
        public const string NotAllowedCharacterInLine = "Error: There are not allowed charecters in line. You should write 4 numbers less between {0} and {1}";

        /// <summary>
        /// Error: Please write width and height of the layer in pattern "width, height".
        /// </summary>
        public const string LayerDimentionException = "Error: Please write width and height of the layer in pattern \"width, height\"";

        /// <summary>
        /// Error: Width must be an integer between MIN and MAX and can not be greater then height - "width, height".
        /// </summary>
        public const string NotCorrectRow = "Error: Rows must be a whole even positive number less than {0} - \"rows columns\"";

        /// <summary>
        /// Error: Height must be an integer between MIN and MAX.
        /// </summary>
        public const string NotCorrectColumn = "Error: Columns must be a whole even positive number less than {0} - \"rows columns\"";

        /// <summary>
        /// Error: The line must contains digits equal to the Layer Width digits (1 for green or 0 for red) without space between.
        /// </summary>
        public const string NotCorrectDigitsCount = "Error: The line {0} - \"{1}\" must contains {2} digits ({3} for green or {4} for red) without space between";

        /// <summary>
        /// Error: Layer is full. It is not possible to enter more rows.
        /// </summary>
        public const string LayerIsFull = "Error: Layer is full. It is not possible to enter more rows.";
    }
}
