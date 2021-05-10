using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared.Items.Bases;

namespace Diamond.Client.Handlers
{
    public class WeaponHandler : BaseScript
    {
        public WeaponHandler()
        {
            // var timer = new Timer(2000) { Enabled = true, AutoReset = true };
            // timer.Elapsed += OnTimerElapsed;
            //
            // timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // var ped = Game.PlayerPed;
            // foreach (KeyValuePair<BaseItem, int> kvp in MainClient.Character.Inventory)
            // {
            //     if (!(kvp.Key is BaseWeaponItem weapon)) return;
            //     
            //     if (!API.HasPedGotWeapon(ped.Handle, weapon.WeaponHash, false))
            //         API.GiveWeaponToPed(ped.Handle, weapon.WeaponHash, 0, false, false);
            // }
            //
            // // TODO this will not set ammo to 0 if the player doesn't have any ammo.
            // // ex. he has a combat pistol but no pistol ammo. He could pick up ammo from the ground.
            // foreach (KeyValuePair<BaseItem, int> kvp in MainClient.Character.Inventory.Where(kvp => kvp.Key is BaseAmmoItem))
            // {
            //     if (!(kvp.Key is BaseAmmoItem ammo)) return;
            //     API.SetPedAmmoByType(ped.Handle, ammo.AmmoHash, kvp.Value);
            // }
        }
    }
}