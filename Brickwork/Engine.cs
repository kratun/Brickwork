// <copyright file="Engine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Brickwork.Common.Constants;
    using Brickwork.Services;

    /// <summary>
    /// Provide method to run game.
    /// </summary>
    /// <inheritdoc cref="IEngine"/>
    public class Engine : IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            this.WriteService = new WriteService();
            this.ReadService = new ReadService();
            this.LayerService = new LayerService();
        }

        /// <summary>
        /// Service for writing text.
        /// </summary>
        /// <inheritdoc cref="IWrite"/>
        private IWrite WriteService { get; set; }

        /// <summary>
        /// Service for reading text.
        /// </summary>
        /// <inheritdoc cref="IRead"/>
        private IRead ReadService { get; set; }

        /// <summary>
        /// Gets or sets properties and methods that used to create and recalculating layer.
        /// </summary>
        private ILayerService LayerService { get; set; }

        /// <summary>
        /// Method that run game.
        /// </summary>
        /// <exception cref="Exception">Can throw exception.</exception>
        public void Run()
        {
            while (true)
            {
                try
                {
                    var result = string.Empty;

                    result = this.TryGetLayerDeimensions();
                    if (result == GeneralConstants.RestartGame)
                    {
                        continue;
                    }
                    else if (result == GeneralConstants.EndGame)
                    {
                        return;
                    }

                    result = this.CreateLayer();
                    if (result == GeneralConstants.RestartGame)
                    {
                        continue;
                    }
                    else if (result == GeneralConstants.EndGame)
                    {
                        return;
                    }

                    result = this.BuildLayer();

                    this.WriteResult(result);
                }
                catch (Exception e)
                {
                    if ((e is ArgumentException) || (e is ArgumentOutOfRangeException))
                    {
                        Console.WriteLine(e);
                        continue;
                    }

                    throw e;
                }

                var wantToProceed = CommonService.WantToProceed();

                if (!wantToProceed)
                {
                    break;
                }

                this.LayerService.Reset();
            }
        }

        private string BuildLayer()
        {
            this.WriteService.WriteLine(string.Format(GeneralConstants.WaitCalculations));

            var result = this.LayerService.BuildLayer();

            return result;
        }

        private void WriteResult(string result)
        {
            this.WriteService.WriteLine(result);
            //var layerState = this.LayerService.GetLayerState();

            //for (int i = 0; i < layerState.Count; i++)
            //{
            //    this.WriteService.WriteLine(string.Join(", ", layerState[i]));
            //}
        }

        private string CreateLayer()
        {
            var layerColumns = this.LayerService.GetLayerColumns();
            var layerRows = this.LayerService.GetLayerRows();

            var layerTargetBrickCount = this.LayerService.GetLayerTargetBrickCount();

            this.WriteService.WriteLine(string.Format(GeneralConstants.EnterLayer, layerRows, layerColumns, GeneralConstants.MinBrickParts, layerTargetBrickCount));

            for (int i = 0; i < layerRows; i++)
            {
                try
                {
                    this.WriteService.Write(string.Format(GeneralConstants.EnterLayerRow, i + 1));
                    var inputArgsStr = this.ReadService.ReadLine();

                    if (inputArgsStr.ToLower() == GeneralConstants.EndGame)
                    {
                        return GeneralConstants.EndGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RestartGame)
                    {
                        return GeneralConstants.RestartGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RepeatProcess)
                    {
                        i = GeneralConstants.StartPositionIndex;

                        inputArgsStr = $"{layerRows} {layerColumns}";
                        this.LayerService.GetLayerDimensions(inputArgsStr);

                        continue;
                    }

                    this.LayerService.AddLayerRow(inputArgsStr);
                }
                catch (ArgumentException e)
                {
                    this.WriteService.WriteLine(e.Message);
                    i--;
                }
            }

            return string.Empty;
        }

        private string TryGetLayerDeimensions()
        {
            while (true)
            {
                this.WriteService.Write(GeneralConstants.EnterLayerDimensions, GeneralConstants.MaxLayerSize.ToString());

                try
                {
                    var inputArgsStr = this.ReadService.ReadLine();
                    if (inputArgsStr.ToLower() == GeneralConstants.EndGame)
                    {
                        return GeneralConstants.EndGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RestartGame)
                    {
                        return GeneralConstants.RestartGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RepeatProcess)
                    {
                        continue;
                    }

                    this.LayerService.GetLayerDimensions(inputArgsStr);
                    return string.Empty;
                }
                catch (Exception e)
                {
                    if (e is ArgumentException)
                    {
                        this.WriteService.WriteLine(e.Message);
                        throw e;
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }
    }
}
