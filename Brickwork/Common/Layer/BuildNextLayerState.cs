// <copyright file="BuildNextLayerState.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Common.Layer
{
    using System.Collections.Generic;
    using System.Linq;

    using Brickwork.Common.Constants;
    using Brickwork.Services;

    /// <summary>
    /// Provides static methods to build next layer state.
    /// </summary>
    /// <inheritdoc cref="ILayerService"/>
    public static class BuildNextLayerState
    {
        /// <summary>
        /// Static method that run built proccess.
        /// </summary>
        /// <param name="currentState">Current Layer state from the input.</param>
        /// <param name="lRow">Current Layer rows from the input.</param>
        /// <param name="lCol">Current Layer columns from the input.</param>
        /// <returns>Return Built layer state.</returns>
        public static List<List<int>> Run(List<List<int>> currentState, int lRow, int lCol)
        {
            var nextLayerState = Enumerable.Range(0, lRow).Select(i => new int[lCol].ToList()).ToList();

            lRow--;
            lCol--;

            return Func(currentState, lRow, lCol, nextLayerState, lRow, lCol);
        }

        /// <summary>
        /// Static method that traversing the current layer state from back to begining.
        /// </summary>
        /// <param name="currentState">Current Layer state from the input.</param>
        /// <param name="lRow">Current Layer row possition.</param>
        /// <param name="lCol">Current Layer column possition.</param>
        /// <param name="nextLayerState">Next Layer state.</param>
        /// <param name="nextLayerRow">Next Layer row possition.</param>
        /// <param name="nextLayerCol">Next Layer column possition.</param>
        /// <returns>Returns next layer state.</returns>
        private static List<List<int>> Func(List<List<int>> currentState, int lRow, int lCol, List<List<int>> nextLayerState, int nextLayerRow, int nextLayerCol)
        {
            if (lRow < 0 || nextLayerRow < 0)
            {
                return nextLayerState;
            }

            if (lCol < 0)
            {
                lCol = currentState[0].Count - 1;
                var previousRow = lRow - 1;
                return Func(currentState, previousRow, lCol, nextLayerState, nextLayerRow, nextLayerCol);
            }

            if (nextLayerCol < 0)
            {
                nextLayerCol = nextLayerState[0].Count - 1;
                var previousRow = nextLayerRow - 1;
                return Func(currentState, lRow, lCol, nextLayerState, previousRow, nextLayerCol);
            }

            return FindPossiblePosition(currentState, lRow, lCol, nextLayerState, nextLayerRow, nextLayerCol);
        }

        /// <summary>
        /// Static method that set the brick in the next layer state.
        /// </summary>
        /// <param name="currentState">Current Layer state from the input.</param>
        /// <param name="lRow">Current Layer row possition.</param>
        /// <param name="lCol">Current Layer column possition.</param>
        /// <param name="nextLayerState">Next Layer state.</param>
        /// <param name="nextLayerRow">Next Layer row possition.</param>
        /// <param name="nextLayerCol">Next Layer column possition.</param>
        /// <returns>Returns next layer state.</returns>
        private static List<List<int>> FindPossiblePosition(List<List<int>> currentState, int lRow, int lCol, List<List<int>> nextLayerState, int nextLayerRow, int nextLayerCol)
        {
            // Check if brick is already added
            var brickId = currentState[lRow][lCol];
            var isBrickAdded = nextLayerState.Any(line => line.Any(el => el == brickId));
            if (isBrickAdded)
            {
                var previousCol = lCol - 1;
                return Func(currentState, lRow, previousCol, nextLayerState, nextLayerRow, nextLayerCol);
            }

            // Check if the build currentState possition is free
            if (nextLayerState[nextLayerRow][nextLayerCol] != 0)
            {
                var previousCol = nextLayerCol - 1;
                return Func(currentState, lRow, lCol, nextLayerState, nextLayerRow, previousCol);
            }

            // Check brick rotation Left to left
            var isBrickLeftToLeft = nextLayerCol > 0
                && currentState[nextLayerRow][nextLayerCol] != currentState[nextLayerRow][nextLayerCol - 1]
                && nextLayerState[nextLayerRow][nextLayerCol - 1] == 0
                && lCol > 0
                && currentState[lRow][lCol] == currentState[lRow][lCol - 1];

            if (isBrickLeftToLeft)
            {
                nextLayerState[nextLayerRow][nextLayerCol - 1] = currentState[lRow][lCol];
                nextLayerState[nextLayerRow][nextLayerCol] = currentState[lRow][lCol];

                return Func(currentState, lRow, lCol - GeneralConstants.MaxBrickParts, nextLayerState, nextLayerRow, nextLayerCol - GeneralConstants.MaxBrickParts);
            }

            // Check brick rotation Up to left
            var isBrickUpToLeft = nextLayerCol > 0
                && currentState[nextLayerRow][nextLayerCol] != currentState[nextLayerRow][nextLayerCol - 1]
                && nextLayerState[nextLayerRow][nextLayerCol - 1] == 0
                && lRow > 0
                && currentState[lRow][lCol] == currentState[lRow - 1][lCol];

            if (isBrickUpToLeft)
            {
                nextLayerState[nextLayerRow][nextLayerCol - 1] = currentState[lRow][lCol];
                nextLayerState[nextLayerRow][nextLayerCol] = currentState[lRow][lCol];

                return Func(currentState, lRow, lCol - GeneralConstants.MinBrickParts, nextLayerState, nextLayerRow, nextLayerCol - GeneralConstants.MaxBrickParts);
            }

            // Check brick rotation Left to Up
            var isBrickLeftToUp = nextLayerRow > 0
                && currentState[nextLayerRow][nextLayerCol] != currentState[nextLayerRow - 1][nextLayerCol]
                && nextLayerState[nextLayerRow - 1][nextLayerCol] == 0
                && lCol > 0
                && currentState[lRow][lCol] == currentState[lRow][lCol - 1];

            if (isBrickLeftToUp)
            {
                nextLayerState[nextLayerRow - 1][nextLayerCol] = currentState[lRow][lCol];
                nextLayerState[nextLayerRow][nextLayerCol] = currentState[lRow][lCol];

                return Func(currentState, lRow, lCol - GeneralConstants.MaxBrickParts, nextLayerState, nextLayerRow, nextLayerCol - GeneralConstants.MinBrickParts);
            }

            // Check brick rotation Up to Up
            var isBrickUpToUp = nextLayerRow > 0
                && currentState[nextLayerRow][nextLayerCol] != currentState[nextLayerRow - 1][nextLayerCol]
                && nextLayerState[nextLayerRow - 1][nextLayerCol] == 0
                && lRow > 0
                && currentState[lRow][lCol] == currentState[lRow - 1][lCol];

            if (isBrickUpToUp)
            {
                nextLayerState[nextLayerRow - 1][nextLayerCol] = currentState[lRow][lCol];
                nextLayerState[nextLayerRow][nextLayerCol] = currentState[lRow][lCol];

                return Func(currentState, lRow, lCol - GeneralConstants.MinBrickParts, nextLayerState, nextLayerRow - GeneralConstants.MinBrickParts, nextLayerCol);
            }

            return Func(currentState, lRow, lCol - GeneralConstants.MaxBrickParts, nextLayerState, nextLayerRow, nextLayerCol - GeneralConstants.MaxBrickParts);
        }
    }
}
