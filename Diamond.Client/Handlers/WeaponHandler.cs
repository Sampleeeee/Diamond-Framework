using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared.Items.Bases;

namespace Diamond.Client.Handlers
{
	public class WeaponHandler : BaseScript
	{
		private int _lastGameTime = 0;

		[Tick]
		private async Task OnTick()
		{
			if ( MainClient.Character == null ) return;

			int gameTime = API.GetGameTimer();
			if ( gameTime < this._lastGameTime + 2000 ) return;

			var ped = Game.PlayerPed;

			foreach ( KeyValuePair<BaseItem, int> kvp in MainClient.Character.ItemInventory )
			{
				if ( !( kvp.Key is BaseWeaponItem weapon ) ) continue;
				Console.WriteLine( "Is weapon" );

				if ( !API.HasPedGotWeapon( ped.Handle, weapon.WeaponHash, false ) )
					API.GiveWeaponToPed( ped.Handle, weapon.WeaponHash, 0, false, false );
			}

			this._lastGameTime = gameTime;
		}
	}
}
