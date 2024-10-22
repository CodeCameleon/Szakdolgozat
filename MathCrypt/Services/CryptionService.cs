using MathCrypt.Models;

namespace MathCrypt.Services;

/// <summary>
/// A titkosító algoritmust megvalósító szolgáltatás.
/// </summary>
public class CryptionService
{
    /// <summary>
    /// A kulcsról készített szótárt tároló adattag.
    /// A szótár kulcsai a kulcsban előforduló karakterek.
    /// A szótár értékei pedig a karakterek előfordulásának helye a kulcsban lista formájában.
    /// </summary>
    private readonly Dictionary<char, ShuffleList<Point>> _alphabet;

    /// <summary>
    /// A kulcsot tároló adattag.
    /// </summary>
    private char[][] _key;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public CryptionService()
    {
        _alphabet = [];
    }

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    public CryptionService(char[][] key)
    {
        _alphabet = [];
        _key = key;
        MapKey();
    }

    /// <summary>
    /// A kulcs publikus tulajdonsága.
    /// </summary>
    public char[][] Key
    {
        get
        {
            return _key;
        }

        set
        {
            _key = value;
            _alphabet.Clear();
            MapKey();
        }
    }

    /// <summary>
    /// Feltérképezi a kulcsot és egy szótárat készít róla.
    /// </summary>
    private void MapKey()
    {
        for (int j = 0; j < _key.Length; j++)
        {
            for (int i = 0; i < _key[j].Length ; i++)
            {
                if (_alphabet.TryGetValue(_key[j][i], out ShuffleList<Point> points))
                {
                    points.Add(new Point(j, i));
                }
                else
                {
                    points = new();
                    points.Add(new Point(j, i));
                    _alphabet.Add(_key[j][i], points);
                }
            }
        }
    }
}
