using System;

namespace GildedRose.Console;

public class QualityController
{
    private readonly Inventory _inventory;

    public QualityController(Inventory inventory)
    {
        _inventory = inventory;
    }

    /// Updates item quality based on categories, allowing for easy extension 
    /// when adding new items. 
    public void UpdateQuality()
    {
        var items = _inventory.GetItems();
        var itemCategories = _inventory.GetCategories();

        foreach (var item in items)
        {
            if (itemCategories[item.Name] != "Legendary")
            {
                item.SellIn--;
            }

            switch (itemCategories[item.Name])
            {
                case "Standard":
                    UpdateStandardItem(item);
                    break;
                case "Appreciating":
                    UpdateAppreciatingItem(item);
                    break;
                case "EventBased":
                    UpdateEventBasedItem(item);
                    break;
                case "Conjured":
                    UpdateConjuredItem(item);
                    break;
            }

        }
    }

    private void UpdateStandardItem(Item item)
    {
        DecrementQuality(item, 1);
        if (item.SellIn < 0)
        {
            DecrementQuality(item, 1);
        }
    }

    private void UpdateAppreciatingItem(Item item)
    {
        IncrementQuality(item, 1);
        if (item.SellIn < 0)
        {
            IncrementQuality(item, 1);
        }
    }

    private void UpdateEventBasedItem(Item item)
    {
        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
        else if (item.SellIn < 6)
        {
            IncrementQuality(item, 3);
        }
        else if (item.SellIn < 11)
        {
            IncrementQuality(item, 2);
        }
        else
        {
            IncrementQuality(item, 1);
        }
    }

    private void UpdateConjuredItem(Item item)
    {
        DecrementQuality(item, 2);
        if (item.SellIn < 0)
        {
            DecrementQuality(item, 2);
        }
    }

    private void IncrementQuality(Item item, int amount)
    {
        item.Quality = Math.Min(50, item.Quality + amount);
    }

    private void DecrementQuality(Item item, int amount)
    {
        item.Quality = Math.Max(0, item.Quality - amount);
    }
}
