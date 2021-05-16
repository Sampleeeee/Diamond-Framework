using System.Collections.Generic;
using System.Drawing;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared.Shops
{
	public class TwentyFourSevenShop : BaseShop
	{
		public override string Name => "Twenty Four Seven";
		public override string UniqueId => GetType().FullName;

#if !USER_INTERFACE
		public override List<Vector3> Locations => new List<Vector3>()
		{
			new Vector3(374.26f, 325.91f, 103.57f - 0.975f),
			new Vector3(26.11f, -1347.82f, 29.5f - 0.975f),
			new Vector3(-3038.96f, 586.34f, 7.91f - 0.975f),
			new Vector3(-3241.55f, 1001.47f, 12.83f - 0.975f),
			new Vector3(547.31f, 2671.95f, 42.16f - 0.975f),
			new Vector3(1961.7f, 3740.25f, 32.34f - 0.975f)
		};
#endif

		public override List<IPurchasableItem> Items => new List<IPurchasableItem>()
		{
			new HotDogItem(), new WaterBottleItem(), new BeerItem()
		};

#if CLIENT
		public override string BannerDict => "shopui_title_conveniencestore";
		public override string BannerText => "shopui_title_conveniencestore";

		public override BlipColor BlipColor => BlipColor.Green;
		public override Color MarkerColor => Color.FromArgb( 0, 255, 0 );
#endif
	}
}
