using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Diamond.Shared.Items.Bases
{
    public abstract class BaseAmmoItem : BaseItem, IPurchasableItem, IUseableItem
    {
        public string UseWord => "Equip";
#if CLIENT
        public abstract int AmmoHash { get; }
#endif
        
        public abstract int Price { get; set; }
        public virtual int BulkAmount { get; set; } = 1;

        public abstract int Amount { get; }

        public bool CanBuy(Character character, int amount = 1) => 
            Utility.DefaultCanBuy(this, character, amount);

        public void OnBuy(Character character, int amount = 1) =>
            Utility.DefaultOnBuy(this, character, amount);

        public bool CanUse(Character character) => character.Alive && !character.Arrested;

#if SERVER
        public void OnUse(Character character) { }
#else
        public Task OnUse(Character character)
        {
            Debug.WriteLine("On use");
            API.AddPedAmmo(Game.PlayerPed.Handle, AmmoHash, Amount);
            return Task.FromResult(0);
        }
#endif
        
        

        
    }
}