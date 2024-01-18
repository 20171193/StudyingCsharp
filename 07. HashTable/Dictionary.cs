using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 1000;

        private struct Entry
        {
            public enum State { None, Using, Deleted }

            public State state;
            public TKey key;
            public TValue value;
        }

        private Entry[] table;
        private int count;

        public Dictionary()
        {
            table = new Entry[DefaultCapacity];
            count = 0;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (Find(key, out int index))
                {
                    return table[index].value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set
            {
                if (Find(key, out int index))
                {
                    table[index].value = value;
                }
                else
                {
                    if (count > table.Length * 0.7f)
                    {
                        ReHashing();
                    }

                    table[index].key = key;
                    table[index].value = value;
                    table[index].state = Entry.State.Using;
                    count++;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (Find(key, out int index))
            {
                throw new InvalidOperationException();
            }
            else
            {
                if (count > table.Length * 0.7f)
                {
                    ReHashing();
                }

                table[index].key = key;
                table[index].value = value;
                table[index].state = Entry.State.Using;
                count++;
            }
        }

        public void Clear()
        {
            table = new Entry[DefaultCapacity];
            count = 0;
        }

        public bool ContainsKey(TKey key)
        {
            if (Find(key, out int index))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(TKey key)
        {
            if (Find(key, out int index))
            {
                table[index].state = Entry.State.Deleted;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Find(TKey key, out int index)
        {
            if (key == null)
                throw new ArgumentNullException();

            index = Hash(key);
            for (int i = 0; i < table.Length; i++)
            {
                if (table[index].state == Entry.State.None)
                {
                    return false;
                }
                else if (table[index].state == Entry.State.Using &&
                    key.Equals(table[index].key))
                {
                    return true;
                }
                index = Hash2(index);
            }

            index = -1;
            throw new InvalidOperationException();
        }

        private int Hash(TKey key)
        {
            return Math.Abs(key.GetHashCode() % table.Length);
        }

        private int Hash2(int index)
        {
            // 선형 탐사
            return (index + 1) % table.Length;

            // 제곱 탐사
            // return (index + 1) * (index + 1) % table.Length;

            // 이중 해싱
            // return Math.Abs((index + 1).GetHashCode() % table.Length);
        }

        private void ReHashing()
        {
            Entry[] oldTable = table;
            table = new Entry[table.Length * 2];
            count = 0;

            for (int i = 0; i <  oldTable.Length; i++)
            {
                Entry entry = oldTable[i];
                if (entry.state == Entry.State.Using)
                {
                    Add(entry.key, entry.value);
                }
            }
        }
    }
}
