using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared.Items.Bases;
using MenuAPI;

namespace Diamond.Client.Menus
{
    // TODO this menu completely fucking sucks
    public class InventoryMenu : BaseScript
    {
        private Menu _menu = new Menu("Inventory", "All of your beloved items!");
        private bool _menuOpen = false;
        
        public InventoryMenu()
        {
            API.RegisterKeyMapping("inventory", "Open Inventory", "keyboard", "f2");
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
            MenuController.AddMenu(_menu);

            _menu.OnMenuOpen += menu => _menuOpen = true;
            _menu.OnMenuClose += menu => _menuOpen = false;
        }

        [Command("inventory")]
        public void Inventory()
        {
            if (_menuOpen)
            {
                _menu.CloseMenu();
                return;
            }
            
            _menu.ClearMenuItems();

            // TODO this might be super inefficient because I bet MenuAPI doesn't clear out unused submenus
            foreach (var kvp in MainClient.Character.Inventory)
            {
                var item = kvp.Key;
                var title = $"{item.Name} (x{kvp.Value})";
                var menuItem = new MenuItem(title, item.Description) { Label = "→→→" };

                var menu = new Menu(title, item.Description);
                _menu.AddMenuItem(menuItem);
                MenuController.AddSubmenu(_menu, menu);
                MenuController.BindMenuItem(_menu, menu, menuItem);
                
                // TODO add event for this
                if (item is IUseableItem useableItem)
                {
                    var useItem = new MenuItem(useableItem.UseWord, $"{useableItem.UseWord} this item");
                    menu.OnItemSelect += (selectedMenu, selectedItem, selectedIndex) =>
                    {
                        if (selectedItem == useItem)
                            TriggerServerEvent("UseItem", item.UniqueId);
                    };
                    menu.AddMenuItem(useItem);
                }
                
                var giveItem = new MenuItem("Give", "Give this item to a nearby player") {Label = "→→→"};
                menu.AddMenuItem(giveItem);

                var giveMenu = new Menu("Give Item", title);

                List<Player> players = Players.Where(x =>
                    x != Game.Player &&
                    x.Character.Position.DistanceToSquared(Game.PlayerPed.Position) < 4 * 4)
                    .ToList();

                if (players.Count == 0)
                {
                    giveMenu.AddMenuItem(new MenuItem("No nearby players."));
                }
                else
                {
                    foreach (var player in players)
                    {
                        var playerItem = new MenuItem(player.Name);
                        giveMenu.AddMenuItem(playerItem);
                    
                        // TODO make this an input for how many items you want to give them.
                        giveMenu.OnItemSelect += (sMenu, sItem, sIndex) =>
                            TriggerServerEvent("GiveItem", player.ServerId, kvp.Key.UniqueId, 1);
                    }
                }
                
                MenuController.AddSubmenu(menu, giveMenu);
                MenuController.BindMenuItem(menu, giveMenu, giveItem);
                
                // TODO make this work
                menu.AddMenuItem(new MenuItem("~r~Delete", "Delete this item") {});
            }

            _menu.OpenMenu();
        }
    }
}