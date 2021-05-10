using System.Collections.Generic;
using System.Drawing;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared
{
    public abstract class BaseShop
    {
        public abstract string Name { get; }
        public abstract string UniqueId { get; }

        public abstract List<Vector3> Locations { get; }
        public abstract List<IPurchasableItem> Items { get; }
        
#if CLIENT
        public abstract string BannerDict { get; }
        public abstract string BannerText { get; }
        
        public virtual BlipColor BlipColor => BlipColor.White;
        public virtual Color MarkerColor => Color.FromArgb(255, 255, 255);
#endif
    }
}