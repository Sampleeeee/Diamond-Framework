using System;
using Diamond.UserInterface.Shared;
using Microsoft.AspNetCore.Components;

namespace Diamond.UserInterface.Events
{
	public class Events : BaseNuiScript
	{
		[Inject] private NavigationManager Navigation { get; set; }
		
		[NuiEventHandler("open")]
		public void OnOpen(string data)
		{
			MainLayout.ShouldShowLayout = true;
		}

		[NuiEventHandler("close")]
		public void OnClose()
		{
			MainLayout.ShouldShowLayout = false;
		}
		
		[NuiEventHandler("gay")]
		private void OnGayEvent(string data)
		{
			Console.WriteLine("Got gay event! " + data);
			Console.WriteLine(Communicator.TriggerNuiCallback("test", new {someVar = "gay"}));
		}
	}
}
