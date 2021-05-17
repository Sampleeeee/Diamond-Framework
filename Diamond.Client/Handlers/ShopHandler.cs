using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Diamond.Client.Extensions;
using Diamond.Client.Wrappers;
using Diamond.Shared;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface;
using Newtonsoft.Json;

namespace Diamond.Client.Handlers
{
	public class ShopHandler : BaseScript
	{
		private bool _menuOpen = false;

		public ShopHandler()
		{
			CreateBlips();
			API.RegisterNuiCallbackType( "BoughtShopItem" );
		}

		private static void CreateBlips()
		{
			foreach ( var shop in Shared.Utility.GetAllShops() )
			{
				foreach ( var blip in shop.Locations.Select( location => World.CreateBlip( location ) ) )
				{
					blip.IsShortRange = true;
					blip.Sprite = BlipSprite.Store;

					// These must be set after changing the blip
					blip.Color = shop.BlipColor;
					blip.Name = shop.Name;
				}
			}
		}

		private void CloseShop()
		{
			Utility.SendNuiMessage( "HideShop" );
			Nui.DisableFocus();
			this._menuOpen = false;
		}

		public void OpenShop(BaseShop shop)
		{
			Utility.SendNuiMessage( "ShowShop", new ShowShopEventArgs
			{
				Shop = shop,
				PlayerInventory = MainClient.Character.ItemInventory.AsDictionary()
			} );
			
			Nui.EnableFocus( true, true );
			this._menuOpen = true;
		}

		[EventHandler( "__cfx_nui:BoughtShopItem" )]
		private void OnBoughtShopItem( IDictionary<string, object> data, CallbackDelegate callback )
		{
			var json = ( string ) data["data"];
			var @event = JsonConvert.DeserializeObject<ShopBuyItemEventArgs>( json );
			
			Network.TriggerServerEvent( "BoughtShopItem", @event );
			callback.Invoke( true );
		}

		[EventHandler( "BoughtItemConfirmed" )]
		private void OnBoughtItemConfirmed( string data )
		{
			var @event = JsonConvert.DeserializeObject<ShopBuyItemEventArgs>( data );

			if ( @event.Item is IPurchasableItem i )
				i.OnBuy( MainClient.Character );
			
			Debug.WriteLine("Successfully bought item!");
		}

		[Tick]
		private Task DrawShopMarkers()
		{
			foreach ( var shop in Shared.Utility.GetAllShops() )
			{
				if ( shop.Items.Count == 0 ) continue;

				foreach ( var location in shop.Locations )
				{
					location.DrawMarkerHere( out float dist, MarkerType.HorizontalCircleSkinny, null, null, null, shop.MarkerColor );
					if ( dist > 2 * 2 ) continue;

					if (!this._menuOpen)
						Screen.DisplayHelpTextThisFrame( "Press ~INPUT_CONTEXT~ to open this shop." );

					if ( !Game.IsControlJustPressed( 1, Control.Context ) ) continue;

					if ( this._menuOpen )
						this.CloseShop();
					else
						this.OpenShop( shop );
				}
			}

			return Task.FromResult( 0 );
		}
	}
}
