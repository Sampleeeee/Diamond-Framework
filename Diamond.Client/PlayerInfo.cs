using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Diamond.Shared.Items;

namespace Diamond.Client
{
	// TODO Not too sure if this actually benefits performance.
	public class PlayerInfo : BaseScript
	{
		private static bool _pedSet = false;
		private static bool _playerSet = false;
		private static bool _vehicleSet = false;

		private static Ped _currentPed = null;
		public static Ped CurrentPed
		{
			get
			{
				if ( _pedSet ) return _currentPed;

				_currentPed = new Ped(API.PlayerPedId());
				_pedSet = true;

				return _currentPed;
			}
		}

		private static Player _currentPlayer = null;

		public static Player CurrentPlayer
		{
			get
			{
				if ( _playerSet ) return _currentPlayer;

				_currentPed = new Player( API.PlayerId() );
				_playerSet = true;
				
				return _currentPlayer;
			}
		}

		private static Vehicle _currentVehicle = null;
		public static Vehicle CurrentVehicle
		{
			get
			{
				if ( _vehicleSet ) return _currentVehicle;

				_currentVehicle = API.GetVehiclePedIsIn( CurrentPed.Handle, false );
				_vehicleSet = true;
				
				return _currentVehicle;
			}
		}
		
		[Tick]
		private static async Task OnTick()
		{
			ResetCache();
			await Delay( 500 );
		}

		private void ResetCache()
		{
			_pedSet = false;
			_currentPed = null;
			
			_vehicleSet = false;
			_currentVehicle = null;

			// Going to keep the player, afaik it doesn't change
			// _playerSet = false
		}
	}
}
