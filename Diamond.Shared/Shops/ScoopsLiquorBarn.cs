using System.Collections.Generic;
using System.Drawing;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Shops
{
	public class ScoopsLiquorBarn : BaseShop
	{
		public override string Name => "Scoops Liquor Barn";
		public override string UniqueId => GetType().FullName;

#if !USER_INTERFACE
		public override List<Vector3> Locations => new List<Vector3>()
		{
			new Vector3(1166.8f, 2708.7f, 38.16f - 1f),
		};
#endif

		public override List<IPurchasableItem> Items => new List<IPurchasableItem>()
		{
			new HotDogItem(), new WaterBottleItem()
		};

#if CLIENT
		public override string BannerDict => "shopui_title_liquorstore3";
		public override string BannerText => "shopui_title_liquorstore3";

		public override BlipColor BlipColor => ( BlipColor )26;
		public override Color MarkerColor => Color.FromArgb( 137, 209, 254 );
#endif
	}
}
