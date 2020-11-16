using StoreApplication.Library;
using System;
using Xunit;

namespace StoreApplication.Tests
{
    public class LocationTests
    {
        public Library.Models.Location location = new Library.Models.Location();

        [Fact]
        public void AdjustingInventoryChangesInventory()
        {
        }
    }
}
