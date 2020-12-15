// <copyright file="IWrite.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    /// <summary>
    /// Provide methods that write.
    /// </summary>
    public interface IWrite
    {
        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="outputMsg">The value to write.</param>
        void Write(string outputMsg);

        /// <summary>
        /// Writes the specified string value to the standard output stream with two args.
        /// </summary>
        /// <param name="outputMsg">The value to write pattern.</param>
        /// <param name="arg">Arg fill pattern.</param>
        void Write(string outputMsg, string arg);

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to
        ///     the standard output stream.
        /// </summary>
        /// <param name="outputMsg">The value to write.</param>
        void WriteLine(string outputMsg);
    }
}