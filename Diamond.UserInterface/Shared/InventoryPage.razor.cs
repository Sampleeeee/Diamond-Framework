using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface.Inventory;
using Diamond.UserInterface.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Diamond.UserInterface.Shared
{
	public partial class InventoryPage
	{
		[Parameter] public ShowInventoryEvent? EventArguments { get; set; }

		private int _itemCount = 1;
		private bool _showPlayerList = false;
		private bool _secondaryDragged = false;
		private BaseItem? _selectedItem = null;

		protected override void OnInitialized()
		{
			if ( this.EventArguments == null ) return;
			base.OnInitialized();
		}

		private void OnItemClicked( BaseItem item, bool primary = true )
		{
			this._selectedItem = item;
			this._secondaryDragged = !primary;
		}

		private async Task OnUse()
		{
			if ( this._selectedItem == null ) return;

			Console.WriteLine( "on use" );

			var data = new ItemUsedEventArgs { Item = this._selectedItem, Count = this._itemCount };
			
			await this.TriggerNuiCallbackAsync(
				this._secondaryDragged ? "SecondaryInventoryItemUsed" : "PrimaryInventoryItemUsed", data );
		}

		private async Task OnDrop()
		{
			if ( this._selectedItem == null ) return;

			var data = new ItemDroppedEventArgs { Item = this._selectedItem, Count = this._itemCount };

			await this.TriggerNuiCallbackAsync(
				this._secondaryDragged ? "SecondaryInventoryItemDropped" : "PrimaryInventoryItemDropped", data );
		}

		private void OnGive()
		{
			if ( this._selectedItem == null ) return;
			
			// TODO request list of nearby players, save item?
			this._showPlayerList = true;
		}

		private async Task OnPlayerSelected( int player )
		{
			if ( this._selectedItem == null ) return;
			this._showPlayerList = false;

			var data = new ItemGaveEventArgs
			{
				PlayerServerId = player, Item = this._selectedItem, Count = this._itemCount
			};
			
			await this.TriggerNuiCallbackAsync(
				this._secondaryDragged ? "SecondaryInventoryItemGave" : "PrimaryInventoryItemGave", data );
		}
		
		private string GetActiveClass( BaseItem item, bool secondary = false ) =>
			this._selectedItem?.GetType() == item.GetType() && this._secondaryDragged == secondary ? "active" : "";

		private string GetStyle( BaseItem item ) =>
			$"background-image: url({item.ImageUrl})";
	}
}
