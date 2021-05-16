using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Diamond.UserInterface.Events;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Diamond.UserInterface
{
	public class Program
	{
		public static async Task Main( string[] args )
		{
			var builder = WebAssemblyHostBuilder.CreateDefault( args );
			builder.RootComponents.Add<App>( "#app" );

			builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) } );

			SetupAttributes();

			await builder.Build().RunAsync();
		}

		private static void SetupAttributes()
		{
			IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany( assembly => assembly.GetTypes() )
				.Where( type => type.IsSubclassOf( typeof( BaseNuiScript ) ) );

			foreach ( var type in types )
				Activator.CreateInstance( type );
		}
	}
}
