// <copyright file="IBrick.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide collection of brick parts.
    /// </summary>
    /// <inheritdoc cref="IPoint"/>
    public interface IBrick
    {
        /// <summary>
        /// Gets or sets id of the brick.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets collection of brick parts.
        /// </summary>
        List<IPoint> Parts { get; set; }

        /// <summary>
        /// Bool Add a brick part.
        /// </summary>
        /// <param name="x">Part Point X.</param>
        /// <param name="y">Part Point Y.</param>
        /// <returns>True for added part.</returns>
        bool AddPart(int x, int y);

        /// <summary>
        /// Check is a correct brick part.
        /// </summary>
        /// <param name="x">Part Point X.</param>
        /// <param name="y">Part Point Y.</param>
        /// <returns>True for added part.</returns>
        bool IsCorrectPart(int x, int y);
    }
}
