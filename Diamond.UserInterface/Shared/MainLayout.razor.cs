using System;
using System.Collections.Generic;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface.Inventory;
using Diamond.UserInterface.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Diamond.UserInterface.Shared
{
	public partial class MainLayout
	{
		[Inject] private IJSRuntime _javascript { get; set; }
		public static IJSRuntime Javascript { get; set; }
		public static MainLayout Instance { get; set; }

		protected override void OnInitialized()
		{
			Instance = this;
			Javascript = this._javascript;
			base.OnInitialized();
		}
	}
	
	#region Inventory
	public partial class MainLayout
	{
		private ShowInventoryEvent? _showInventoryEvent;
		private ShowInventoryEvent? InventoryEventArgs
		{
			get => this._showInventoryEvent;
			set => this.UpdateInventoryEventArgs( value );
		}
		
		[NuiEventHandler( "ShowInventory" )]
		public static void OnShowInventoryEvent( string data )
		{
			Console.WriteLine( "ShowInventoryEvent " + data );
			Instance.InventoryEventArgs = JsonConvert.DeserializeObject<ShowInventoryEvent>( data );
		}

		[NuiEventHandler("HideInventory")]
		public static void HideInventory( string _ )
		{
			Instance.InventoryEventArgs = null;
		}

		private void UpdateInventoryEventArgs( ShowInventoryEvent? @event )
		{
			Console.WriteLine("Updating inventory event args");
			this._showInventoryEvent = @event;
			this.StateHasChanged();
		}
	}
	#endregion
}
