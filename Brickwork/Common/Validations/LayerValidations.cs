// <copyright file="LayerValidations.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Common.Validations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Brickwork.Common.Constants;
    using Brickwork.Models;

    /// <summary>
    /// Provides static methods that validate LayerService.
    /// </summary>
    internal static class LayerValidations
    {
        /// <summary>
        /// Validate Brick parts.
        /// </summary>
        /// <param name="args">Integer list that contains brick parts.</param>
        /// <param name="state">Current layer state.</param>
        /// <param name="bricks">List of IBrick taht contains add brick parts.</param>
        /// <exception cref="ArgumentException">If one of the bick part in args is not correct part.</exception>
        /// <returns>Return true if args are correct.</returns>
        internal static bool BricksPart(List<int> args, List<List<int>> state, List<IBrick> bricks)
        {
            var lastEnteredRow = state.Count;
            var tmpBricks = new List<IBrick>(bricks.ToArray());

            for (int i = 0; i < args.Count; i++)
            {
                var checkId = args[i];
                var countSameBrickPartsInArgs = args.Where(bp => bp == checkId).ToList().Count;
                var existingBrick = tmpBricks.FirstOrDefault(b => b.Id == checkId);
                var errMsg = string.Empty;

                if (existingBrick == null)
                {
                    if (lastEnteredRow > 0)
                    {
                        var aboveId = state[lastEnteredRow - 1][i];
                        var aboveBrick = tmpBricks.FirstOrDefault(br => br.Id == aboveId);

                        errMsg = aboveBrick.IsCorrectPart(checkId, lastEnteredRow, i);
                        if (errMsg != null)
                        {
                            throw new ArgumentException(errMsg);
                        }
                    }

                    existingBrick = new Brick();
                    existingBrick.AddPart(lastEnteredRow, i);
                    existingBrick.Id = checkId;
                    tmpBricks.Add(existingBrick);
                }
                else
                {
                    errMsg = existingBrick.IsCorrectPart(checkId, lastEnteredRow, i);
                    if (errMsg != null)
                    {
                        throw new ArgumentException(errMsg);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Validate input row.
        /// </summary>
        /// <param name="inputArgsStr">Input string to validate.</param>
        /// <param name="layerColumns">Valid input args count.</param>
        /// <param name="maxBrickNumber">Maximum number that make arg valid. </param>
        /// <returns>Return true if input row is valid.</returns>
        internal static bool BrickPartsRowPattern(string inputArgsStr, int layerColumns, int maxBrickNumber)
        {
            var repeatStr = new StringBuilder(RegXPattern.RowNumbers.Length * layerColumns).Insert(0, RegXPattern.RowNumbers, layerColumns).ToString();
            var pattern = $@"^[\W]*{repeatStr.ToString().Trim()}$";
            if (!Regex.IsMatch(inputArgsStr, pattern))
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, layerColumns, 1, maxBrickNumber);
                throw new ArgumentException(errMsg);
            }

            return true;
        }

        /// <summary>
        /// Validate brick parts are in range.
        /// </summary>
        /// <param name="args">List of brick parts numbers.</param>
        /// <param name="maxBrickNumber">Maximum possible brick part number.</param>
        /// <param name="layerColumns">Maximum brick parts in row.</param>
        /// <returns>Return true if valid.</returns>
        internal static bool RowWrittenNumbersRange(List<int> args, int maxBrickNumber, int layerColumns)
        {
            var isAnyOutOfRange = args.Any(element => element < GeneralConstants.MinBrickNumber || element > maxBrickNumber);

            if (isAnyOutOfRange)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeNumberInLine, layerColumns, GeneralConstants.MinBrickNumber, maxBrickNumber);
                throw new ArgumentOutOfRangeException(errMsg);
            }

            return true;
        }

        /// <summary>
        /// Validate layer dimensions string.
        /// </summary>
        /// <param name="layerDimensionsStr">Layer dimensions as input string.</param>
        /// <param name="validArgsCount">Integer for valid layer dimensions.</param>
        /// <exception cref="ArgumentException">The exception is thrown when layerDimensionsStr did not match pattern.</exception>
        /// <returns>Return true if is valid.</returns>
        internal static bool LayerDimensionsStr(string layerDimensionsStr, int validArgsCount)
        {
            var errMsg = ErrMsg.LayerDimentionException;
            var repeatStr = new StringBuilder(RegXPattern.RowNumbers.Length * validArgsCount).Insert(0, RegXPattern.RowNumbers, validArgsCount).ToString();
            var pattern = $@"^[\W]*{repeatStr.ToString().Trim()}$";

            if (!Regex.IsMatch(layerDimensionsStr, pattern))
            {
                throw new ArgumentException(errMsg);
            }

            return true;
        }

        /// <summary>
        /// Validate Layer dimension.
        /// </summary>
        /// <param name="args">List of dimensions.</param>
        /// <exception cref="ArgumentException">The exception is thrown when one of arguments is not integer.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The exception is thrown when one of arguments is not in range.</exception>
        /// <returns>Return true if valid.</returns>
        internal static bool Dimensions(List<string> args)
        {
            var errMsg = string.Format(ErrMsg.NotCorrectRow, GeneralConstants.MaxLayerSize);
            for (int i = 0; i < args.Count; i++)
            {
                if (!int.TryParse(args[i], out int value) || value % 2 == 1)
                {
                    throw new ArgumentException(errMsg);
                }

                var intValue = value;
                if (intValue < GeneralConstants.MinLayerSize || intValue >= GeneralConstants.MaxLayerSize)
                {
                    throw new ArgumentOutOfRangeException(errMsg);
                }
            }

            return true;
        }

        /// <summary>
        /// Extention method for IBrick. Check is a correct brick part.
        /// </summary>
        /// <param name="x">Part Point X.</param>
        /// <param name="y">Part Point Y.</param>
        /// <returns>Returns error or empty string is id correct.</returns>
        private static string IsCorrectPart(this IBrick checkedBrick, int id, int x, int y)
        {
            if (checkedBrick.Parts.Count >= GeneralConstants.MaxBrickParts && checkedBrick.Id == id)
            {
                return string.Format(ErrMsg.BrickWithThatIdHasAllParts, y, checkedBrick.Id);
            }

            if (checkedBrick.Parts.Count == GeneralConstants.MinBrickParts)
            {
                var existingPart = checkedBrick.Parts[0];
                var canProceed = (existingPart.Y == y && existingPart.X + 1 == x && checkedBrick.Id == id)
                    || (existingPart.X == x && existingPart.Y + 1 == y && checkedBrick.Id == id);

                if (!canProceed)
                {
                    return string.Format(ErrMsg.BrickPartWrongPosition, id, y);
                }

                if (existingPart.Y == y && existingPart.X + 1 != x && checkedBrick.Id != id)
                {
                    return string.Format(ErrMsg.BrickPartAboveMustMatch, id, y, checkedBrick.Id);
                }

                if (existingPart.X == x && existingPart.Y + 1 != y && checkedBrick.Id != id)
                {
                    return string.Format(ErrMsg.BrickPartWrongPositionSameRow, y, checkedBrick.Id);
                }
            }

            return null;
        }

        /// <summary>
        /// Extention method for IBrick. Has all brick parts.
        /// </summary>
        /// <returns>Returns true if has all brick parts.</returns>
        private static bool HasAllParts(this IBrick checkedBrick)
        {
            return checkedBrick.Parts.Count == GeneralConstants.MaxBrickParts;
        }
    }
}
