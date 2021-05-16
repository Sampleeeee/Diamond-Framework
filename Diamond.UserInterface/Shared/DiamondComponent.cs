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

			Console.WriteLine( "Sending json: " + json );
			await this.Javascript.InvokeVoidAsync( "TriggerNuiCallback", name, json );
			//
			// string execute =
		 //        "fetch(`https://${GetParentResourceName()}/" + name + "`, { method: 'POST', headers: { 'Content-Type': 'application/json; charset=UTF-8' }, body: JSON.stringify(" +
		 //        json + ") }).then(resp => console.log(resp));";
			//
			// await this.Javascript.InvokeVoidAsync( "eval", execute );
		}
	}
}
