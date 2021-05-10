using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Diamond.Client.Extensions;
using Diamond.Shared;
using Diamond.Shared.Items.Bases;
using MenuAPI;

namespace Diamond.Client.Handlers
{
    public class ShopHandler : BaseScript
    {
        private Menu _menu = new Menu(" ");
        private bool _menuOpen = false;
        
        public ShopHandler()
        {
            CreateBlips();
            
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
            MenuController.AddMenu(_menu);
            
            _menu.OnMenuOpen += menu => _menuOpen = true;
            _menu.OnMenuClose += menu => _menuOpen = false;
        }
        
        private static void CreateBlips()
        {
            foreach (var shop in Shared.Utility.GetAllShops())
            {
                foreach (var blip in shop.Locations.Select(location => World.CreateBlip(location)))
                {
                    blip.IsShortRange = true;
                    blip.Sprite = BlipSprite.Store;
                    
                    // These must be set after changing the blip
                    blip.Color = shop.BlipColor;
                    blip.Name = shop.Name;
                }
            }
        }

        public void OpenMenu(BaseShop shop)
        {
            _menu.MenuSubtitle = shop.Name;
            _menu.HeaderTexture = new KeyValuePair<string, string>(shop.BannerDict, shop.BannerText);
            _menu.ClearMenuItems();

            foreach (var item in shop.Items)
            {
                if (!(item is BaseItem i)) continue;

                var menuItem = new MenuItem(i.Name, i.Description) { Label = $"${item.Price} "};

                if (!MainClient.Character.CanAfford(item.Price))
                    menuItem.Enabled = false;
                
                _menu.OnItemSelect += (menu, mItem, index) =>
                {
                    if (menuItem == mItem)
                        TriggerServerEvent("BuyItem", shop.UniqueId, i.UniqueId);
                };
                    
                _menu.AddMenuItem(menuItem);
            }
            
            _menu.OpenMenu();
        }

        [Tick]
        private Task DrawShopMarkers()
        {
            foreach (var shop in Shared.Utility.GetAllShops())
            {
                if (shop.Items.Count == 0) continue;
                
                foreach (var location in shop.Locations)
                {
                    location.DrawMarkerHere(out float dist, MarkerType.HorizontalCircleSkinny, null, null, null, shop.MarkerColor);
                    if (dist > 2 * 2) continue;

                    if (!_menuOpen)
                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open this shop.");

                    if (!Game.IsControlJustPressed(1, Control.Context)) continue;
                    
                    if (_menuOpen)
                        _menu.CloseMenu();
                    else
                        OpenMenu(shop);
                }
            }

            return Task.FromResult(0);
        }
    }
}