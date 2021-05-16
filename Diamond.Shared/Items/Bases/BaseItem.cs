using System;
using System.ComponentModel;
using Diamond.Shared.JsonConverters;
using Newtonsoft.Json;

namespace Diamond.Shared.Items.Bases
{
	[JsonConverter(typeof(BaseItemJsonConverter))]
	[TypeConverter( typeof( BaseItemTypeConverter ) )]
	public abstract class BaseItem
	{
		public abstract string Name { get; set; }
		public string UniqueId => this.GetType().FullName;
		public abstract string Description { get; set; }

		public virtual int Weight { get; set; } = 1;
		public virtual int StackSize { get; set; } = -1;

		public virtual bool Illegal { get; set; } = false;
		public virtual bool PremiumOnly { get; set; } = false;
		public virtual string ImageUrl => string.Empty;

		public override bool Equals( object obj ) => this.UniqueId == ( obj as BaseItem )?.UniqueId;
	}
}
