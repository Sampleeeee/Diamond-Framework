using System.Collections.Generic;
using System.Drawing;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Shops
{
	public class RobsLiquorShop : BaseShop
	{
		public override string Name => "Rob's Liquor";
		public override string UniqueId => this.GetType().FullName;

#if !USER_INTERFACE
		public override List<Vector3> Locations => new List<Vector3>()
		{
			new Vector3(-1223.57f, -907f, 12.33f - 1f), // subtracting 1f because there is no mat
            new Vector3(-1487.12f, -379.95f, 40.16f - 0.95f),
			new Vector3(-2968.45f, 390.28f, 15.04f - 0.95f),
		};
#endif

		public override List<IPurchasableItem> Items => new List<IPurchasableItem>()
		{
			new HotDogItem(), new WaterBottleItem()
		};

#if CLIENT
		public override string BannerDict => "shopui_title_liquorstore2";
		public override string BannerText => "shopui_title_liquorstore2";

		public override BlipColor BlipColor => BlipColor.Red;
		public override Color MarkerColor => Color.FromArgb( 255, 0, 0 );
#endif
	}
}
