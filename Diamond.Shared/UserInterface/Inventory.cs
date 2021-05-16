using System.Collections.Generic;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.UserInterface.Inventory
{
	public class ShowInventoryEvent
	{
		public string InfoText { get; set; } = string.Empty;
		public Dictionary<BaseItem, int> Primary { get; set; }
		public Dictionary<BaseItem, int> Secondary { get; set; } = null;
		public Dictionary<int, string> Players { get; set; } = new Dictionary<int, string>();

		public ShowInventoryEvent() { }
	}

	public class ItemUsedEventArgs
	{
		public BaseItem Item { get; set; }
		public int Count { get; set; }
	}

	public class ItemDroppedEventArgs
	{
		public BaseItem Item { get; set; }
		public int Count { get; set; }
	}

	public class ItemGaveEventArgs
	{
		public BaseItem Item { get; set; }
		public int Count { get; set; }
		public int PlayerServerId { get; set; }
	}
}
