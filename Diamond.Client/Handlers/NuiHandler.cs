using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;

namespace Diamond.Client.Handlers
{
	public class NuiHandler : BaseScript
	{
		public NuiHandler()
		{
			API.RegisterNuiCallbackType( "test" );
			EventHandlers["__cfx_nui:test"] += new Action<object, CallbackDelegate>( OnTestEvent );
		}

		private static void OnTestEvent( object data, CallbackDelegate cb )
		{
			Debug.WriteLine( "Got test!!!" + data );
			cb( new
			{
				item = "Test"
			} );
		}
	}
}
