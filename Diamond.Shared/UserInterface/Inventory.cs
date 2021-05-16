using System.Collections.Generic;
using Diamond.Shared.Inventory;

namespace Diamond.Shared.UserInterface.Inventory
{
	public class ShowInventoryEvent
	{
		public string Type { get; set; } = "normal";
		public string InfoText { get; set; }
		public Dictionary<string, int> Primary { get; set; }
		public Dictionary<string, int> Secondary { get; set; } = null;
		public Dictionary<int, string> Players { get; set; } = new Dictionary<int, string>();

		public ShowInventoryEvent() { }
	}
}
