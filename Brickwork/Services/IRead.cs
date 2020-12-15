// <copyright file="IRead.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    /// <summary>
    /// Provide read methods.
    /// </summary>
    public interface IRead
    {
        /// <summary>
        /// Read a single line.
        /// </summary>
        /// <returns>Read a single line and return it's trimmed value.</returns>
        string ReadLine();
    }
}