// <copyright file="CommonService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Services
{
    using System;
    using Brickwork.Common.Constants;

    /// <summary>
    /// Provides static methods for gameplay.
    /// </summary>
    public static class CommonService
    {
        /// <summary>
        /// Ask "Do you want to proceed?" and wainting for response "Yes" or "No".
        /// </summary>
        /// <returns>True or false.</returns>
        public static bool WantToProceed()
        {
            var wantToProceed = false;
            while (true)
            {
                Console.WriteLine(GeneralConstants.WantToProceedStr);
                var answer = Console.ReadLine().Trim();
                var answerToLower = answer.ToLower();

                if (answerToLower == GeneralConstants.Yes)
                {
                    wantToProceed = true;
                    break;
                }

                if (answerToLower == GeneralConstants.No)
                {
                    wantToProceed = false;
                    break;
                }
            }

            return wantToProceed;
        }
    }
}
