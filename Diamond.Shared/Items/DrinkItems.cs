using System.Collections.Generic;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{
    public class WaterBottleItem : BaseDrinkItem, IPurchasableItem
    {
        public override string Name { get; set; } = "Water Bottle";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A refreshing drink";
        public override int Weight { get; set; } = 1;
        
        public override int TimeOverride => 2000;
        
        public int Price { get; set; } = 25;
        public int BulkAmount { get; set; } = 1;

        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}