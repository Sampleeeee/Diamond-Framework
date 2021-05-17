using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using Diamond.Shared;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.Jobs;
using Diamond.Shared.UserInterface;
using Newtonsoft.Json;

namespace Diamond.Server
{
	public class MainServer : BaseScript
	{
		public static MainServer Instance;

		public Dictionary<Player, Character> Characters = new Dictionary<Player, Character>();

		public MainServer()
		{
			Instance = this;
			Debug.WriteLine( "Creating Instance" );
		}

		[EventHandler( "PlayerReady" )]
		public void PlayerReady( [FromSource] Player player )
		{
			var character = new Character
			{
				Player = player,
				FirstName = "Bill",
				LastName = "Microsoft",
				Job = new TrooperJob(),
				JobGrade = new TrooperJob.Sergeant(),
				Money = 10000,
				Bank = 5000,
				DirtyMoney = 500,
				Hunger = 50,
				Thirst = 50,
				Age = 82,
			};

			character.ItemInventory = new ItemInventory( character );

			this.Characters[player] = character;

			string json = JsonConvert.SerializeObject( character );
			player.TriggerEvent( "SetupCharacter", json );

			character.ItemInventory.AddItem( new HotDogItem(), 10 );
			character.ItemInventory.AddItem( new CombatPistolItem() );
			character.ItemInventory.AddItem( new PistolAmmoItem() );
			character.ItemInventory.AddItem( new WaterBottleItem() );
		}

		[EventHandler( "GiveItem" )]
		private void GiveItem( [FromSource] Player player, int iOther, string sItem, int amount )
		{
			Debug.WriteLine( "Giving Item" );
			var other = this.Players[iOther];

			if ( player.Character.Position.DistanceToSquared( other.Character.Position ) > 4 * 4 ) return;

			var character = this.Characters[player];
			var otherCharacter = this.Characters[other];
			var item = Shared.Utility.GetItem( sItem );

			if ( !character.ItemInventory.HasItem( item, amount ) )
			{
				Debug.WriteLine( "no have" );
				return;
			};
			if ( !otherCharacter.ItemInventory.CanAddItem( item, amount ) )
			{
				Debug.WriteLine( "can no give" );
				return;
			};

			character.ItemInventory.TakeItem( item, amount );
			otherCharacter.ItemInventory.AddItem( item, amount );

			Debug.WriteLine( "Gave Item" );
		}

		[EventHandler( "BoughtShopItem" )]
		private void OnBoughtShopItem( [FromSource] Player player, string data )
		{
			Debug.WriteLine( "Bought item event" );
			var @event = JsonConvert.DeserializeObject<ShopBuyItemEventArgs>( data );
			var character = this.Characters[player];

			if ( @event == null ) return;
			if ( character == null ) return;

			Debug.WriteLine( "Bought item event 2" );

			if ( @event.Item is IPurchasableItem i )
			{
				// TODO notify player
				if ( !character.CanAfford( i.Price ) ) return;
				if ( !character.ItemInventory.CanAddItem( @event.Item ) ) return;

				character.Money -= i.Price;
				character.ItemInventory.AddItem( @event.Item );

				i.OnBuy( character );
				Network.TriggerClientEvent( player, "BoughtItemConfirmed", @event );
			}
		}

		[EventHandler( "BuyItem" )]
		private void OnBuyItem( [FromSource] Player player, string sShop, string sItem )
		{
			var shop = Shared.Utility.GetShop( sShop );
			var item = Shared.Utility.GetItem( sItem );

			if ( !( item is IPurchasableItem purchasableItem ) ) return;
			if ( !shop.Locations.Any( location => player.Character.Position.DistanceToSquared( location ) < 4 * 4 ) &&
				shop.Items.Contains( purchasableItem ) ) return;

			var character = this.Characters[player];
			if ( !character.CanAfford( purchasableItem.Price ) ) return;
			if ( !purchasableItem.CanBuy( character ) ) return;

			character.Money -= purchasableItem.Price;
			character.ItemInventory.AddItem( item, purchasableItem.BulkAmount );

			purchasableItem.OnBuy( character );
		}

		[EventHandler( "UseItem" )]
		private void OnUseItem( [FromSource] Player player, string data )
		{
			var @event = JsonConvert.DeserializeObject<ItemUsedEventArgs>( data );
			if ( @event == null ) return;
			
			var character = this.Characters[player];

			if ( !character.ItemInventory.HasItem( @event.Item ) ) return;
			if ( !( @event.Item is IUseableItem useableItem ) ) return;
			if ( !useableItem.CanUse( character ) ) return;

			character.ItemInventory.TakeItem( @event.Item );
			useableItem.OnUse( character );
			
			Network.TriggerClientEvent( character.Player, "UsedItem", @event.Item );
		}
	}
}
