using System.Threading.Tasks;

namespace Diamond.Shared.Items.Bases
{
    public abstract class BaseAmmoItem : BaseItem, IPurchasableItem
    {
#if CLIENT
        public abstract int AmmoHash { get; }
#endif
        
        public abstract int Price { get; set; }
        public virtual int BulkAmount { get; set; } = 50;

        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);
    }
}