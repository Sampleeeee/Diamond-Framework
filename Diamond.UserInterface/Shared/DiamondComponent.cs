using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Diamond.UserInterface.Shared
{
	public class DiamondComponent : ComponentBase
	{
		[Inject] protected IJSRuntime Javascript { get; set; }

		protected async Task TriggerNuiCallbackAsync( string name, object data )
		{
			string json = JsonConvert.SerializeObject( data );
			await this.Javascript.InvokeVoidAsync( "TriggerNuiCallback", name, json );
		}
	}
}
