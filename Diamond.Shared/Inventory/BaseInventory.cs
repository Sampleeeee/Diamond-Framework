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

        protected BaseInventory(Character owner)
        {
            Owner = owner;
        }

        public void AddItem(Type type, int amount = 1)
        {
            if (type == null || !type.IsSubclassOf(typeof(BaseItem))) return;
            
            if (_items.Contains(type))
            {
                int location = _items.IndexOf(type);
                _values[location] += amount;
            }
            else
            {
                _items.Add(type);
                _values.Insert(_items.IndexOf(type),amount);
            }

#if SERVER
            if (Owner == null || Owner.Player == null) return;
            Owner.Player.TriggerEvent("InventoryUpdated", GetType().FullName, type.FullName, amount);
#endif
        }
        
        public void AddItem(T item, int amount = 1) =>
            AddItem(item.GetType(), amount);

        public void TakeItem(T item, int amount = 1) =>
            AddItem(item, -amount);

        public bool HasItem(T item, int amount = 1)
        {
            var type = item.GetType();

            if (!_items.Contains(type)) return false;
            return _values[_items.IndexOf(type)] >= amount;
        }

        public int GetCount(T item)
        {
            var type = item.GetType();
            return !_items.Contains(type) ? 0 : _values[_items.IndexOf(type)];
        }

        #region IEnumerator
        
        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return new BaseInventoryEnumerator<T>(_items, _values);
        }
        
        private IEnumerator GetEnumerator1()
        {
            return GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
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
                if (_current == null)
                    throw new InvalidOperationException();

                return (KeyValuePair<T, int>) _current;
            }
        }

        private object Current1 => Current;
        object IEnumerator.Current => Current1;
        
        public BaseInventoryEnumerator(List<Type> items, List<int> values)
        {
            _items = items;
            _values = values;
        }

        public bool MoveNext()
        {
            if (_items.Count <= 0) return false;
            if (_items.Count < _position + 1) return false;

            object instance = Activator.CreateInstance(_items[_position]);

            if (instance is T i)
                _current = new KeyValuePair<T, int>(i, _values[_position]);

            _position++;
            return true;
        }

        public void Reset()
        {
            _position = 0;
            _current = null;
        }

        public void Dispose()
        { }

        ~BaseInventoryEnumerator() =>
            Dispose();
    }
}
