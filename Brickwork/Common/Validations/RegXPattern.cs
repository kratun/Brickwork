// <copyright file="RegXPattern.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Common.Validations
{
    /// <summary>
    /// RegXPattern is a static class holds constant string used for regular expressions.
    /// </summary>
    public static class RegXPattern
    {
        /// <summary>
        /// FirstLine is a constant string pattern for layer dimensions input.
        /// </summary>
        public const string LayerDimensionLine = @"^[\W]*[0-9]+[\W]*[0-9]+[\W]*$";

        /// <summary>
        /// AllowedDigitsInLayer is a constant string pattern for layer row input.
        /// </summary>
        public const string RowNumbers = @"^[\W]*[0-9]{1,2}[\W]*[0-9]{1,2}[\W]*[0-9]{1,2}[\W]*[0-9]{1,2}[\W]*$";
    }
}
