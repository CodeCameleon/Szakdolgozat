namespace MathCrypt.Models;

/// <summary>
/// Egy különleges listát megvalósító osztály.
/// </summary>
/// <typeparam name="T">Az elem tipusa, amit a lista tárolni fog.</typeparam>
internal class ShuffleList<T>
    where T : class
{
    /// <summary>
    /// A listát tároló adattag.
    /// </summary>
    private readonly List<T> _list;

    /// <summary>
    /// A véletlenszám generátort tároló adattag.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Az aktuális elem indexét tároló adattag.
    /// </summary>
    private int _index;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public ShuffleList()
    {
        _list = [];
        _random = new Random();
        _index = 0;
    }

    /// <summary>
    /// Hozzáad egy új elemet a lista végére.
    /// </summary>
    /// <param name="item">A hozzáadni kivánt elem.</param>
    public void Add(T item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Visszaadja a listából a következő elemet.
    /// Ha eléri a végét, akkor megkeveri és újraindul az elejéről.
    /// </summary>
    /// <returns>Az aktuális elem a listából.</returns>
    public T Next()
    {
        switch (_list.Count)
        {
            case 0:
                return null;
            case 1:
                return _list[_index];
            default:
                T current = _list[_index];
                _index++;

                if (_index == _list.Count)
                {
                    Shuffle();
                    _index = 0;
                }

                return current;
        }
    }

    /// <summary>
    /// Megkeveri a lista elemeit.
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            int newIndex = _random.Next(i, _list.Count);
            (_list[i], _list[newIndex]) = (_list[newIndex], _list[i]);
        }
    }
}
