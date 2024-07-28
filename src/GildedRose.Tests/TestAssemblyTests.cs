using System.Collections.Generic;
using System.Linq;
using Xunit;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class QualityControllerTests
    {
        private Inventory CreateTestInventory()
        {
            return new Inventory(new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 }
            });
        }

        private Item GetItemByName(Inventory inventory, string name)
        {
            return inventory.GetItems().First(i => i.Name.Contains(name));
        }

        [Fact]
        public void StandardItem_QualityDecreasesByOne_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "+5 Dexterity Vest");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(19, item.Quality);
        }

        [Fact]
        public void StandardItem_SellInDecreasesByOne_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "+5 Dexterity Vest");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void StandardItem_QualityDecreasesByTwo_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "+5 Dexterity Vest");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(18, item.Quality);
        }

        [Fact]
        public void StandardItem_SellInDecreasesByOne_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "+5 Dexterity Vest");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void AgedBrie_QualityIncreasesByOne_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Aged Brie");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(21, item.Quality);
        }

        [Fact]
        public void AgedBrie_SellInDecreasesByOne_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Aged Brie");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void AgedBrie_QualityIncreasesByTwo_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Aged Brie");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(22, item.Quality);
        }

        [Fact]
        public void AgedBrie_SellInDecreasesByOne_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Aged Brie");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void Sulfuras_QualityDoesNotChange()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Sulfuras");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(80, item.Quality);
        }

        [Fact]
        public void Sulfuras_SellInDoesNotChange()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Sulfuras");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(10, item.SellIn);
        }

        [Fact]
        public void BackstagePasses_QualityIncreasesByOne_WhenMoreThan10Days()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 15;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(21, item.Quality);
        }

        [Fact]
        public void BackstagePasses_SellInDecreasesByOne_WhenMoreThan10Days()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 15;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(14, item.SellIn);
        }

        [Fact]
        public void BackstagePasses_QualityIncreasesByTwo_When10DaysOrLess()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 10;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(22, item.Quality);
        }

        [Fact]
        public void BackstagePasses_SellInDecreasesByOne_When10DaysOrLess()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 10;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void BackstagePasses_QualityIncreasesByThree_When5DaysOrLess()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 5;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(23, item.Quality);
        }

        [Fact]
        public void BackstagePasses_SellInDecreasesByOne_When5DaysOrLess()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 5;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(4, item.SellIn);
        }

        [Fact]
        public void BackstagePasses_QualityDropsToZero_AfterConcert()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void BackstagePasses_SellInDecreasesByOne_AfterConcert()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Backstage passes");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void ConjuredItem_QualityDecreasesByTwo_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Conjured");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(18, item.Quality);
        }

        [Fact]
        public void ConjuredItem_SellInDecreasesByOne_BeforeSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Conjured");

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void ConjuredItem_QualityDecreasesByFour_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Conjured");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(16, item.Quality);
        }

        [Fact]
        public void ConjuredItem_SellInDecreasesByOne_AfterSellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Conjured");
            item.SellIn = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void QualityNeverExceedsFifty()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "Aged Brie");
            item.Quality = 50;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void QualityNeverGoesNegative()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            var item = GetItemByName(inventory, "+5 Dexterity Vest");
            item.Quality = 0;

            // Act
            controller.UpdateQuality();

            // Assert
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void NewItemsAreIncludedInQualityUpdate_Quality()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            inventory.AddItem(new Item { Name = "New Magic Wand", SellIn = 10, Quality = 30 });

            // Act
            controller.UpdateQuality();

            // Assert
            var newItem = GetItemByName(inventory, "New Magic Wand");
            Assert.Equal(29, newItem.Quality);
        }

        [Fact]
        public void NewItemsAreIncludedInQualityUpdate_SellIn()
        {
            // Arrange
            var inventory = CreateTestInventory();
            var controller = new QualityController(inventory);
            inventory.AddItem(new Item { Name = "New Magic Wand", SellIn = 10, Quality = 30 });

            // Act
            controller.UpdateQuality();

            // Assert
            var newItem = GetItemByName(inventory, "New Magic Wand");
            Assert.Equal(9, newItem.SellIn);
        }
    }
}