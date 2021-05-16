using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
#if !USER_INTERFACE
using CitizenFX.Core;
using Newtonsoft.Json;
#endif
using Diamond.Shared.Items.Bases;

namespace Diamond.Shared.Inventory
{
	public class BaseInventory<T> : IEnumerable<KeyValuePair<T, int>>
	{
		public Character Owner { get; set; }

		protected List<Type> _items = new List<Type>();
		protected List<int> _values = new List<int>();

		protected BaseInventory( Character owner )
		{
			this.Owner = owner;
		}

		public void AddItem( Type type, int amount = 1 )
		{
			if ( type == null || !type.IsSubclassOf( typeof( BaseItem ) ) ) return;

			if ( this._items.Contains( type ) )
			{
				int location = this._items.IndexOf( type );
				this._values[location] += amount;
			}
			else
			{
				this._items.Add( type );
				this._values.Insert( this._items.IndexOf( type ), amount );
			}

#if SERVER
            if (Owner == null || Owner.Player == null) return;
            Owner.Player.TriggerEvent("InventoryUpdated", GetType().FullName, type.FullName, amount);
#endif
		}

		public void AddItem( T item, int amount = 1 ) => this.AddItem( item.GetType(), amount );

		public void TakeItem( T item, int amount = 1 ) => this.AddItem( item, -amount );

		public bool HasItem( T item, int amount = 1 )
		{
			var type = item.GetType();

			if ( !this._items.Contains( type ) ) return false;
			return this._values[this._items.IndexOf( type )] >= amount;
		}

		public int GetCount( T item )
		{
			var type = item.GetType();
			return !this._items.Contains( type ) ? 0 : this._values[this._items.IndexOf( type )];
		}

		public Dictionary<T, int> AsDictionary() =>
			this.ToDictionary( kvp => kvp.Key, kvp => kvp.Value );

		#region IEnumerator

		public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
		{
			return new BaseInventoryEnumerator<T>( this._items, this._values );
		}

		private IEnumerator GetEnumerator1()
		{
			return this.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator1();
		}

		#endregion
	}

	public class BaseInventoryEnumerator<T> : IEnumerator<KeyValuePair<T, int>>
	{
		private int _position = 0;
		private List<Type> _items;
		private List<int> _values;

		private KeyValuePair<T, int>? _current;

		public KeyValuePair<T, int> Current
		{
			get
			{
				if ( this._current == null )
					throw new InvalidOperationException();

				return ( KeyValuePair<T, int> ) this._current;
			}
		}

		private object Current1 => this.Current;
		object IEnumerator.Current => this.Current1;

		public BaseInventoryEnumerator( List<Type> items, List<int> values )
		{
			this._items = items;
			this._values = values;
		}

		public bool MoveNext()
		{
			if ( this._items.Count <= 0 ) return false;
			if ( this._items.Count < this._position + 1 ) return false;

			object instance = Activator.CreateInstance( this._items[this._position] );

			if ( instance is T i ) this._current = new KeyValuePair<T, int>( i, this._values[this._position] );

			this._position++;
			return true;
		}

		public void Reset()
		{
			this._position = 0;
			this._current = null;
		}

		public void Dispose()
		{ }

		~BaseInventoryEnumerator() => this.Dispose();
	}
}
