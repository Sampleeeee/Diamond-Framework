using System.Collections.Generic;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;


// TODO all of these should be IPurchaseable
// I don't plan on adding crafting
namespace Diamond.Shared.Items
{
    public class HotDogItem : BaseFoodItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Hotdog";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "Yummy";

        public override int RestoreHunger => 60;
        
        public override string Model => "prop_cs_hotdog_01";

        public int Price { get; set; } = 50;
        public int BulkAmount { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}