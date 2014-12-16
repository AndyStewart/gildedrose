using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private static readonly Dictionary<string, IProduct> Product = new Dictionary<string, IProduct>
            {
                {"Aged Brie", new Product(new QualityAdjuster(+1))},
                {"Backstage passes to a TAFKAL80ETC concert", new Product(new BackstageAdjuster())},
                {"Sulfuras, Hand of Ragnaros", new Sulfuras()},
                {"Conjured Mana Cake", new Product(new QualityAdjuster(-2))},
                {"NORMAL ITEM", new Product(new QualityAdjuster(-1))}
            };

        public static void UpdateQuality(List<Item> items)
        {
            foreach (var item in items)
            {
                Product[item.Name].Update(item);
            }
        }
    }

    internal interface IProduct
    {
        void Update(Item item);
    }

    internal class QualityAdjuster : IAdjuster
    {
        private readonly int _adjustment;

        public QualityAdjuster(int adjustment)
        {
            _adjustment = adjustment;
        }

        public int Adjust(Item item)
        {
            return (item.SellIn < 0 ? _adjustment * 2 : _adjustment);
        }
    }

    internal class Sulfuras : IProduct
    {
        public void Update(Item item)
        {
        }
    }

    internal class Product : IProduct
    {
        private readonly IAdjuster _qualityAdjuster;

        public Product(IAdjuster qualityAdjuster)
        {
            _qualityAdjuster = qualityAdjuster;
        }

        public void Update(Item item)
        {
            item.SellIn = item.SellIn - 1;
            item.Quality = item.Quality + _qualityAdjuster.Adjust(item);
            item.Quality = Math.Max(item.Quality, 0);
            item.Quality = Math.Min(item.Quality, 50);
        }
    }

    public class BackstageAdjuster : IAdjuster
    {
        public int Adjust(Item item)
        {
            if (item.SellIn < 0)
                return -item.Quality;
            if (item.SellIn < 5)
                return +3;
            if (item.SellIn < 10)
                return +2;
            return +1;
        }
    }

    public interface IAdjuster
    {
        int Adjust(Item item);
    }
}