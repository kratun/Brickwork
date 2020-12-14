// <copyright file="IEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork
{
    using System;

    /// <summary>
    /// Provide method to run game.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Method that run game.
        /// </summary>
        /// <exception cref="Exception">Can throw exception.</exception>
        void Run();
    }
}
