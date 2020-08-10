using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

[PublicAPI]
public readonly struct SlottingList<T> : IList<T> where T : class {

  private readonly List<T> _slots;

  private readonly Queue<int> _vacancies;

  public SlottingList(int initialItemCapacity, int freeListCapacity) {
    _slots = new List<T>(initialItemCapacity);
    _vacancies = new Queue<int>(freeListCapacity);
  }

  public IEnumerator<T> GetEnumerator() {
    for (var i = 0; i < Allocated; ++i) {
      var item = _slots[i];
      if (item != null)
        yield return item;
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
    => GetEnumerator();

  public int Add(T item) {
    if (_vacancies.TryDequeue(out var index)) {
      _slots[index] = item;
    }
    else {
      index = Allocated;
      _slots.Add(item);
    }

    return index;
  }

  void ICollection<T>.Add(T item)
    => Add(item);

  public void Clear() {
    _slots.Clear();
    _vacancies.Clear();
  }

  public bool Contains(T item)
    => _slots.Contains(item);

  public void CopyTo(T[] array, int arrayIndex)
    => _slots.CopyTo(array, arrayIndex);

  public bool Remove(T item) {
    var index = _slots.IndexOf(item);
    if (index == -1)
      return false;

    _slots[index] = null!;
    _vacancies.Enqueue(index);
    return true;
  }

  public int Count
    => _slots.Count - _vacancies.Count;

  public int Allocated
    => _slots.Count;

  public int Free
    => _vacancies.Count;

  public int Capacity
    => _slots.Capacity;

  public bool IsReadOnly
    => false;

  public int IndexOf(T item)
    => _slots.IndexOf(item);

  public void Insert(int index, T item)
    => throw new NotSupportedException();

  public T RemoveAt(int index) {
    if (index < 0 || index > Allocated)
      throw new ArgumentOutOfRangeException(nameof(index));

    var v = _slots[index];
    _slots[index] = null!;
    _vacancies.Enqueue(index);
    return v;
  }

  void IList<T>.RemoveAt(int index)
    => RemoveAt(index);

  public T this[int index] {
    get => _slots[index];
    set => _slots[index] = value;
  }

  public bool TryGet(int index, out T item) {
    if (index >= Allocated) {
      item = null;
      return false;
    }

    item = this[index];
    return item != null;
  }

}