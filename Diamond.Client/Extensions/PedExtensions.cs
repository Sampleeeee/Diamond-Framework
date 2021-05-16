using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Diamond.Client.Extensions
{
	public static class PedExtensions
	{
		public static int GetPlayer( this Ped ped ) =>
			API.NetworkGetPlayerIndexFromPed( ped.Handle );
	}
}
