using System;
using System.Collections.Generic;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface;
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

	#region Shops
	public partial class MainLayout
	{
		private ShowShopEventArgs? _showShopEventArgs;

		private ShowShopEventArgs? ShowShopEventArgs
		{
			get => this._showShopEventArgs;
			set => this.UpdateShopEventArgs( value );
		}

		[NuiEventHandler( "ShowShop" )]
		public static void OnShowShopEvent( string data )
		{
			Console.WriteLine( data );
			Instance.ShowShopEventArgs = JsonConvert.DeserializeObject<ShowShopEventArgs>( data );
		}

		[NuiEventHandler( "HideShop" )]
		public static void OnHideShopEvent( string _ = "" )
		{
			Instance.ShowShopEventArgs = null;
		}

		private void UpdateShopEventArgs( ShowShopEventArgs? @event )
		{
			Console.WriteLine( "Updating inventory event args" );
			this._showShopEventArgs = @event;
			this.StateHasChanged();
		}
	}
	#endregion
	
	#region Inventory
	public partial class MainLayout
	{
		private ShowInventoryEventArgs? _showInventoryEvent;
		private ShowInventoryEventArgs? InventoryEventArgs
		{
			get => this._showInventoryEvent;
			set => this.UpdateInventoryEventArgs( value );
		}
		
		[NuiEventHandler( "ShowInventory" )]
		public static void OnShowInventoryEvent( string data )
		{
			Console.WriteLine( "ShowInventoryEvent " + data );
			Instance.InventoryEventArgs = JsonConvert.DeserializeObject<ShowInventoryEventArgs>( data );
		}

		[NuiEventHandler("HideInventory")]
		public static void HideInventory( string _ = "" )
		{
			Instance.InventoryEventArgs = null;
		}

		private void UpdateInventoryEventArgs( ShowInventoryEventArgs? @event )
		{
			Console.WriteLine("Updating inventory event args");
			this._showInventoryEvent = @event;
			this.StateHasChanged();
		}
	}
	#endregion
}
