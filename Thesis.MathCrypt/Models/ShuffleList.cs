using System.Collections;
using System.Collections.ObjectModel;
using System.Text;

namespace Thesis.MathCrypt.Models;

/// <summary>
/// Egy beépített keveréssel rendelkező listát megvalósító osztály.
/// </summary>
/// <typeparam name="T">A tárolt elemek típusa.</typeparam>
internal class ShuffleList<T>
    : IList<T>, IList, IReadOnlyList<T>
{
    /// <summary>
    /// A lista elemeit tároló adattag.
    /// </summary>
    private readonly List<T> _items;

    /// <summary>
    /// A véletlenszám generátort tároló adattag.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Az aktuális elem indexét tároló adattag.
    /// </summary>
    private int _index;

    /// <summary>
    /// A lista alapértelmezett konstruktora.
    /// </summary>
    public ShuffleList()
    {
        _items = [];
        _random = new();
    }

    /// <summary>
    /// A lista paraméteres konstruktora.
    /// </summary>
    /// <param name="capacity">A lista kapacitása.</param>
    public ShuffleList(int capacity)
    {
        _items = new(capacity);
        _random = new();
    }

    /// <summary>
    /// A lista paraméteres konstruktora.
    /// </summary>
    /// <param name="collection">A lista elemeit tartalmazó kollekció.</param>
    public ShuffleList(IEnumerable<T> collection)
    {
        _items = new(collection);
        _random = new();
    }

    /// <summary>
    /// A lista kapacitása.
    /// </summary>
    public int Capacity
    {
        get => _items.Capacity;
        set => _items.Capacity = value;
    }

    /// <summary>
    /// A lista elemeinek száma.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// A lista meghatározott méretű-e.
    /// </summary>
    bool IList.IsFixedSize => ((IList)_items).IsFixedSize;

    /// <summary>
    /// A kollekció csak olvasható-e.
    /// </summary>
    bool ICollection<T>.IsReadOnly => ((ICollection<T>)_items).IsReadOnly;

    /// <summary>
    /// A lista csak olvasható-e.
    /// </summary>
    bool IList.IsReadOnly => ((IList)_items).IsReadOnly;

    /// <summary>
    /// A kollekció szálbiztos-e.
    /// </summary>
    bool ICollection.IsSynchronized => ((ICollection)_items).IsSynchronized;

    /// <summary>
    /// A kollekció hozzáférését szinkronizáló objektum.
    /// </summary>
    object ICollection.SyncRoot => ((ICollection)_items).SyncRoot;

    /// <summary>
    /// A lista elemeinek elérése index alapján.
    /// </summary>
    /// <param name="index">Az elem indexe.</param>
    /// <returns>A lista elem az adott indexen.</returns>
    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    /// <summary>
    /// A lista elemeinek elérése index alapján.
    /// </summary>
    /// <param name="index">Az elem indexe.</param>
    /// <returns>A lista elem az adott indexen.</returns>
    object? IList.this[int index]
    {
        get => ((IList)_items)[index];
        set => ((IList)_items)[index] = value;
    }

    /// <summary>
    /// Hozzáad egy elemet a lista végére.
    /// </summary>
    /// <param name="item">A hozzáadni kivánt elem.</param>
    public void Add(T item) => _items.Add(item);

    /// <summary>
    /// Hozzáad egy elemet a listához.
    /// </summary>
    /// <param name="item">A hozzáadni kivánt elem.</param>
    /// <returns>A hozzáadott elem indexe ha sikeres volt a hozzáadás, különben -1.</returns>
    int IList.Add(object? item) => ((IList)_items).Add(item);

    /// <summary>
    /// Hozzáadja a kollekció elemeit a lista végére.
    /// </summary>
    /// <param name="collection">A hozzáadni kívánt kollekció.</param>
    public void AddRange(IEnumerable<T> collection) => _items.AddRange(collection);

    /// <summary>
    /// Viszaadja a listát egy csak olvasható kollekcióként.
    /// </summary>
    /// <returns>A lista csak olvasható kollekcióként.</returns>
    public ReadOnlyCollection<T> AsReadOnly() => _items.AsReadOnly();

    /// <summary>
    /// Bináris keresést végez a listán.
    /// </summary>
    /// <param name="index">A keresés kezdő indexe.</param>
    /// <param name="count">A keresés hossza.</param>
    /// <param name="item">A keresett elem.</param>
    /// <param name="comparer">Az elemek összehasonlítására használt függvény.</param>
    /// <returns>A keresett elem indexe ha megtalálható, különben egy negatív szám.</returns>
    public int BinarySearch(int index, int count, T item, IComparer<T>? comparer)
        => _items.BinarySearch(index, count, item, comparer);

    /// <summary>
    /// Bináris keresést végez a listán.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>A keresett elem indexe ha megtalálható, különben egy negatív szám.</returns>
    public int BinarySearch(T item) => _items.BinarySearch(item);

    /// <summary>
    /// Bináris keresést végez a listán.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <param name="comparer">Az elemek összehasonlítására használt függvény.</param>
    /// <returns>A keresett elem indexe ha megtalálható, különben egy negatív szám.</returns>
    public int BinarySearch(T item, IComparer<T>? comparer) => _items.BinarySearch(item, comparer);

    /// <summary>
    /// Törli az összes elemet a listából.
    /// </summary>
    public void Clear() => _items.Clear();

    /// <summary>
    /// Leellenőrzi, hogy az adott elem benne van-e a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>Igaz ha benne van, különben hamis.</returns>
    public bool Contains(T item) => _items.Contains(item);

    /// <summary>
    /// Leellenőrzi, hogy az adott elem benne van-e a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>Igaz ha benne van, különben hamis.</returns>
    bool IList.Contains(object? item) => ((IList)_items).Contains(item);

    /// <summary>
    /// Átalakítja a lista elemeit egy másik típusra.
    /// </summary>
    /// <typeparam name="TOutput">Az új típus.</typeparam>
    /// <param name="converter">Az átalakító függvény.</param>
    /// <returns>Az új típusú lista az átalakított elemekkel.</returns>
    public ShuffleList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        => new(_items.ConvertAll(converter));

    /// <summary>
    /// Átmásolja a lista elemeit egy tömbbe.
    /// </summary>
    /// <param name="array">A tömb, amibe másolni szeretnénk.</param>
    public void CopyTo(T[] array) => _items.CopyTo(array);

    /// <summary>
    /// Átmásolja a kollekció elemeit egy tömbbe.
    /// </summary>
    /// <param name="array">A tömb, amibe másolni szeretnénk.</param>
    /// <param name="arrayIndex">A tömb indexe, ahova másolni szeretnénk.</param>
    void ICollection.CopyTo(Array array, int arrayIndex) => ((ICollection)_items).CopyTo(array, arrayIndex);

    /// <summary>
    /// Átmásolja a lista elemeit egy tömbbe.
    /// </summary>
    /// <param name="index">A másolás kezdő indexe.</param>
    /// <param name="array">A tömb, amibe másolni szeretnénk.</param>
    /// <param name="arrayIndex">A tömb indexe, ahova másolni szeretnénk.</param>
    /// <param name="count">A másolás hossza.</param>
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
        => _items.CopyTo(index, array, arrayIndex, count);

    /// <summary>
    /// Átmásolja a lista elemeit egy tömbbe.
    /// </summary>
    /// <param name="array">A tömb, amibe másolni szeretnénk.</param>
    /// <param name="arrayIndex">A tömb indexe, ahova másolni szeretnénk.</param>
    public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    /// <summary>
    /// Biztosítja, hogy a lista kapacitása legalább annyi mint a megadott érték.
    /// </summary>
    /// <param name="capacity">A minimális kapacitás, amit biztosítani kell.</param>
    /// <returns>A lista új kapacitása.</returns>
    public int EnsureCapacity(int capacity) => _items.EnsureCapacity(capacity);

    /// <summary>
    /// Meghatározza, hogy a lista tartalmazza-e olyan elemet, amely megfelel a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Igaz ha van ilyen elem, különben hamis.</returns>
    public bool Exists(Predicate<T> match) => _items.Exists(match);

    /// <summary>
    /// Visszaadja az első olyan elemet, amely megfelel a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az első megfelelő elem ha van ilyen, különben a típus alapértelmezett értéke.</returns>
    public T? Find(Predicate<T> match) => _items.Find(match);

    /// <summary>
    /// Visszaadja az összes olyan elemet, amelyek megfelelnek a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az összes megfelelő elem egy listában ha van ilyen, különben egy üres lista.</returns>
    public ShuffleList<T> FindAll(Predicate<T> match) => new(_items.FindAll(match));

    /// <summary>
    /// Visszaadja az első olyan elem indexét, amely megfelel a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az első megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindIndex(Predicate<T> match) => _items.FindIndex(match);

    /// <summary>
    /// Visszaadja az első olyan elem indexét, amely megfelel a megadott feltételnek.
    /// </summary>
    /// <param name="startIndex">A keresés kezdő indexe.</param>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az első megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindIndex(int startIndex, Predicate<T> match) => _items.FindIndex(startIndex, match);

    /// <summary>
    /// Visszaadja az első olyan elem indexét, amely megfelel a megadott feltételnek.
    /// </summary>
    /// <param name="startIndex">A keresés kezdő indexe.</param>
    /// <param name="count">A keresés hossza.</param>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az első megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindIndex(int startIndex, int count, Predicate<T> match)
        => _items.FindIndex(startIndex, count, match);

    /// <summary>
    /// Visszaadja az utolsó olyan elemet, amely megfelel a megadott feltételnek.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az utolsó megfelelő elem ha van ilyen, különben a típus alapértelmezett értéke.</returns>
    public T? FindLast(Predicate<T> match) => _items.FindLast(match);

    /// <summary>
    /// Visszaadja az utolsó olyan elem indexét, amely megfelel a megadott feltételnek.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az utolsó megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindLastIndex(Predicate<T> match) => _items.FindLastIndex(match);

    /// <summary>
    /// Visszaadja az utolsó olyan elem indexét, amely megfelel a megadott feltételnek.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="startIndex">A keresés kezdő indexe.</param>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az utolsó megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindLastIndex(int startIndex, Predicate<T> match) => _items.FindLastIndex(startIndex, match);

    /// <summary>
    /// Visszaadja az utolsó olyan elem indexét, amely megfelel a megadott feltételnek.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="startIndex">A keresés kezdő indexe.</param>
    /// <param name="count">A keresés hossza.</param>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Az utolsó megfelelő elem indexe ha van ilyen, különben -1.</returns>
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        => _items.FindLastIndex(startIndex, count, match);

    /// <summary>
    /// Végre hajtja a megadott műveletet minden elemen a listában.
    /// </summary>
    /// <param name="action">A végrehajtandó művelet.</param>
    public void ForEach(Action<T> action) => _items.ForEach(action);

    /// <summary>
    /// Visszaad egy enumerátort a listához.
    /// </summary>
    /// <returns>Egy enumerátort a listához.</returns>
    public List<T>.Enumerator GetEnumerator() => _items.GetEnumerator();

    /// <summary>
    /// Visszaad egy enumerátort a kollekcióhoz.
    /// </summary>
    /// <returns>Egy enumerátort a kollekcióhoz.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_items).GetEnumerator();

    /// <summary>
    /// Visszaad egy enumerátort a kollekcióhoz.
    /// </summary>
    /// <returns>Egy enumerátort a kollekcióhoz.</returns>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

    /// <summary>
    /// Visszaadja a következő elemet a listából.
    /// Ha eléri a lista végét, akkor megkeveri és az elejéről kezdi újra.
    /// </summary>
    /// <returns>A következő elem ha a lista nem üres, különben a típus alapértelmezett értéke.</returns>
    public T? GetNext()
    {
        if (_items.Count == 0)
        {
            return default;
        }

        if (_items.Count == 1)
        {
            return _items[_index];
        }

        T current = _items[_index++];

        if (_index == _items.Count)
        {
            Shuffle();
            _index = 0;
        }

        return current;
    }

    /// <summary>
    /// Visszaad egy sekély másolatot a lista kiválasztott részéről.
    /// </summary>
    /// <param name="index">A másolat kezdő indexe.</param>
    /// <param name="count">A másolat hossza.</param>
    /// <returns>A lista kiválasztott részének sekély másolata.</returns>
    public ShuffleList<T> GetRange(int index, int count) => new(_items.GetRange(index, count));

    /// <summary>
    /// Visszaad egy sekély másolatot a lista kiválasztott részéről.
    /// </summary>
    /// <param name="start">A másolat kezdő indexe.</param>
    /// <param name="length">A másolat hossza.</param>
    /// <returns>A lista kiválasztott részének sekély másolata.</returns>
    public ShuffleList<T> Slice(int start, int length) => new(_items.Slice(start, length));

    /// <summary>
    /// Megkeresi az adott elem első előfordulásának indexét a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>A keresett elem első előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int IndexOf(T item) => _items.IndexOf(item);

    /// <summary>
    /// Megkeresi az adott elem indexét a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>A keresett elem indexe ha megtalálható, különben -1.</returns>
    int IList.IndexOf(object? item) => ((IList)_items).IndexOf(item);

    /// <summary>
    /// Megkeresi az adott elem első előfordulásának indexét a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <param name="index">A keresés kezdő indexe.</param>
    /// <returns>A keresett elem első előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int IndexOf(T item, int index) => _items.IndexOf(item, index);

    /// <summary>
    /// Megkeresi az adott elem első előfordulásának indexét a listában.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <param name="index">A keresés kezdő indexe.</param>
    /// <param name="count">A keresés hossza.</param>
    /// <returns>A keresett elem első előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int IndexOf(T item, int index, int count) => _items.IndexOf(item, index, count);

    /// <summary>
    /// Beszúr egy elemet a listába az adott indexre.
    /// </summary>
    /// <param name="index">A beszúrás helye.</param>
    /// <param name="item">A beszúrandó elem.</param>
    public void Insert(int index, T item) => _items.Insert(index, item);

    /// <summary>
    /// Beszúr egy elemet a listába az adott indexre.
    /// </summary>
    /// <param name="index">A beszúrás helye.</param>
    /// <param name="item">A beszúrandó elem.</param>
    void IList.Insert(int index, object? item) => ((IList)_items).Insert(index, item);

    /// <summary>
    /// Beszúr egy kollekciót a listába az adott indexre.
    /// </summary>
    /// <param name="index">A beszúrás helye.</param>
    /// <param name="collection">A beszúrandó kollekció.</param>
    public void InsertRange(int index, IEnumerable<T> collection) => _items.InsertRange(index, collection);

    /// <summary>
    /// Megkeresi az adott elem utolsó előfordulásának indexét a listában.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <returns>A keresett elem utolsó előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int LastIndexOf(T item) => _items.LastIndexOf(item);

    /// <summary>
    /// Megkeresi az adott elem utolsó előfordulásának indexét a listában.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <param name="index">A keresés kezdő indexe.</param>
    /// <returns>A keresett elem utolsó előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int LastIndexOf(T item, int index) => _items.LastIndexOf(item, index);

    /// <summary>
    /// Megkeresi az adott elem utolsó előfordulásának indexét a listában.
    /// A keresés visszafelé történik.
    /// </summary>
    /// <param name="item">A keresett elem.</param>
    /// <param name="index">A keresés kezdő indexe.</param>
    /// <param name="count">A keresés hossza.</param>
    /// <returns>A keresett elem utolsó előfordulásának indexe ha megtalálható, különben -1.</returns>
    public int LastIndexOf(T item, int index, int count) => _items.LastIndexOf(item, index, count);

    /// <summary>
    /// Törli az első előfordulását az adott elemnek a listában.
    /// </summary>
    /// <param name="item">A törölni kívánt elem.</param>
    /// <returns>
    /// Igaz ha sikerült a törlés, különben hamis.
    /// Akkor is hamis, ha az elem nem található a listában.
    /// </returns>
    public bool Remove(T item) => _items.Remove(item);

    /// <summary>
    /// Törli az első előfordulását az adott elemnek a listában.
    /// </summary>
    /// <param name="item">A törölni kívánt elem.</param>
    void IList.Remove(object? item) => ((IList)_items).Remove(item);

    /// <summary>
    /// Törli az összes elemet a listából, amelyek megfelelnek a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>A törölt elemek száma.</returns>
    public int RemoveAll(Predicate<T> match) => _items.RemoveAll(match);

    /// <summary>
    /// Törli az adott indexű elemet a listából.
    /// </summary>
    /// <param name="index">A törölni kívánt elem indexe.</param>
    public void RemoveAt(int index) => _items.RemoveAt(index);

    /// <summary>
    /// Törli a megadott tartományban lévő elemeket a listából.
    /// </summary>
    /// <param name="index">A törlés kezdő indexe.</param>
    /// <param name="count">A törlés hossza.</param>
    public void RemoveRange(int index, int count) => _items.RemoveRange(index, count);

    /// <summary>
    /// Megfordítja a lista elemeinek sorrendjét.
    /// </summary>
    public void Reverse() => _items.Reverse();

    /// <summary>
    /// Megfordítja a lista elemeinek sorrendjét.
    /// </summary>
    /// <param name="index">A megfordítás kezdő indexe.</param>
    /// <param name="count">A megfordítás hossza.</param>
    public void Reverse(int index, int count) => _items.Reverse(index, count);

    /// <summary>
    /// Megkeveri a lista elemeit.
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            int newIndex = _random.Next(i, _items.Count);
            (_items[i], _items[newIndex]) = (_items[newIndex], _items[i]);
        }
    }

    /// <summary>
    /// Sorba rendezi a lista elemeit.
    /// </summary>
    public void Sort() => _items.Sort();

    /// <summary>
    /// Sorba rendezi a lista elemeit.
    /// </summary>
    /// <param name="comparer">Az elemek összehasonlítására használt függvény.</param>
    public void Sort(IComparer<T>? comparer) => _items.Sort(comparer);

    /// <summary>
    /// Sorba rendezi a lista elemeit.
    /// </summary>
    /// <param name="index">A sorba rendezés kezdő indexe.</param>
    /// <param name="count">A sorba rendezés hossza.</param>
    /// <param name="comparer">Az elemek összehasonlítására használt függvény.</param>
    public void Sort(int index, int count, IComparer<T>? comparer) => _items.Sort(index, count, comparer);

    /// <summary>
    /// Sorba rendezi a lista elemeit.
    /// </summary>
    /// <param name="comparison">Az azonos típusú elemek összehasonlítására használt függvény.</param>
    public void Sort(Comparison<T> comparison) => _items.Sort(comparison);

    /// <summary>
    /// Visszaad egy tömböt, amely tartalmazza a lista elemeinek másolatait.
    /// </summary>
    /// <returns>A lista elemeinek másolatainak tömbje.</returns>
    public T[] ToArray() => _items.ToArray();

    /// <summary>
    /// Vissza ad egy karakterláncot, amely az aktuális objektumot reprezentálja.
    /// </summary>
    /// <returns>Egy karakterlánc, amely az aktuális objektumot reprezentálja.</returns>
    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append('[');
        for (int i = 0; i < _items.Count; i++)
        {
            stringBuilder.Append(_items[i]);
            if (i < _items.Count - 1)
            {
                stringBuilder.Append("; ");
            }
        }
        stringBuilder.Append(']');

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Lecsökkenti a lista kapacitását az aktuális elemek számára.
    /// </summary>
    public void TrimExcess() => _items.TrimExcess();

    /// <summary>
    /// Meghatározza, hogy a lista minden eleme megfelel-e a megadott feltételnek.
    /// </summary>
    /// <param name="match">A keresési feltétel.</param>
    /// <returns>Igaz ha minden elem megfelel a feltételnek, különben hamis.</returns>
    public bool TrueForAll(Predicate<T> match) => _items.TrueForAll(match);
}
