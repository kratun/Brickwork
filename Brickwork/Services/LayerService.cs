// <copyright file="LayerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Brickwork.Common.Constants;
    using Brickwork.Common.Validations;
    using Brickwork.Models;

    /// <summary>
    /// Provides properties and methods that create and recalculating layer N times.
    /// </summary>
    /// <inheritdoc cref="ILayerService"/>
    public class LayerService : ILayerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayerService"/> class.
        /// </summary>
        public LayerService()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets or sets current layer state.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private ILayer Layer { get; set; }

        /// <summary>
        /// Get layer X (width) and Y (height).
        /// </summary>
        /// <param name="inputArgsStr">A string contains two integers separated by space.</param>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        public void GetLayerDimensions(string inputArgsStr)
        {
            var result = this.TryGetPoint(inputArgsStr);
            this.Layer = new Layer(result.X, result.Y);
        }

        /// <summary>
        /// Gets layer columns (width).
        /// </summary>
        /// <returns>Layer columns.</returns>
        public int GetLayerColumns()
        {
            return this.Layer.Y;
        }

        /// <summary>
        /// Gets layer rows (height).
        /// </summary>
        /// <returns>Layer rows.</returns>
        public int GetLayerRows()
        {
            return this.Layer.X;
        }

        /// <summary>
        /// Gets layer target brick count.
        /// </summary>
        /// <returns>Layer target brick count.</returns>
        public int GetLayerTargetBrickCount()
        {
            return this.Layer.TargetBricks;
        }

        /// <summary>
        /// Reset game.
        /// </summary>
        public void Reset()
        {
            this.Layer = new Layer();
        }

        /// <summary>
        /// Add layer row from the input.
        /// </summary>
        /// <param name="inputArgsStr">Layer row - numbers separarated by space.</param>
        /// <returns>True if layer row is added.</returns>
        /// <exception cref="ArgumentException">Throw when input contains not allowed charakters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the row constains less/more characters or the layer is full.</exception>
        public bool AddLayerRow(string inputArgsStr)
        {
            var currentLayerWidth = this.Layer.State.Count;
            if (currentLayerWidth == this.Layer.Y)
            {
                throw new ArgumentOutOfRangeException(ErrMsg.LayerIsFull, new Exception());
            }

            var errMsg = string.Empty;

            // Validate allowed digits in layer
            if (!Regex.IsMatch(inputArgsStr, RegXPattern.RowNumbers))
            {
                errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, 1, this.Layer.TargetBricks);
                throw new ArgumentException(errMsg);
            }

            var separators = new char[] { ' ' };
            var args = inputArgsStr
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            // Validate row width
            if (args.Count != this.Layer.Y)
            {
                errMsg = string.Format(ErrMsg.NotCorrectDigitsCount, currentLayerWidth + 1, inputArgsStr, this.Layer.X, GeneralConstants.MinBrickNumber);
                throw new ArgumentOutOfRangeException(errMsg, new Exception());
            }

            this.Layer.State.Add(args);

            return true;
        }

        /// <summary>
        /// Try get two numbers from string.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        private IPoint TryGetPoint(string inputArgsStr)
        {
            var separators = new char[] { ' ' };

            var args = inputArgsStr
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var errMsg = string.Empty;

            // Validate first line that contain two argument
            errMsg = ErrMsg.LayerDimentionException;
            var inputArgsPattern = RegXPattern.LayerDimensionLine;
            this.ValidateInputArgsStr(inputArgsStr, GeneralConstants.LayerDimension, args, errMsg, inputArgsPattern);

            // Validate that rows is intereger
            errMsg = string.Format(ErrMsg.NotCorrectRow, GeneralConstants.MaxLayerSize);
            int rows = this.TryGetIntValue(args[0], errMsg);

            // Validate that Height is intereger
            errMsg = string.Format(ErrMsg.NotCorrectColumn, GeneralConstants.MaxLayerSize);
            int columns = this.TryGetIntValue(args[1], errMsg);

            var point = new Point(rows, columns);

            return point;
        }

        /// <summary>
        /// Try convert string to integer.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown integer is out of range.</exception>
        private int TryGetIntValue(string arg, string errMsg)
        {
            int value;
            if (!int.TryParse(arg, out value) || value % 2 == 1)
            {
                throw new ArgumentException(errMsg);
            }

            if (value < GeneralConstants.MinLayerSize || value >= GeneralConstants.MaxLayerSize)
            {
                throw new ArgumentOutOfRangeException(errMsg);
            }

            return value;
        }

        private bool ValidateInputArgsStr(string inputArgsStr, int validArgsCount, string[] args, string errMsg, string inputArgsPattern)
        {
            if (!Regex.IsMatch(inputArgsStr, inputArgsPattern) || args.Length != validArgsCount)
            {
                throw new ArgumentException(errMsg);
            }

            return true;
        }
    }
}
