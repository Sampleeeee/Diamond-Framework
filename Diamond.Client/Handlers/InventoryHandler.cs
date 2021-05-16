using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface.Inventory;
using Newtonsoft.Json;

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

		private static readonly string[] _nuiCallbackTypes = new string[]
		{
			"PrimaryInventoryItemUsed", "SecondaryInventoryItemUsed",
			"PrimaryInventoryItemGave", "SecondaryInventoryItemGave",
			"PrimaryInventoryItemDropped", "SecondaryInventoryItemDropped"
		};
		
		public InventoryHandler()
		{
			foreach ( string name in _nuiCallbackTypes )
				API.RegisterNuiCallbackType( name );
		}

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
		private void OnUsedItem( string sItem )
		{
			var item = Shared.Utility.GetItem( sItem );

			if ( item is IUseableItem useableItem )
				useableItem.OnUse( MainClient.Character );
		}

		[EventHandler( "__cfx_nui:PrimaryInventoryItemUsed" )]
		private void OnPrimaryInventoryItemUsed( IDictionary<string, object> data, CallbackDelegate callback )
		{
			var json = ( string ) data["data"];
			callback.Invoke( true );
		}
	}
}
