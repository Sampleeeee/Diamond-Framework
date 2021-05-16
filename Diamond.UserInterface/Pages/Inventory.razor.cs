using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Diamond.Shared;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface.Inventory;
using Diamond.UserInterface.Events;
using Diamond.UserInterface.Shared;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Diamond.UserInterface.Pages
{
	public partial class Inventory
	{
		[Inject] private static NavigationManager _navigation { get; set; }
		public static Inventory Instance { get; set; }

		protected override void OnInitialized()
		{
			Instance = this;
			base.OnInitialized();
		}

		public Dictionary<BaseItem, int> PrimaryInventory { get; set; } = new() { { new BeerItem(), 1 } };
		
		public Dictionary<BaseItem, int> SecondaryInventory { get; set; } = null;

		public Dictionary<int, string> NearbyPlayers { get; set; } = new() { { 1, "Sample"}, {2, "Robert" }};

		public bool ShowDialog { get; set; } = false;
		public bool ShowInfo { get; set; } = false;
		public int ItemCount { get; set; } = 1;
		public BaseItem SelectedItem { get; set; } = null;
		public bool SecondaryDragged { get; set; } = false;

		[NuiEventHandler("ShowInventory")]
		private static void OnShowInventoryEvent( string data )
		{
			_navigation.NavigateTo( "/Inventory" );
			MainLayout.ShouldShowLayout = true;

			var eventArgs = JsonConvert.DeserializeObject<ShowInventoryEvent>( data );

			Instance.ShowInfo = eventArgs?.Type switch
			{
				"normal"   => false,
				"trunk"    => true,
				"property" => false,
				"storage"  => false,
				"player"   => true,
				"shop"     => true,
				_          => Instance.ShowInfo
			};
			
			if ( eventArgs?.Primary != null )
			{
				var primary = new Dictionary<BaseItem, int>();
				foreach ( (string name, int value) in eventArgs.Primary )
				{
					var item = Utility.GetItem( name );
					primary.Add( item, value );
				}

				Instance.PrimaryInventory = primary;
			}

			if ( eventArgs?.Secondary != null )
			{
				var secondary = new Dictionary<BaseItem, int>();
				foreach ( ( string name, int value ) in eventArgs.Secondary )
				{
					var item = Utility.GetItem( name );
					secondary.Add( item, value );
				}

				Instance.PrimaryInventory = secondary;
			}

			if ( eventArgs?.Players != null )
			{
				Instance.NearbyPlayers = eventArgs.Players;
			}
		}

		[NuiEventHandler( "HideInventory" )]
		private static void OnHideInventoryEvent( string data )
		{
			MainLayout.ShouldShowLayout = false;
			Instance.PrimaryInventory = null;
			Instance.SecondaryInventory = null;
		}

		[NuiEventHandler( "UpdateInventoryNearbyPlayers" )]
		private static void OnUpdateNearbyPlayers( string data )
		{
			Instance.NearbyPlayers = JsonConvert.DeserializeObject<Dictionary<int, string>>( data );
		}

		private void OnUse()
		{
			if ( this.SelectedItem == null ) return;

			var data = new { item = this.SelectedItem.UniqueId, count = this.ItemCount };
			
			Communicator.TriggerNuiCallback(
				this.SecondaryDragged ? "SecondaryInventoryItemUsed" : "PrimaryInventoryItemUsed", data );
		}

		private void OnGive()
		{
			this.ShowDialog = true;

			// TODO request updated listed of nearby players?
		}

		private void OnDrop()
		{
			if ( this.SelectedItem == null ) return;
			
			var data = new { item = this.SelectedItem.UniqueId, count = this.ItemCount };

			Communicator.TriggerNuiCallback(
				this.SecondaryDragged ? "SecondaryInventoryItemDrop" : "PrimaryInventoryItemDrop", data );
		}

		private void OnNearbyPlayerClicked( int player )
		{
			if ( this.SelectedItem == null ) return;
			
			this.ShowDialog = false;
			Communicator.TriggerNuiCallback( "GiveItem",
				new { player, item = this.SelectedItem?.UniqueId, count = this.ItemCount } );
		}

		private void ItemDragged( BaseItem item, bool primary = true )
		{
			this.SelectedItem = item;
			this.SecondaryDragged = !primary;
		}
	}
}
