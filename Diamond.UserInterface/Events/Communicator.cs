using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using RestSharp;

namespace Diamond.UserInterface.Events
{
	public static class Communicator
	{
		public static string AppStyle = "display: none";

		private static Dictionary<string, List<Action<string>>> _events = new();
		private static string ParentResourceName { get; set; }

		public static string TriggerNuiCallback( string name, dynamic data )
		{
			string json = JsonConvert.SerializeObject( data );
			Console.WriteLine( $"Triggering nui callback {name}: {json}" );

			var client = new RestClient( $"https://{ParentResourceName}/{name}" );
			client.AddDefaultHeader( "content-type", "application/json" );

			var request = new RestRequest( Method.POST )
			{ Body = JsonConvert.SerializeObject( data ) };

			var response = client.Execute( request );
			return response.Content;
		}

		[JSInvokable( "OnNuiEvent" )]
		public static void OnNuiEvent( string name, string data )
		{
			Console.WriteLine( "Got event " + name );
			if ( !_events.ContainsKey( name ) ) return;

			foreach ( Action<string> callback in _events[name] )
				callback?.Invoke( data );
		}

		[JSInvokable( "SetParentResourceName" )]
		public static void SetParentResourceName( string name )
		{
			ParentResourceName = name;
		}

		private static void Test()
		{
			if ( string.IsNullOrWhiteSpace( ParentResourceName ) ) return;

			var client = new RestClient( $"https://{ParentResourceName}/test" );
			client.AddDefaultHeader( "content-type", "application/json" );

			var request = new RestRequest( Method.POST );
			var response = client.Execute( request );
		}

		public static void AddEventHandler( string name, Action<string> callback )
		{
			if ( !_events.ContainsKey( name ) )
				_events[name] = new List<Action<string>>();

			_events[name].Add( callback );
		}
	}
}
