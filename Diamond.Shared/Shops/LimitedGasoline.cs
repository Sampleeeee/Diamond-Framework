using System.Collections.Generic;
using System.Drawing;
using Diamond.Shared;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Client.Shops
{
	public class LimitedGasolineShop : BaseShop
	{
		public override string Name => "Limited Gasoline";
		public override string UniqueId => GetType().FullName;

#if !USER_INTERFACE
		public override List<Vector3> Locations => new List<Vector3>()
		{
			new Vector3(1163.25f, -323.98f, 69.21f - 0.975f),
			new Vector3(-48.81f, -1757.69f, 29.42f - 0.975f),
			new Vector3(-707.88f, -914.66f, 19.22f - 0.975f),
			new Vector3(1698.1f, 4924.78f, 42.06f - 0.975f),
			new Vector3(1729f, 6413.91f, 35.04f - 0.975f),
			new Vector3(2557.98f, 382.23f, 108.62f - 0.975f)
		};
#endif

		public override List<IPurchasableItem> Items => new List<IPurchasableItem>()
		{
			new HotDogItem(), new WaterBottleItem()
		};

#if CLIENT
		public override string BannerDict => "shopui_title_gasstation";
		public override string BannerText => "shopui_title_gasstation";

		public override BlipColor BlipColor => ( BlipColor )30;
		public override Color MarkerColor => Color.FromArgb( 0, 0, 255 );
#endif
	}
}
