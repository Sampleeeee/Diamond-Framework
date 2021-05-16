using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Diamond.Client.Shops;
using Diamond.Shared;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.UserInterface.Events;

namespace Diamond.UserInterface.Pages
{
	public partial class Shop
	{
		public static Shop Instance { get; private set; }

		public BaseShop CurrentShop { get; set; } = new LimitedGasolineShop();
		public int ItemCount { get; set; } = 1;

		public readonly Dictionary<BaseItem, int> PlayerInventory = new()
		{
			{ new BeerItem(), 1 }, { new ChampagneItem(), 10 }, { new CombatPistolItem(), 5 }
		};
		
		public BaseItem SelectedItem { get; set; }

		protected override void OnInitialized()
		{
			Instance = this;
			base.OnInitialized();
		}

		private void ItemDragged( BaseItem item )
		{
			this.SelectedItem = item;
		}

		private void OnBuy()
		{
			if ( this.CurrentShop.Items.Contains( this.SelectedItem as IPurchasableItem ) )
				Communicator.TriggerNuiCallback( "ShopBuyItem", new { item = this.SelectedItem.UniqueId, amount = this.ItemCount } );
		}
	}
}
