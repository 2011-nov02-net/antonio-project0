using StoreApplication.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApplication.Tests
{
    public class StockTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(0)]
        public void StockQuantityDecreasesByN(int n)
        {
            var stock = new Stock();
            stock.Quantity = 10;
            stock.AdjustStock(n);
            bool result = 10 - n == stock.Quantity;

            Assert.True(result, $"Stock should have decreased by {n}");
        }

        [Theory]
        [InlineData(13)]
        [InlineData(11)]
        [InlineData(500)]
        public void StockQuantityShouldNeverFallBelow0(int n)
        {
            var stock = new Stock();
            stock.Quantity = 10;
            stock.AdjustStock(n);
            bool result = 0 == stock.Quantity;

            Assert.True(result, $"Stock should never fall under 0. Stock: {stock.Quantity}");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void CheckIfStockHasEnoughWhenThereIs(int n)
        {
            var stock = new Stock();
            stock.Quantity = 10;
            
            bool result = stock.CheckStock(n);

            Assert.True(result, $"There should be enough in stock. Stock: {stock.Quantity}");

        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void CheckIfStockHasEnoughWhenThereIsNotEnough(int n)
        {
            var stock = new Stock();
            stock.Quantity = 0;

            bool result = stock.CheckStock(n);

            Assert.False(result, $"There should not be enough in stock. Stock: {stock.Quantity}");

        }
    }
}
