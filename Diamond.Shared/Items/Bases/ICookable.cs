using System.Collections.Generic;

namespace Diamond.Shared.Items.Bases
{
    public interface ICookableItem
    {
        int ExplosionChance { get; set; }
        int CookTime { get; set; }
        int AmountToCook { get; set; }
        Dictionary<BaseItem, int> Ingredients { get; set; }
    }
}