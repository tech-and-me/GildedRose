using System;
using System.Collections.Generic;

namespace GildedRose.Console;

public class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("OMGHAI!");

        var items = new List<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        var inventory = new Inventory(items);
        var controller = new QualityController(inventory);

        int days = 31;
        if (args.Length > 0)
        {
            days = int.Parse(args[0]) + 1;
        }

        for (var i = 0; i < days; i++)
        {
            System.Console.WriteLine($"-------- day {i} --------");
            inventory.PrintInventory();
            controller.UpdateQuality();

            // Add new items to test both categorization and item addition features
            if (i == 3)
            {
                inventory.AddItem(new Item { Name = "Aged Brie Deluxe", SellIn = 5, Quality = 10 });
            }
            else if (i == 5)
            {
                inventory.AddItem(new Item { Name = "New Magic Wand", SellIn = 10, Quality = 30 });
            }

        }

        System.Console.ReadKey();
    }
}
