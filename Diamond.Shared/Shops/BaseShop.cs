using System.Collections.Generic;
using System.Drawing;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared
{
	public abstract class BaseShop
	{
		public abstract string Name { get; }
		public abstract string UniqueId { get; }

#if !USER_INTERFACE
		public abstract List<Vector3> Locations { get; }
#endif

		public abstract List<IPurchasableItem> Items { get; }

#if CLIENT
		public abstract string BannerDict { get; }
		public abstract string BannerText { get; }

		public virtual BlipColor BlipColor => BlipColor.White;
		public virtual Color MarkerColor => Color.FromArgb( 255, 255, 255 );
#endif
		
#if CLIENT || USER_INTERFACE
		public virtual string ImageUrl => string.Empty;
#endif
	}
}
