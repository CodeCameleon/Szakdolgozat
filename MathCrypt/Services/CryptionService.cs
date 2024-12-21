using MathCrypt.Helpers;
using MathCrypt.Models;
using System.Text;

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
    /// A sorméret ábrázolásához szükséges számjegyek számát tároló adattag.
    /// </summary>
    private int _rowDigits;

    /// <summary>
    /// Az oszlopméret ábrázolásához szükséges számjegyek számát tároló adattag.
    /// </summary>
    private int _colDigits;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public CryptionService()
    {
        _alphabet = [];
        _rowDigits = 0;
        _colDigits = 0;
    }

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    public CryptionService(char[][] key)
    {
        _alphabet = [];
        _key = key;
        _rowDigits = _key.Length.ToString().Length;
        _colDigits = _key[0].Length.ToString().Length;
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
            _alphabet.Clear();
            _key = value;
            _rowDigits = _key.Length.ToString().Length;
            _colDigits = _key[0].Length.ToString().Length;
            MapKey();
        }
    }

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg.</returns>
    public string Decrypt(string cipherText)
    {
        StringBuilder stringBuilder = new();
        Point currentPoint = new();
        int i = 0;

        while (i + _rowDigits + _colDigits <= cipherText.Length)
        {
            currentPoint.Row += int.Parse(cipherText.Substring(i, _rowDigits));
            currentPoint.Row %= _key.Length; 
            i += _rowDigits;

            currentPoint.Column += int.Parse(cipherText.Substring(i, _colDigits));
            currentPoint.Column %= _key[0].Length;
            i += _colDigits;
         
            stringBuilder.Append(
                _key[currentPoint.Row][currentPoint.Column]
            );
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg.</returns>
    public string Encrypt(string plainText)
    {
        StringBuilder stringBuilder = new();
        Point lastPoint = new();

        foreach (char character in plainText)
        {
            if (!_alphabet.TryGetValue(character, out ShuffleList<Point> points))
            {
                throw new ArgumentException(StringHelper.Error.MissingCharacter(character));
            }

            Point point = points.GetNext();

            stringBuilder.Append(CalculateMove(point, lastPoint));

            lastPoint = point;
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Vissza ad egy karakterláncot, amely az aktuális objektumot reprezentálja.
    /// </summary>
    /// <returns>Egy karakterlánc, amely az aktuális objektumot reprezentálja.</returns>
    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendLine("{");
        foreach (KeyValuePair<char, ShuffleList<Point>> pair in _alphabet.OrderBy(kvp => kvp.Key))
        {
            stringBuilder.Append("  ");
            switch ((int) pair.Key)
            {
                case 8:
                    stringBuilder.Append($" BS   ");
                    break;
                case 9:
                    stringBuilder.Append($" HT   ");
                    break;
                case 10:
                    stringBuilder.Append($" LF   ");
                    break;
                case 12:
                    stringBuilder.Append($" FF   ");
                    break;
                case 13:
                    stringBuilder.Append($" CR   ");
                    break;
                case 32:
                    stringBuilder.Append($"Space ");
                    break;
                default:
                    stringBuilder.Append($" {pair.Key}    ");
                    break;
            }
            stringBuilder.Append($"=> {pair.Value}" + Environment.NewLine);
        }
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Kiszámolja hogyan lehet eljutni a megadott kiindulási pontból az új pontba.
    /// </summary>
    /// <param name="newPoint">Az új pont.</param>
    /// <param name="oldPoint">A kiindulási pont.</param>
    /// <returns>A lépés normalizált alakban.</returns>
    private string CalculateMove(Point newPoint, Point oldPoint)
    {
        int vertical, horizontal;
        string move = string.Empty;

        if (newPoint.Row >= oldPoint.Row)
        {
            vertical = newPoint.Row - oldPoint.Row;
        }
        else
        {
            vertical = newPoint.Row + _key.Length - oldPoint.Row;
        }

        if (newPoint.Column >= oldPoint.Column)
        {
            horizontal = newPoint.Column - oldPoint.Column;
        }
        else
        {
            horizontal = newPoint.Column + _key[0].Length - oldPoint.Column;
        }

        if (vertical.ToString().Length < _rowDigits)
        {
            move += string.Format(StringHelper.FormatMove(_rowDigits), vertical);
        }
        else
        {
            move += vertical;
        }

        if (horizontal.ToString().Length < _colDigits)
        {
            move += string.Format(StringHelper.FormatMove(_colDigits), horizontal);
        }
        else
        {
            move += horizontal;
        }

        return move;
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
