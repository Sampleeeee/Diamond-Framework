using System;

namespace Diamond.UserInterface.Events
{
	[AttributeUsage( AttributeTargets.Method )]
	public class NuiEventHandlerAttribute : Attribute
	{
		public string Name { get; private set; }

		public NuiEventHandlerAttribute( string name )
		{
			this.Name = name;
		}
	}
}
