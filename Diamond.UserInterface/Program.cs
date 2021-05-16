using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
			var methods = Assembly.GetExecutingAssembly().GetTypes()
				.SelectMany( t => t.GetMethods() )
				.Where( m => m.GetCustomAttributes( typeof( NuiEventHandlerAttribute ), false ).Length > 0 )
				.ToArray();
			
			foreach ( var method in methods )
			{
				Type[] param = method.GetParameters().Select( p => p.ParameterType ).ToArray();
				var actionType = Expression.GetDelegateType( param.Concat( new[] { typeof( void ) } ).ToArray() );
				var attribute = method.GetCustomAttribute<NuiEventHandlerAttribute>();

				if ( method.IsStatic )
				{
					Communicator.AddEventHandler( attribute?.Name,
						( Action<string> ) Delegate.CreateDelegate( actionType, method ) );
				}
				// else
				// {
				// 	Communicator.AddEventHandler( attribute?.Name,
				// 		( Action<string> ) Delegate.CreateDelegate( actionType, , method ) );
				// }
			}
		}
	}
}
