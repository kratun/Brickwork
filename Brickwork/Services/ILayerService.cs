// <copyright file="ILayerService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods that create and recalculate layer N times.
    /// </summary>
    public interface ILayerService
    {
        /// <summary>
        /// Get layer X (width) and Y (height).
        /// </summary>
        /// <param name="inputArgsStr">A string contains two integers separated by space.</param>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        void GetLayerDimensions(string inputArgsStr);

        /// <summary>
        /// Gets layer columns (width).
        /// </summary>
        /// <returns>Layer columns.</returns>
        int GetLayerColumns();

        /// <summary>
        /// Gets layer rows (height).
        /// </summary>
        /// <returns>Layer rows.</returns>
        int GetLayerRows();

        /// <summary>
        /// Gets layer target brick count.
        /// </summary>
        /// <returns>Layer target brick count.</returns>
        int GetLayerTargetBrickCount();

        /// <summary>
        /// Reset game.
        /// </summary>
        void Reset();

        /// <summary>
        /// Create layer row from input.
        /// </summary>
        /// <param name="inputArgsStr">Layer row as string.</param>
        /// <returns>True if layer row is added.</returns>
        /// <exception cref="ArgumentException">Throw when input contains not allowed charakters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the row constains less/more characters or the layer is full.</exception>
        bool AddLayerRow(string inputArgsStr);

        /// <summary>
        /// Get layer state.
        /// </summary>
        /// <returns>Return layer state.</returns>
        List<List<int>> GetLayerState();

        /// <summary>
        /// Build layer if possible.
        /// </summary>
        /// <returns> The built layer result.</returns>
        string BuildLayer();
    }
}