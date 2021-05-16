using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

			var client = new RestClient( $"https://{ParentResourceName}" );

			var request = new RestRequest( $"/{name}", Method.POST );
			request.AddParameter( "application/json", json,	ParameterType.RequestBody );

			var response = client.Execute( request );

			Console.WriteLine( response.Content );
			return response.Content;




			// var client = new RestClient( $"https://{ParentResourceName}/{name}" );
			// client.AddDefaultHeader( "content-type", "application/json" );
			//
			// var request = new RestRequest( Method.POST );
			// request.AddParameter( "application/json", json, ParameterType.RequestBody );
			//
			// Console.WriteLine(request.Parameters);
			//
			// var response = client.Execute( request );
			//
			// string content = response.Content;
			// Console.WriteLine( content );
			//
			// return content;
		}

		[JSInvokable( "OnNuiEvent" )]
		public static void OnNuiEvent( string name, string data )
		{
			Console.WriteLine( "Got event " + name );
			if ( !_events.ContainsKey( name ) ) return;

			foreach ( Action<string> callback in _events[name] )
				callback.Invoke( data );
		}

		[JSInvokable( "SetParentResourceName" )]
		public static void SetParentResourceName( string name )
		{
			ParentResourceName = name;
		}

		public static void AddEventHandler( string name, Action<string> callback )
		{
			if ( !_events.ContainsKey( name ) )
				_events[name] = new List<Action<string>>();

			Console.WriteLine( "Adding event handler for " + name );

			_events[name].Add( callback );
		}
	}
}
