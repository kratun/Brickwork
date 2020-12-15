// <copyright file="Layer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide properties X (width) and Y (height) and collection for layer state.
    /// </summary>
    /// <inheritdoc cref="Point"/>
    /// <inheritdoc cref="ILayer"/>
    public class Layer : Point, ILayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        public Layer()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        /// <param name="x">Layer X (width).</param>
        /// <param name="y">Layer Y (heught).</param>
        public Layer(int x, int y)
            : base(x, y)
        {
            this.TargetBricks = x * y / 2;
            this.State = new List<List<int>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        /// <param name="x">Layer X (width).</param>
        /// <param name="y">Layer Y (heught).</param>
        /// <param name="state">Layer collection.</param>
        public Layer(int x, int y, List<List<int>> state)
            : this(x, y)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets layer target bricks.
        /// </summary>
        public int TargetBricks { get; private set; }

        /// <summary>
        /// Gets or sets collection of greens and reds.
        /// </summary>
        public List<List<int>> State { get; set; }
    }
}
