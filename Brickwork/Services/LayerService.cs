// <copyright file="LayerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
        /// Gets or sets all bricks.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private List<IBrick> Bricks { get; set; }

        /// <summary>
        /// Get layer state.
        /// </summary>
        /// <returns>Return layer state.</returns>
        public List<List<int>> GetLayerState()
        {
            return this.Layer.State;
        }

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
            this.Bricks = new List<IBrick>();
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
            if (currentLayerWidth == this.Layer.X)
            {
                throw new ArgumentOutOfRangeException(ErrMsg.LayerIsFull, new Exception());
            }

            // Validate input row
            var rowNumbers = this.GetAllRowNumbers(inputArgsStr);

            this.AddBricksPart(rowNumbers);

            return true;
        }

        /// <summary>
        /// Build layer from input.
        /// </summary>
        /// <returns>True if layer row is added.</returns>
        /// <exception cref="ArgumentException">Throw when can not build the layer.</exception>
        public string BuildLayer()
        {
            var lRow = this.GetLayerRows();
            var lCol = this.GetLayerColumns();
            var state = Enumerable.Range(0, lRow).Select(i => new int[lCol].ToList()).ToList();
            var tmpLayer = new Layer(lRow, lCol, state);
            lRow--;
            lCol--;
            var canBuildLayer = Func(this.Layer, lRow, lCol, tmpLayer, lRow, lCol);

            var nextState = canBuildLayer.State;

            var result = canBuildLayer.State.Any(row => row.Any(col => col == 0));

            return result ? "-1" : "Ok";
        }

        private static ILayer Func(ILayer layer, int lRow, int lCol, ILayer tmpLayer, int tmpRow, int tmpCol)
        {
            if (lRow < 0 || tmpRow < 0)
            {
                return tmpLayer;
            }

            if (lCol < 0)
            {
                lCol = layer.State[0].Count - 1;
                var previousRow = lRow - 1;
                return Func(layer, previousRow, lCol, tmpLayer, tmpRow, tmpCol);
            }

            if (tmpCol < 0)
            {
                tmpCol = tmpLayer.State[0].Count - 1;
                var previousRow = tmpRow - 1;
                return Func(layer, lRow, lCol, tmpLayer, previousRow, tmpCol);
            }

            return FindPossiblePosition(layer, lRow, lCol, tmpLayer, tmpRow, tmpCol);
        }

        private static ILayer FindPossiblePosition(ILayer layer, int lRow, int lCol, ILayer tmpLayer, int tmpRow, int tmpCol)
        {
            var maxRow = layer.State.Count;
            var maxCol = layer.State[0].Count;

            // Check if the build layer possition is free
            if (tmpLayer.State[tmpRow][tmpCol] != 0)
            {
                var previousCol = tmpCol - 1;
                return Func(layer, lRow, lCol, tmpLayer, tmpRow, previousCol);
            }

            // TODO Check Step for lRow and lCol

            //// Maybe it is not possible
            //// Check under is Empty or Exist
            //var isUnder = lRow + 1 < maxRow
            //    && layer.State[lRow][lCol] != layer.State[lRow + 1][lCol]
            //    && tmpRow + 1 < maxRow
            //    && tmpLayer.State[tmpRow + 1][tmpCol] == 0;

            //if (isUnder)
            //{
            //    tmpLayer.State[tmpRow + 1][tmpCol] = layer.State[lRow][lCol];
            //    tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

            //    return Func(layer, lRow, --lCol, tmpLayer, tmpRow, --tmpCol);
            //}

            //// Maybe it is not possible
            //// Check right is Empty or Exist
            //var isRight = lCol + 1 < maxCol
            //    && layer.State[lRow][lCol] != layer.State[lRow][lCol + 1]
            //    && tmpCol + 1 < maxCol
            //    && tmpLayer.State[tmpRow][tmpCol + 1] == 0;
            //if (isRight)
            //{
            //    tmpLayer.State[tmpRow][tmpCol + 1] = layer.State[lRow][lCol];
            //    tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];
            //    return Func(layer, lRow, lCol, tmpLayer, --tmpRow, --tmpCol);
            //}

            // Check left is Empty or Exist
            var isLeft = tmpCol > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow][tmpCol - 1]
                && tmpLayer.State[tmpRow][tmpCol - 1] == 0;
            if (isLeft)
            {
                tmpLayer.State[tmpRow][tmpCol - 1] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];
                var steps = GetLayerSteps(layer, lRow, lCol);

                return Func(layer, lRow, lCol - steps, tmpLayer, tmpRow, tmpCol - GeneralConstants.MaxBrickParts);
            }

            // Check up is Empty or Exist
            var isUp = tmpRow > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow - 1][tmpCol]
                && tmpLayer.State[tmpRow - 1][tmpCol] == 0;

            if (isUp)
            {
                tmpLayer.State[tmpRow - 1][tmpCol] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

                var steps = GetLayerSteps(layer, lRow, lCol);

                return Func(layer, lRow, lCol - steps, tmpLayer, tmpRow, tmpCol - GeneralConstants.MinBrickParts);
            }

            return Func(layer, lRow, lCol - GeneralConstants.MaxBrickParts, tmpLayer, tmpRow, tmpCol - GeneralConstants.MaxBrickParts);
        }

        private static int GetLayerSteps(ILayer layer, int lRow, int lCol)
        {
            var steps = GeneralConstants.MinBrickParts;
            if (lCol > 0 && layer.State[lRow][lCol] == layer.State[lRow][lCol - 1])
            {
                steps++;
            }

            return steps;
        }

        private bool AddBricksPart(List<int> rowNumbers)
        {
            for (int i = 0; i < rowNumbers.Count; i++)
            {
                var id = rowNumbers[i];
                var brick = this.Bricks.FirstOrDefault(b => b.Id == id) != null ? this.Bricks.FirstOrDefault(b => b.Id == id) : new Brick();
                var row = this.Layer.State.Count;
                brick.Id = brick.Id != 0 ? brick.Id : id;
                brick.AddPart(row, i);
                this.Bricks.Add(brick);
            }

            this.Layer.State.Add(rowNumbers);

            return true;
        }

        private List<int> GetAllRowNumbers(string inputArgsStr)
        {
            // Validate written numbers in a row
            this.ValidateRowPattern(inputArgsStr);

            var separators = new char[] { ' ' };
            var args = inputArgsStr
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            // Validate row written numbers range
            this.ValidateRowWrittenNumbersRange(args);

            // Validate brick
            this.ValidBricksPart(args);

            return args;
        }

        private bool ValidBricksPart(List<int> args)
        {
            var lastEnteredRow = this.Layer.State.Count;

            for (int i = 0; i < args.Count; i++)
            {
                var checkId = args[i];

                if (lastEnteredRow > 0)
                {
                    var aboveId = this.Layer.State[lastEnteredRow - 1][i];
                    var aboveBrick = this.Bricks.FirstOrDefault(b => b.Id == aboveId);
                    if (aboveBrick.Parts.Count < GeneralConstants.MaxBrickParts && checkId != aboveId)
                    {
                        var errMsg = string.Format(ErrMsg.BrickPartAboveMustMatch, args[i], i, aboveId);
                        throw new ArgumentException(errMsg);
                    }
                }

                var brick = this.Bricks.FirstOrDefault(b => b.Id == checkId);
                if (brick != null)
                {
                    var isCorrectPart = brick.IsCorrectPart(lastEnteredRow, i);

                    if (!isCorrectPart)
                    {
                        var errMsg = string.Format(ErrMsg.BrickPartMissMatch, args[i], i);
                        throw new ArgumentException(errMsg);
                    }
                }
            }

            return true;
        }

        private void ValidateRowPattern(string inputArgsStr)
        {
            var maxNumber = this.GetLayerColumns();
            var repeatStr = new StringBuilder(RegXPattern.RowNumbers.Length * maxNumber).Insert(0, RegXPattern.RowNumbers, maxNumber).ToString();
            var pattern = $@"^[\W]*{repeatStr.ToString().Trim()}$";
            if (!Regex.IsMatch(inputArgsStr, pattern))
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, 1, this.Layer.TargetBricks);
                throw new ArgumentException(errMsg);
            }
        }

        private bool ValidateRowWrittenNumbersRange(List<int> args)
        {
            var maxBrickNumber = this.GetLayerTargetBrickCount();
            var isAnyOutOfRange = args.Any(element => element < GeneralConstants.MinBrickNumber || element > maxBrickNumber);

            if (isAnyOutOfRange)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeNumberInLine, this.GetLayerColumns(), GeneralConstants.MinBrickNumber, this.GetLayerTargetBrickCount());
                throw new ArgumentOutOfRangeException(errMsg);
            }

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
