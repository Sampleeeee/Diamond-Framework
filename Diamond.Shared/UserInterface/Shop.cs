using System.Collections.Generic;
using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.UserInterface
{
	public class ShowShopEventArgs
	{
		public BaseShop Shop { get; set; }
		public Dictionary<BaseItem, int> PlayerInventory { get; set; }
	}

	public class ShopBuyItemEventArgs
	{
		public BaseItem Item { get; set; }
		public int Count { get; set; }
	}
}
