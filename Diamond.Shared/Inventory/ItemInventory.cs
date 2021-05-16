using System;
using System.Collections.Generic;
using System.Linq;
using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Inventory
{
    public class ItemInventory : BaseInventory<BaseItem>
    {
        public int MaxWeight { get; private set; } = 100;

        public ItemInventory(Character owner, int maxWeight = 100) : base(owner)
        {
            MaxWeight = maxWeight;
        }

        public ItemInventory(Character owner, Dictionary<string, int> items, int maxWeight = 100) : base(owner)
        {
            MaxWeight = maxWeight;
            
            // TODO do the items
        }
        
        public int GetWeight() =>
            _items.Select(t => new {t, item = Activator.CreateInstance(t) as BaseItem})
                .Where(t1 => t1.item != null)
                .Select(t1 => t1.item.Weight * _values[_items.IndexOf(t1.t)]).Sum();
        
        public bool CanAddWeight(int weight) =>
            GetWeight() + weight <= MaxWeight;

        public bool CanAddItem(BaseItem item, int amount = 1)
        {
            var stackSize = true;
            if (item.StackSize != -1)
                stackSize = GetCount(item) + amount <= item.StackSize;

            return stackSize && CanAddWeight(item.Weight * amount);
        }
    }
}