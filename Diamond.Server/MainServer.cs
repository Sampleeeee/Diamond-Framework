using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using Diamond.Shared;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.Jobs;
using Newtonsoft.Json;

namespace Diamond.Server
{
    public class MainServer : BaseScript
    {
        public static MainServer Instance;
        
        public Dictionary<Player, Character> Characters = new Dictionary<Player, Character>();
        
        public MainServer()
        {
            Instance = this;
            Debug.WriteLine("Creating Instance");
        }

        [EventHandler("PlayerReady")]
        public void PlayerReady([FromSource] Player player)
        {
            var character = new Character
            {
                Player = player,
                FirstName = "Bill",
                LastName = "Microsoft",
                Job = new TrooperJob(),
                JobGrade = new TrooperJob.Sergeant(),
                Money = 10000,
                Bank = 5000,
                DirtyMoney = 500,
                Hunger = 50,
                Thirst = 50,
                Age = 82,
            };
            
            character.ItemInventory = new ItemInventory(character);

            Characters[player] = character;

            string json = JsonConvert.SerializeObject(character);
            player.TriggerEvent("SetupCharacter", json);
            
            character.ItemInventory.AddItem(new HotDogItem(), 10);
            character.ItemInventory.AddItem(new CombatPistolItem());
            character.ItemInventory.AddItem(new PistolAmmoItem());
        }

        [EventHandler("GiveItem")]
        private void GiveItem([FromSource] Player player, int iOther, string sItem, int amount)
        {
            Debug.WriteLine("Giving Item");
            var other = Players[iOther];

            if (player.Character.Position.DistanceToSquared(other.Character.Position) > 4 * 4) return;
            
            var character = Characters[player];
            var otherCharacter = Characters[other];
            var item = Shared.Utility.GetItem(sItem);

            if (!character.ItemInventory.HasItem(item, amount))
            {
                Debug.WriteLine("no have");
                return;
            };
            if (!otherCharacter.ItemInventory.CanAddItem(item, amount))
            {
                Debug.WriteLine("can no give");
                return;
            };
            
            character.ItemInventory.TakeItem(item, amount);
            otherCharacter.ItemInventory.AddItem(item, amount);
            
            Debug.WriteLine("Gave Item");
        }

        [EventHandler("BuyItem")]
        private void OnBuyItem([FromSource] Player player, string sShop, string sItem)
        {
            var shop = Shared.Utility.GetShop(sShop);
            var item = Shared.Utility.GetItem(sItem);

            if (!(item is IPurchasableItem purchasableItem)) return;
            if (!shop.Locations.Any(location => player.Character.Position.DistanceToSquared(location) < 4 * 4) &&
                shop.Items.Contains(purchasableItem)) return;

            var character = Characters[player];
            if (!character.CanAfford(purchasableItem.Price)) return;
            if (!purchasableItem.CanBuy(character)) return;
            
            character.Money -= purchasableItem.Price;
            character.ItemInventory.AddItem(item, purchasableItem.BulkAmount);
            
            purchasableItem.OnBuy(character);
        }

        [EventHandler("UseItem")]
        private void OnUseItem([FromSource] Player player, string sItem)
        {
            var item = Shared.Utility.GetItem(sItem);
            var character = Characters[player];

            if (!character.ItemInventory.HasItem(item)) return;
            if (!(item is IUseableItem useableItem)) return;
            if (!useableItem.CanUse(character)) return;
            
            character.ItemInventory.TakeItem(item);
            useableItem.OnUse(character);
            
            character.Player.TriggerEvent("UsedItem", item.UniqueId);
        }
    }
}
