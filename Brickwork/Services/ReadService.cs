// <copyright file="ReadService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System;

    /// <summary>
    /// Provide read methods.
    /// </summary>
    /// <inheritdoc cref="IRead"/>
    public class ReadService : IRead
    {
        /// <summary>
        /// Read a single line.
        /// </summary>
        /// <returns>Read a single line and return it's trimmed value.</returns>
        public string ReadLine()
        {
            return Console.ReadLine().Trim();
        }
    }
}
