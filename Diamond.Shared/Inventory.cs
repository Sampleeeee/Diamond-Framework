using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using Diamond.Shared.Items.Bases;
using Newtonsoft.Json;

namespace Diamond.Shared
{
    public class Inventory : IEnumerable<KeyValuePair<BaseItem, int>>
    {
        public int MaxWeight { get; set; }
        public Character Owner { get; set; }
        
        private List<Type> _items = new List<Type>();
        private List<int> _values = new List<int>();

        public Inventory(Character owner, int maxWeight = 100)
        {
            Owner = owner;
            MaxWeight = maxWeight;
        }

        public Inventory(Character owner, Dictionary<string, int> items, int maxWeight = 100)
        {
            Owner = owner;
            MaxWeight = maxWeight;
            FromDict(items);
        }

        public Inventory(int maxWeight = 100)
        {
            MaxWeight = maxWeight;
        }
        
        public Inventory(Dictionary<string, int> items, int maxWeight = 100)
        {
            MaxWeight = maxWeight;
            FromDict(items);
        }

        private void FromDict(Dictionary<string, int> items)
        {
            foreach (var kvp in items)
            {
                var type = Type.GetType(kvp.Key);
                if (type == null) continue;
                
                AddItem(type, kvp.Value);
            }
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
            Owner.Player.TriggerEvent("InventoryUpdated", type.FullName, amount);
#endif
        }
        
        public void AddItem(BaseItem item, int amount = 1) =>
            AddItem(item.GetType(), amount);

        public void TakeItem(BaseItem item, int amount = 1) =>
            AddItem(item, -amount);

        public int GetWeight() =>
            (from t in _items let item = Activator.CreateInstance(t) as BaseItem where item != null select item.Weight * _values[_items.IndexOf(t)]).Sum();

        public bool CanAddWeight(int weight) =>
            GetWeight() + weight <= MaxWeight;

        public bool HasItem(BaseItem item, int amount = 1)
        {
            var type = item.GetType();

            if (!_items.Contains(type)) return false;
            return _values[_items.IndexOf(type)] >= amount;
        }

        public int GetItemCount(BaseItem item)
        {
            var type = item.GetType();
            return !_items.Contains(type) ? 0 : _values[_items.IndexOf(type)];
        }

        public bool CanAddItem(BaseItem item, int amount = 1)
        {
            var stackSize = true;
            if (item.StackSize != -1)
                stackSize = GetItemCount(item) + amount <= item.StackSize;
            
            return stackSize && CanAddWeight(item.Weight * amount);
        }
            

        #region IEnumerator
        
        public IEnumerator<KeyValuePair<BaseItem, int>> GetEnumerator()
        {
            return new InventoryEnumerator(_items, _values);
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

    public class InventoryEnumerator : IEnumerator<KeyValuePair<BaseItem, int>>
    {
        private int _position = 0;
        private List<Type> _items;
        private List<int> _values;
        
        private KeyValuePair<BaseItem, int>? _current;

        public KeyValuePair<BaseItem, int> Current
        {
            get
            {
                if (_current == null)
                    throw new InvalidOperationException();

                return (KeyValuePair<BaseItem, int>) _current;
            }
        }

        private object Current1 => Current;
        object IEnumerator.Current => Current1;
        
        public InventoryEnumerator(List<Type> items, List<int> values)
        {
            _items = items;
            _values = values;
        }

        public bool MoveNext()
        {
            if (_items.Count <= 0) return false;
            if (_items.Count < _position + 1) return false;

            _current = new KeyValuePair<BaseItem, int>(Activator.CreateInstance(_items[_position]) as BaseItem, 
                _values[_position]);
            
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

        ~InventoryEnumerator() =>
            Dispose();
    }
}