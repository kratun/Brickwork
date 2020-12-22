// <copyright file="LayerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Brickwork.Common.Constants;
    using Brickwork.Common.Layer;
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
        /// <returns>True if correct.</returns>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        public bool GetLayerDimensions(string inputArgsStr)
        {
            var result = this.TryGetPoint(inputArgsStr);
            this.Layer = new Layer(result.X, result.Y);
            return true;
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
        /// <returns>Return built layer as string.</returns>
        /// <exception cref="ArgumentException">Throw when can not build the layer.</exception>
        public string BuildLayer()
        {
            var lRow = this.GetLayerRows();
            var lCol = this.GetLayerColumns();

            var builtLayerState = BuildNextLayerState.Run(this.Layer.State, lRow, lCol);

            var hasEmptySlot = builtLayerState.Any(row => row.Any(col => col == 0));
            var finalState = builtLayerState;
            var result = this.ConvertLayerStateToString(finalState);

            return hasEmptySlot ? GeneralConstants.NoSolutionExist : result;
        }

        /// <summary>
        /// Convert layer state to a string.
        /// </summary>
        /// <param name="state"> State that hold list of interegrs.</param>
        /// <returns>Returns a single string.</returns>
        private string ConvertLayerStateToString(List<List<int>> state)
        {
            var fillFinalState = this.FillFinalState(state);

            var result = string.Join(Environment.NewLine, fillFinalState.Select(line => string.Join(string.Empty, line)));
            return result;
        }

        /// <summary>
        /// Create final layer state.
        /// </summary>
        /// <param name="state"> State that hold list of interegrs.</param>
        /// <returns>Return Lists of strings.</returns>
        private List<List<string>> FillFinalState(List<List<int>> state)
        {
            var maxRow = state.Count;
            var maxCol = state[0].Count;
            var lRow = (this.GetLayerRows() * 2) + 1;
            var lCol = (this.GetLayerColumns() * 2) + 1;
            var finalState = Enumerable.Range(0, lRow).Select(i => new string[lCol].ToList()).ToList();

            for (int i = 0; i < maxRow; i++)
            {
                for (int k = 0; k < maxCol; k++)
                {
                    this.FillSurroundedArea(state, finalState, i, k, maxRow - 1, maxCol - 1);
                }
            }

            return finalState;
        }

        /// <summary>
        /// Method that fill surrounded area.
        /// </summary>
        /// <param name="state">Current layer state.</param>
        /// <param name="finalState">Inital final state.</param>
        /// <param name="i"> Current layer row.</param>
        /// <param name="k">Current layer column.</param>
        /// <param name="maxRow">Maximum current layer rows.</param>
        /// <param name="maxCol">Maximum current layer columns.</param>
        private void FillSurroundedArea(List<List<int>> state, List<List<string>> finalState, int i, int k, int maxRow, int maxCol)
        {
            var fRow = i * 2;
            var fCol = k * 2;
            var endRow = fRow + GeneralConstants.SurroundedBrickBorderLength;
            var endCol = fCol + GeneralConstants.SurroundedBrickBorderLength;

            for (int row = fRow; row < endRow; row++)
            {
                for (int col = fCol; col < endCol; col++)
                {
                    if (row == fRow + 1 && col == fCol + 1)
                    {
                        finalState[row][col] = state[i][k].ToString();
                        continue;
                    }

                    var isPartLeft = k < maxCol && state[i][k] == state[i][k + 1] && (col == fCol + 2 && row == fRow + 1);
                    var isPartRight = k > 0 && state[i][k] == state[i][k - 1] && (col == fCol && row == fRow + 1);
                    var isPartUp = i < maxRow && state[i][k] == state[i + 1][k] && (row == fRow + 2 && col == fCol + 1);
                    var isPartUnder = i > 0 && state[i][k] == state[i - 1][k] && (row == fRow && col == fCol + 1);

                    if (!isPartLeft && !isPartRight && !isPartUp && !isPartUnder)
                    {
                        finalState[row][col] = GeneralConstants.DrawSymbol;
                    }
                    else
                    {
                        finalState[row][col] = GeneralConstants.BrickBetweenPartsSymbol;
                    }
                }
            }
        }

        /// <summary>
        /// Add all brick part numbers in layer state and all brick part in coresponded brick in the list of bricks.
        /// </summary>
        /// <param name="partNumbers">List of all brick part number in the input.</param>
        /// <returns>Return true if added.</returns>
        private bool AddBricksPart(List<int> partNumbers)
        {
            var tempBricks = new List<IBrick>(this.Bricks.ToArray());
            for (int i = 0; i < partNumbers.Count; i++)
            {
                var id = partNumbers[i];
                var brick = tempBricks.FirstOrDefault(b => b.Id == id) != null ? tempBricks.FirstOrDefault(b => b.Id == id) : new Brick();
                var row = this.Layer.State.Count;
                brick.Id = brick.Id != 0 ? brick.Id : id;
                brick.AddPart(row, i);
                tempBricks.Add(brick);
            }

            if (this.Layer.X == this.Layer.State.Count)
            {
                var hasNotFinishedBrick = tempBricks.Any(br => br.Parts.Count != GeneralConstants.MaxBrickParts);

                if (hasNotFinishedBrick)
                {
                    this.Bricks = tempBricks;
                    throw new ArgumentException("Not finished bricks");
                }
            }

            this.Bricks = tempBricks;
            this.Layer.State.Add(partNumbers);

            return true;
        }

        /// <summary>
        /// Get all brick part nymber from input string.
        /// </summary>
        /// <param name="inputArgsStr">Row that contains all brick parts.</param>
        /// <returns>Return list of brick part numbers. </returns>
        private List<int> GetAllRowNumbers(string inputArgsStr)
        {
            var maxBrickNumber = this.GetLayerTargetBrickCount();
            var layerColumns = this.GetLayerColumns();

            // Validate written numbers in a row
            LayerValidations.BrickPartsRowPattern(inputArgsStr, layerColumns, maxBrickNumber);

            var separators = new char[] { ' ' };
            var args = inputArgsStr
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            // Validate row written numbers range
            LayerValidations.RowWrittenNumbersRange(args, maxBrickNumber, layerColumns);

            // Validate brick
            var state = this.GetLayerState();
            LayerValidations.BricksPart(args, state, this.Bricks);

            return args;
        }

        /// <summary>
        /// Try get two numbers from string.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        private IPoint TryGetPoint(string inputArgsStr)
        {
            // Validate first line that contain two argument
            LayerValidations.LayerDimensionsStr(inputArgsStr, GeneralConstants.LayerDimension);

            var separators = new char[] { ' ' };
            var args = inputArgsStr
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            LayerValidations.Dimensions(args);

            var dimensionRows = int.Parse(args[0]);

            int dimensionColumns = int.Parse(args[1]);

            var point = new Point(dimensionRows, dimensionColumns);

            return point;
        }
    }
}
