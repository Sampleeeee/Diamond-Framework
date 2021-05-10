using System;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;

namespace Diamond.Client.Handlers
{
    public class InventoryHandler : BaseScript
    {
        [EventHandler("InventoryUpdated")]
        private void OnInventoryUpdated(string sItem, int amount)
        {
            var item = Shared.Utility.GetItem(sItem);
            MainClient.Character.Inventory.AddItem(item, amount);
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