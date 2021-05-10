

using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Items
{
    public class LifeAlertItem : BaseItem, IAmazoomItem
    {
        public override string Name { get; set; } = "Life Alert";
        public override string Description { get; set; } = "Automatically contacts Government Officials with your location when you are knocked unconscious.";
        public override int Weight { get; set; } = 1;

        public int Price { get; set; } = 1000;
        public int BulkAmount { get; set; } = 1;
        public int ManufactureTime { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }

    public class CellPhoneItem : BaseItem, IAmazoomItem
    {
        public override string Name { get; set; } = "Cell Phone";
        public override string Description { get; set; } = "A mobile communication device. Press N to open the interface";
        public override int Weight { get; set; } = 1;

        public int Price { get; set; } = 1000;
        public int BulkAmount { get; set; } = 1;
        public int ManufactureTime { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
    
    public class WatchItem : BaseItem, IAmazoomItem
    {
        public override string Name { get; set; } = "Watch";
        public override string Description { get; set; } = "A high tech device used to show the time.";
        public override int Weight { get; set; } = 1;

        public int Price { get; set; } = 600;
        public int BulkAmount { get; set; } = 1;
        public int ManufactureTime { get; set; } = 1;
        
        public bool CanBuy(Character character, int amount = 1) =>
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}