using System.Dynamic;

namespace Diamond.Shared.Items.Bases
{
    public interface IPurchasableItem
    {
        int Price { get; set; }
        int BulkAmount { get; set; }

        bool CanBuy(Character character, int amount = 1);
        void OnBuy(Character character, int amount = 1);
    }

    public interface IAmazoomItem : IPurchasableItem
    {
        int ManufactureTime { get; set; }
    }

    public interface IBlackMarketItem : IPurchasableItem
    {
        int BlackMarketChance { get; set; }
        int PriceFluctuation { get; set; }
    }
}