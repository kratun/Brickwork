namespace Brickwork.Tests
{
    using System;
    using Brickwork.Common.Constants;
    using Brickwork.Services;
    using Xunit;

    public class AddLayerRowTests : IDisposable
    {
        public AddLayerRowTests()
        {
            this.Dispose();
        }

        public int LayerRows { get; set; }

        public int LayerCols { get; set; }

        public int MaxBricksNum => this.LayerRows * this.LayerCols / 2;

        ILayerService LayerService { get; set; }

        public void Dispose()
        {
            var row = 2;
            var col = 4;
            this.LayerService = this.InitializeLayerService(row, col);
        }

        [Fact]
        public void Works()
        {
            var inputArgsStr = $"1 1 2 2";

            var result = this.LayerService.AddLayerRow(inputArgsStr);

            Assert.True(result);
        }

        [Fact]
        public void ReturnArgumentExceptionInvalidCharactersInRow()
        {
            try
            {
                var inputArgsStr = "1 1 2 T";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, this.LayerCols, 1, this.MaxBricksNum);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionLessNumbersInRow()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 3 4";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, this.LayerCols, 1, this.MaxBricksNum);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionMoreNumbersInRow()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 3 4 4 5";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, this.LayerCols, 1, this.MaxBricksNum);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentOutOfRangeExceptionBrickPartNumberAbovePossible()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 3 4 10";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentOutOfRangeException e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeNumberInLine, this.LayerCols, GeneralConstants.MinBrickNumber, this.MaxBricksNum);
                Assert.Equal(errMsg, e.ParamName);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionBrickWithEnterdPartIsFull()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 1 4 4";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.BrickWithThatIdHasAllParts, 1, 1); // string.Format(ErrMsg.BrickPartWrongPosition, 1, 1);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionEnteredPartNotMatchWithAbove()
        {
            try
            {
                var inputArgsStr = "1 2 3 4";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 2 1 4";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.BrickPartWrongPosition, 3, 0);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionBrickPartWithSameNumberNotNextEachOther()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 4 3 4";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.BrickPartWrongPosition, 3, 2);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentExceptionBrickPartIsMoreInSameRow()
        {
            try
            {
                var inputArgsStr = "1 1 1 3";
                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentException e)
            {
                var errMsg = string.Format(ErrMsg.BrickPartWrongPosition, 1, 2);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnArgumentOutOfRangeExceptionTryAddMoreRows()
        {
            try
            {
                var inputArgsStr = "1 1 2 2";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "3 3 4 4";
                this.LayerService.AddLayerRow(inputArgsStr);
                inputArgsStr = "5 5 6 6";

                this.LayerService.AddLayerRow(inputArgsStr);
            }
            catch (ArgumentOutOfRangeException e)
            {
                var errMsg = ErrMsg.LayerIsFull;
                Assert.Equal(errMsg, e.Message);
            }
        }

        private ILayerService InitializeLayerService(int row, int col)
        {
            var layerService = new LayerService();

            this.LayerRows = row;
            this.LayerCols = col;
            var inputArgsStr = $"{this.LayerRows} {this.LayerCols}";

            layerService.GetLayerDimensions(inputArgsStr);

            return layerService;
        }
    }
}
