using System;
using System.Collections.Generic;
using CitizenFX.Core;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;

namespace Diamond.Client.Handlers
{ 
	public class InventoryHandler : BaseScript 
	{
	    // ReSharper disable twice AssignNullToNotNullAttribute
	    private static readonly Dictionary<string, string> _eventNames = new Dictionary<string, string>()
	    {
		    { typeof(ItemInventory).FullName, "ItemInventoryUpdated" },
		    { typeof(VehicleInventory).FullName, "VehicleInventoryUpdated" }
	    };
	    
		[EventHandler("InventoryUpdated")]
	    private void OnInventoryUpdated(string inventoryType, string item, int amount)
	    {
	        if (_eventNames.ContainsKey(inventoryType))
	            TriggerEvent(_eventNames[inventoryType], item, amount);
	    }

	    [EventHandler("ItemInventoryUpdated")]
	    private void OnItemInventoryUpdated(string sItem, int amount)
	    {
	        var item = Shared.Utility.GetItem(sItem);
	        MainClient.Character.ItemInventory.AddItem(item, amount);
	    }

	    [EventHandler("UsedItem")]
	    private void OnUsedItem(string sItem)
	    {
	        var item = Shared.Utility.GetItem(sItem);
	        
	        if (item is IUseableItem useableItem)
	            useableItem.OnUse(MainClient.Character);
	    }
	}
}
