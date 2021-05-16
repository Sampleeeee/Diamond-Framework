using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Diamond.Shared.Inventory;
using Diamond.Shared.Items.Bases;
using Diamond.Shared.Jobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Diamond.Shared.JsonConverters
{
	// TODO No longer need to convert inventories to Dictionary<string, int>, this will automatically do it.
	public class BaseItemJsonConverter : JsonConverter
	{
		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
		{
			if ( !( value is BaseItem item ) ) return;
			
			writer.WriteStartObject();
			
			writer.WritePropertyName("UniqueId");
			writer.WriteValue( item.UniqueId );
			
			writer.WriteEndObject();
		}

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
		{
			if ( objectType != typeof( BaseItem ) ) return existingValue;

			var jObject = JObject.Load( reader );
			return Utility.GetItem( jObject["UniqueId"]?.ToObject<string>() ?? string.Empty );
		}

		public override bool CanConvert( Type objectType ) =>
			objectType == typeof( BaseItem );
	}
	
	public class BaseItemTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType ) =>
			sourceType == typeof( string );

		public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType ) =>
			destinationType == typeof( string );

		public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
		{
			if ( destinationType != typeof( string ) ) return null;

			if ( !( value is BaseItem item ) ) return null;
			return item.UniqueId;
		}

		public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
		{
			if ( !( value is string val ) ) return null;
			return Utility.GetItem( val );
		}
	}
}
