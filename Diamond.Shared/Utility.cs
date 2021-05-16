using System;
using System.Collections.Generic;
using System.Linq;
using Diamond.Shared.Items.Bases;

#if !USER_INTERFACE
using CitizenFX.Core;
#endif

namespace Diamond.Shared
{
	public static class Utility
	{
		private static readonly List<BaseItem> _itemCache = new List<BaseItem>();
		private static readonly List<BaseShop> _shopCache = new List<BaseShop>();

		public static bool DefaultCanBuy( BaseItem item, Character character, int amount ) =>
			!character.Arrested && character.Alive;
		public static void DefaultOnBuy( BaseItem item, Character character, int amount )
		{
			// throw new System.NotImplementedException();
		}

		public static bool DefaultCanUse( IUseableItem item, Character character ) =>
			!character.Arrested && character.Alive;

		public static void DefaultOnUse( IUseableItem item, Character character )
		{
			// throw new System.NotImplementedException();
			// if (!item.CanUse(character)) return;
		}

		public static List<BaseItem> GetAllItems()
		{
			if ( _itemCache.Count != 0 ) return _itemCache;

			var types = typeof( BaseItem )
				.Assembly
				.GetTypes()
				.Where( x => x.IsClass && !x.IsAbstract && x.IsSubclassOf( typeof( BaseItem ) ) );

			foreach ( var type in types )
				if ( Activator.CreateInstance( type ) is BaseItem i )
					_itemCache.Add( i );

			return _itemCache;
		}

		public static List<BaseShop> GetAllShops()
		{
			if ( _shopCache.Count != 0 ) return _shopCache;

			var types = typeof( BaseShop )
				.Assembly
				.GetTypes()
				.Where( x => x.IsClass && !x.IsAbstract && x.IsSubclassOf( typeof( BaseShop ) ) );

			foreach ( var type in types )
				if ( Activator.CreateInstance( type ) is BaseShop i )
					_shopCache.Add( i );

			return _shopCache;
		}

		public static BaseItem GetItem( string uniqueId ) =>
			GetAllItems().FirstOrDefault( x => x.UniqueId == uniqueId );

		public static BaseShop GetShop( string uniqueId ) =>
			GetAllShops().FirstOrDefault( x => x.UniqueId == uniqueId );

		public static int Clamp( int value, int min, int max )
		{
			if ( value > max )
				value = max;

			if ( value < min )
				value = min;

			return value;
		}
	}
}
