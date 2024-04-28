using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.JsonConverter;

namespace XrayGUI.Modle.SingboxOptions
{
    public class Listable<T> : IList<T>, IList, IReadOnlyList<T>
    {
        private readonly List<T> listValue;
        public Listable()
        {
            listValue = new List<T>();
        }
        public Listable(List<T> list)
        {
            listValue = list;
        }
        public T this[int index] { get => listValue[index]; set => listValue[index] = value; }
        object? IList.this[int index] { get => (listValue as IList)[index]; set => (listValue as IList)[index] = value; }

        public int Count => listValue.Count;

        public bool IsReadOnly => (listValue as IList).IsReadOnly;

        public bool IsFixedSize => (listValue as IList).IsFixedSize;

        public bool IsSynchronized => (listValue as IList).IsSynchronized;

        public object SyncRoot => (listValue as IList).SyncRoot;

        public void Add(T item) => listValue.Add(item);

        public int Add(object? value) => (listValue as IList).Add(value);

        public void Clear() => listValue.Clear();

        public bool Contains(T item) => listValue.Contains(item);

        public bool Contains(object? value) => (listValue as IList).Contains(value);

        public void CopyTo(T[] array, int arrayIndex) => listValue.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index) => (listValue as IList).CopyTo(array, index);

        public IEnumerator<T> GetEnumerator() => listValue.GetEnumerator();

        public int IndexOf(T item) => listValue.IndexOf(item);

        public int IndexOf(object? value) => (listValue as IList).IndexOf(value);

        public void Insert(int index, T item) => listValue.Insert(index, item);

        public void Insert(int index, object? value) => (listValue as IList).Insert(index, value);

        public bool Remove(T item) => listValue.Remove(item);

        public void Remove(object? value) => (listValue as IList).Remove(value);

        public void RemoveAt(int index) => listValue.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => listValue.GetEnumerator();
        public static implicit operator Listable<T>(List<T> list) => new(list);
        public static implicit operator List<T>(Listable<T> list) => list.listValue;
        public static implicit operator Listable<T>(T v) => new() { v };
    }
}
