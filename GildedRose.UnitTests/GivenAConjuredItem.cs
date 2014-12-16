using System;
using System.Collections.Generic;
using Xunit;

namespace GildedRose.UnitTests
{
	public class GivenAConjuredItem 
	{
		static String CONJURED_ITEM = "Conjured Mana Cake";

		private Item updateFrom(int initialSellIn, int initialQuality) 
		{
			Item item = new Item(CONJURED_ITEM, initialSellIn, initialQuality);
			List<Item> items = new List<Item>();
			items.Add(item);
			GildedRose.UpdateQuality(items);
			return item;
		}

		[Fact]
		public void BeforeSellDate() 
		{
			Item item = updateFrom(5, 10);
			Assert.Equal(8, item.quality);
			Assert.Equal(4, item.sellIn);
		}

		[Fact]
		public void BeforeSellDateAtZeroQuality() 
		{
			Item item = updateFrom(5, 0);
			Assert.Equal(0, item.quality);
			Assert.Equal(4, item.sellIn);
		}

		[Fact]
		public void OnSellDate() 
		{
			Item item = updateFrom(0, 10);
			Assert.Equal(6, item.quality);
			Assert.Equal(-1, item.sellIn);
		}

		[Fact]
		public void OnSellDateAtZeroQuality() 
		{
			Item item = updateFrom(0, 0);
			Assert.Equal(0, item.quality);
			Assert.Equal(-1, item.sellIn);
		}

		[Fact]
		public void AfterSellDate() 
		{
			Item item = updateFrom(-10, 10);
			Assert.Equal(6, item.quality);
			Assert.Equal(-11, item.sellIn);
		}

		[Fact]
		public void AfterSellDateAtZeroQuality() 
		{
			Item item = updateFrom(-10, 0);
			Assert.Equal(0, item.quality);
			Assert.Equal(-11, item.sellIn);
		}
	}
}
