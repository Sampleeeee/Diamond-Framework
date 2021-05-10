using System.Collections.Generic;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;


namespace Diamond.Shared.Items
{
    public class KevlarItem : BaseItem
    {
        public override string Name { get; set; } = "Kevlar";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A protective vest that gives you 100 armor";
        public override int Weight { get; set; } = 2;

        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class RopeItem : BaseItem, IAmazoomItem
    {
        public override string Name { get; set; } = "Rope";
        public override string UniqueId => GetType().FullName;
        public override string Description { get; set; } = "A strong rope to tie people with.";
        public override int Weight { get; set; } = 1;

        public int Price { get; set; } = 1000;
        public int BulkAmount { get; set; } = 1;
        public int ManufactureTime { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}