// <copyright file="GetLayerDimensionsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Brickwork.Tests
{
    using System;
    using Brickwork.Common.Constants;
    using Brickwork.Services;
    using Xunit;

    public class GetLayerDimensionsTests
    {
        [Fact]
        public void GetLayerDimensionsWorks()
        {
            var validRowsNumber = 2;
            var validColsNumber = 4;

            var inputArgsStr = $"{validRowsNumber} {validColsNumber}";

            var layerService = new LayerService();

            var result = layerService.GetLayerDimensions(inputArgsStr);

            Assert.Equal(validRowsNumber, layerService.GetLayerRows());
            Assert.True(result);
        }

        [Fact]
        public void GetLayerDimensionsReturnArgumentExceptionNotEnterColums()
        {
            var validRowsNumber = 4;

            var inputArgsStr = $"{validRowsNumber} ";

            var layerService = new LayerService();
            try
            {
                layerService.GetLayerDimensions(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = ErrMsg.LayerDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void GetLayerDimensionsReturnArgumentExceptionNotEnterRows()
        {
            var validColsNumber = 4;

            var inputArgsStr = $" {validColsNumber}";

            var layerService = new LayerService();
            try
            {
                layerService.GetLayerDimensions(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = ErrMsg.LayerDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void GetLayerDimensionsReturnArgumentExceptionEnterNotInteger()
        {
            var validRowsNumber = "NotInteger";
            var validColsNumber = 4;

            var inputArgsStr = $"{validRowsNumber} {validColsNumber}";

            var layerService = new LayerService();
            try
            {
                layerService.GetLayerDimensions(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = ErrMsg.LayerDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void GetLayerDimensionsReturnArgumentExceptionWrongRows()
        {
            var validRowsNumber = 3;
            var validColsNumber = 4;

            var inputArgsStr = $"{validRowsNumber} {validColsNumber}";

            var layerService = new LayerService();
            try
            {
                layerService.GetLayerDimensions(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectRow, GeneralConstants.MaxLayerSize);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void GetLayerDimensionsArgumentOutOfRangeExceptionIfRowBelowMin()
        {
            var validRowsNumber = 0;
            var validColsNumber = 4;

            var inputArgsStr = $"{validRowsNumber} {validColsNumber}";

            var layerService = new LayerService();
            try
            {
                layerService.GetLayerDimensions(inputArgsStr);
            }
            catch (ArgumentOutOfRangeException e)
            {
                var errorsMsg = e;
                var errMsg = string.Format(ErrMsg.NotCorrectRow, GeneralConstants.MaxLayerSize);
                Assert.Equal(errMsg, e.ParamName);
            }
        }
    }
}
