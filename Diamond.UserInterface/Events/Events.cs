using System;
using Diamond.UserInterface.Shared;
using Microsoft.AspNetCore.Components;

namespace Diamond.UserInterface.Events
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Events
	{
		[Inject] private static NavigationManager Navigation { get; set; }

		[NuiEventHandler( "open" )]
		public static void OnOpen( string data )
		{
			MainLayout.ShouldShowLayout = true;
		}

		[NuiEventHandler( "close" )] 
		public static void OnClose( string data )
		{
			MainLayout.ShouldShowLayout = false;
		}
	}
}
