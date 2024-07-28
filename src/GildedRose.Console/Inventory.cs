using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console;

public class Inventory
{
    private readonly List<Item> _items;
    private readonly Dictionary<string, string> _itemCategories;

    public Inventory(IEnumerable<Item> items)
    {
        _items = new List<Item>(items);
        _itemCategories = CategorizeItems();
    }

    public IReadOnlyList<Item> GetItems()
    {
        return _items.AsReadOnly();
    }

    public IReadOnlyDictionary<string, string> GetCategories()
    {
        return new Dictionary<string, string>(_itemCategories);
    }

    public void AddItem(Item newItem)
    {
        _items.Add(newItem);

        // New items are categorized based on their name, defaulting to "Standard" if no specific category matches
        _itemCategories[newItem.Name] = CategorizeItem(newItem);
        
        System.Console.WriteLine($"Added new item: {newItem.Name}");
    }

    public void PrintInventory()
    {
        System.Console.WriteLine("name, sellIn, quality");
        foreach (var item in _items)
        {
            System.Console.WriteLine($"{item.Name}, {item.SellIn}, {item.Quality}");
        }
        System.Console.WriteLine("");
    }

    private Dictionary<string, string> CategorizeItems()
    {
        return _items.ToDictionary(item => item.Name, item => CategorizeItem(item));
    }

    private string CategorizeItem(Item item)
    {
        if (item.Name.Contains("Aged Brie"))
            return "Appreciating";
        if (item.Name.Contains("Backstage passes"))
            return "EventBased";
        if (item.Name.Contains("Sulfuras"))
            return "Legendary";
        if (item.Name.Contains("Conjured"))
            return "Conjured";
        return "Standard";
    }


}
