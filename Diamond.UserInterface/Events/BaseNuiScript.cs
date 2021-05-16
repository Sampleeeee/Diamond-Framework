using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Diamond.UserInterface.Events
{
	public abstract class BaseNuiScript
	{
		internal BaseNuiScript()
		{
			foreach ( var method in GetMethods( typeof( NuiEventHandlerAttribute ) ) )
			{
				Type[] param = method.GetParameters().Select( p => p.ParameterType ).ToArray();
				var actionType = Expression.GetDelegateType( param.Concat( new[] { typeof( void ) } ).ToArray() );
				var attribute = method.GetCustomAttribute<NuiEventHandlerAttribute>();

				Communicator.AddEventHandler( attribute?.Name,
					method.IsStatic
						? ( Action<string> )Delegate.CreateDelegate( actionType, method )
						: ( Action<string> )Delegate.CreateDelegate( actionType, this, method ) );
			}
		}

		public IEnumerable<MethodInfo> GetMethods( Type t )
		{
			MethodInfo[] allMethods = GetType().GetMethods( BindingFlags.Public | BindingFlags.NonPublic |
																BindingFlags.Static | BindingFlags.Instance );

			return allMethods.Where( m => m.GetCustomAttributes( t, false ).Length > 0 );
		}
	}
}
