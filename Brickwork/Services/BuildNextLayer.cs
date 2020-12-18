// <copyright file="BuildNextLayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System.Linq;
    using Brickwork.Common.Constants;
    using Brickwork.Models;

    /// <summary>
    /// Provides static methods to build layer.
    /// </summary>
    /// <inheritdoc cref="ILayerService"/>
    public static class BuildNextLayer
    {
        /// <summary>
        /// Startic method that run built proccess.
        /// </summary>
        /// <param name="layer">Layer from input.</param>
        /// <returns>Return Built layer.</returns>
        public static ILayer Run(ILayer layer)
        {
            int lRow = layer.X;
            int lCol = layer.Y;

            var state = Enumerable.Range(0, lRow).Select(i => new int[lCol].ToList()).ToList();
            var tmpLayer = new Layer(lRow, lCol, state);
            lRow--;
            lCol--;

            return Func(layer, lRow, lCol, tmpLayer, lRow, lCol);
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
            // Check if brick is already added
            var brickId = layer.State[lRow][lCol];
            var isBrickAdded = tmpLayer.State.Any(line => line.Any(el => el == brickId));
            if (isBrickAdded)
            {
                var previousCol = lCol - 1;
                return Func(layer, lRow, previousCol, tmpLayer, tmpRow, tmpCol);
            }

            // Check if the build layer possition is free
            if (tmpLayer.State[tmpRow][tmpCol] != 0)
            {
                var previousCol = tmpCol - 1;
                return Func(layer, lRow, lCol, tmpLayer, tmpRow, previousCol);
            }

            // Check brick rotation Left to left
            var isBrickLeftToLeft = tmpCol > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow][tmpCol - 1]
                && tmpLayer.State[tmpRow][tmpCol - 1] == 0
                && lCol > 0
                && layer.State[lRow][lCol] == layer.State[lRow][lCol - 1];

            if (isBrickLeftToLeft)
            {
                tmpLayer.State[tmpRow][tmpCol - 1] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

                return Func(layer, lRow, lCol - GeneralConstants.MaxBrickParts, tmpLayer, tmpRow, tmpCol - GeneralConstants.MaxBrickParts);
            }

            // Check brick rotation Up to left
            var isBrickUpToLeft = tmpCol > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow][tmpCol - 1]
                && tmpLayer.State[tmpRow][tmpCol - 1] == 0
                && lRow > 0
                && layer.State[lRow][lCol] == layer.State[lRow - 1][lCol];

            if (isBrickUpToLeft)
            {
                tmpLayer.State[tmpRow][tmpCol - 1] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

                return Func(layer, lRow, lCol - GeneralConstants.MinBrickParts, tmpLayer, tmpRow, tmpCol - GeneralConstants.MaxBrickParts);
            }

            // Check brick rotation Left to Up
            var isBrickLeftToUp = tmpRow > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow - 1][tmpCol]
                && tmpLayer.State[tmpRow - 1][tmpCol] == 0
                && lCol > 0
                && layer.State[lRow][lCol] == layer.State[lRow][lCol - 1];

            if (isBrickLeftToUp)
            {
                tmpLayer.State[tmpRow - 1][tmpCol] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

                return Func(layer, lRow, lCol - GeneralConstants.MaxBrickParts, tmpLayer, tmpRow, tmpCol - GeneralConstants.MinBrickParts);
            }

            // Check brick rotation Up to Up
            var isBrickUpToUp = tmpRow > 0
                && layer.State[tmpRow][tmpCol] != layer.State[tmpRow - 1][tmpCol]
                && tmpLayer.State[tmpRow - 1][tmpCol] == 0
                && lRow > 0
                && layer.State[lRow][lCol] == layer.State[lRow - 1][lCol];

            if (isBrickUpToUp)
            {
                tmpLayer.State[tmpRow - 1][tmpCol] = layer.State[lRow][lCol];
                tmpLayer.State[tmpRow][tmpCol] = layer.State[lRow][lCol];

                return Func(layer, lRow, lCol - GeneralConstants.MinBrickParts, tmpLayer, tmpRow - GeneralConstants.MinBrickParts, tmpCol);
            }

            return Func(layer, lRow, lCol - GeneralConstants.MaxBrickParts, tmpLayer, tmpRow, tmpCol - GeneralConstants.MaxBrickParts);
        }
    }
}
