using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Client.Wrappers;
using Diamond.Shared;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface;
using Newtonsoft.Json;

namespace Diamond.Client.Handlers
{
	public class InventoryHandler : BaseScript
	{
		public InventoryHandler()
		{
			foreach ( string name in _nuiCallbackTypes ) API.RegisterNuiCallbackType( name );
			
			API.RegisterKeyMapping( "inventory", "Open Inventory", "keyboard", "f3" );
		}

		private bool _inventoryOpen;

		[Command( "inventory" )]
		private void OnInventoryCommand()
		{
			if ( this._inventoryOpen )
				this.CloseInventory();
			else
				this.OpenInventory();
		}

		private void OpenInventory()
		{
			var players = this.Players.ToDictionary( player => player.ServerId, player => player.Name );

			Utility.SendNuiMessage( "ShowInventory",
				new ShowInventoryEventArgs
				{
					Players = players, Primary = MainClient.Character.ItemInventory.AsDictionary()
				} );

			Nui.EnableFocus( true, true );
			this._inventoryOpen = true;
		}

		private void CloseInventory()
		{
			Nui.DisableFocus();
			Utility.SendNuiMessage( "HideInventory" );
			this._inventoryOpen = false;
		}
		
		// ReSharper disable twice AssignNullToNotNullAttribute
		private static readonly Dictionary<string, string> _eventNames = new Dictionary<string, string>()
		{
			{ typeof(ItemInventory).FullName, "ItemInventoryUpdated" },
			{ typeof(VehicleInventory).FullName, "VehicleInventoryUpdated" }
		};

		private static readonly string[] _nuiCallbackTypes = new string[]
		{
			"PrimaryInventoryItemUsed", "SecondaryInventoryItemUsed",
			"PrimaryInventoryItemGave", "SecondaryInventoryItemGave",
			"PrimaryInventoryItemDropped", "SecondaryInventoryItemDropped"
		};

		[EventHandler( "InventoryUpdated" )]
		private void OnInventoryUpdated( string inventoryType, string item, int amount )
		{
			if ( _eventNames.ContainsKey( inventoryType ) )
				TriggerEvent( _eventNames[inventoryType], item, amount );
		}

		[EventHandler( "ItemInventoryUpdated" )]
		private void OnItemInventoryUpdated( string sItem, int amount )
		{
			var item = Shared.Utility.GetItem( sItem );
			MainClient.Character.ItemInventory.AddItem( item, amount );
		}

		[EventHandler( "UsedItem" )]
		private void OnUsedItem( string data )
		{
			BaseItem item = JsonConvert.DeserializeObject<BaseItem>( data );

			if ( item is IUseableItem useableItem )
				useableItem.OnUse( MainClient.Character );
		}

		[EventHandler( "__cfx_nui:PrimaryInventoryItemUsed" )]
		private void OnPrimaryInventoryItemUsed( IDictionary<string, object> data, CallbackDelegate callback )
		{
			var json = ( string ) data["data"];
			var @event = JsonConvert.DeserializeObject<ItemUsedEventArgs>(json);
			if ( @event == null ) return;

			if ( @event.Item is IUseableItem )
			{
				Network.TriggerServerEvent( "UseItem", @event );
				callback.Invoke( true );
			}
			else
			{
				callback.Invoke( false );
			}
		}
	}
}
