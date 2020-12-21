// <copyright file="Brick.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide properties Brick.
    /// </summary>
    /// <inheritdoc cref="Point"/>
    public class Brick : IBrick
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        public Brick()
        {
            this.Parts = new List<IPoint>();
        }

        /// <summary>
        /// Gets or sets id of the brick.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets collection of brick parts.
        /// </summary>
        public List<IPoint> Parts { get; set; }

        /// <summary>
        /// Gets or sets collection of brick parts.
        /// </summary>
        /// <param name="x">Part Point X.</param>
        /// <param name="y">Part Point Y.</param>
        /// <returns>True for added part.</returns>
        public bool AddPart(int x, int y)
        {
            var part = new Point(x, y);
            this.Parts.Add(part);

            return true;
        }
    }
}
