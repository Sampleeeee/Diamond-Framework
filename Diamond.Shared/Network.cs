#if !USER_INTERFACE

using Newtonsoft.Json;
using CitizenFX.Core;

namespace Diamond.Shared
{
	public static class Network
	{
#if CLIENT
		public static void TriggerServerEvent( string @event, object data ) =>
			BaseScript.TriggerServerEvent( @event, GetJson( data ) );
#elif SERVER
		public static void TriggerClientEvent( Player player, string @event, object data ) =>
			player.TriggerEvent( @event, GetJson( data ) );
#endif
		
		public static void TriggerEvent( string @event, object data )
		{
			string json = JsonConvert.SerializeObject( data );
			BaseScript.TriggerEvent( @event, json );
		}

		private static string GetJson( object data )
		{
			// TODO possible fault, if s is not valid json it will be sent anyways
			if ( data is string s ) return s;
			
			return JsonConvert.SerializeObject( data );
		}
	}
}

#endif
