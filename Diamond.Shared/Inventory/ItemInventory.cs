using System;
using System.Collections.Generic;
using System.Linq;
using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Inventory
{
	public class ItemInventory : BaseInventory<BaseItem>
	{
		public int MaxWeight { get; private set; } = 100;

		public ItemInventory( Character owner, int maxWeight = 100 ) : base( owner )
		{
			this.MaxWeight = maxWeight;
		}

		public ItemInventory( Character owner, Dictionary<string, int> items, int maxWeight = 100 ) : base( owner )
		{
			this.MaxWeight = maxWeight;

			// TODO do the items
		}

		public int GetWeight() =>
			this._items.Select( t => new { t, item = Activator.CreateInstance( t ) as BaseItem } )
				.Where( t1 => t1.item != null )
				.Select( t1 => t1.item.Weight * this._values[this._items.IndexOf( t1.t )] ).Sum();

		public bool CanAddWeight( int weight ) => this.GetWeight() + weight <= this.MaxWeight;

		public bool CanAddItem( BaseItem item, int amount = 1 )
		{
			var stackSize = true;
			if ( item.StackSize != -1 )
				stackSize = this.GetCount( item ) + amount <= item.StackSize;

			return stackSize && this.CanAddWeight( item.Weight * amount );
		}
	}
}
