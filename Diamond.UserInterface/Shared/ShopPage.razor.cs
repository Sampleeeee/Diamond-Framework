using System.Threading.Tasks;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.UserInterface;
using Microsoft.AspNetCore.Components;

namespace Diamond.UserInterface.Shared
{
	public partial class ShopPage
	{
		[Parameter] public ShowShopEventArgs? EventArguments { get; set; }

		private int _itemCount = 1;
		private BaseItem? _selectedItem;

		private string GetActiveClass( BaseItem item, bool secondary = false ) =>
			this._selectedItem?.GetType() == item.GetType() ? "active" : "";

		private string GetStyle( BaseItem item ) => 
			string.IsNullOrWhiteSpace( item.ImageUrl ) ? string.Empty : $"background-image: url({item.ImageUrl})";

		private async Task OnBuy()
		{
			// TODO notify player
			if ( this._selectedItem == null ) return;
			
			await this.TriggerNuiCallbackAsync("BoughtShopItem", new ShopBuyItemEventArgs
			{
				Item = this._selectedItem,
				Count = this._itemCount
			});
		}
	}
}
