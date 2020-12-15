// <copyright file="ILayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide properties X (width) and Y (height) and collection for layer state.
    /// </summary>
    /// <inheritdoc cref="IPoint"/>
    public interface ILayer : IPoint
    {
        /// <summary>
        /// Gets layer target bricks.
        /// </summary>
        int TargetBricks { get; }

        /// <summary>
        /// Gets or sets collection for layer state.
        /// </summary>
        List<List<int>> State { get; set; }
    }
}
