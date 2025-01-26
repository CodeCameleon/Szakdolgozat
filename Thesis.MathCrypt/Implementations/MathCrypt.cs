using Shared.Constants;
using System.Text;
using Thesis.MathCrypt.Interfaces;
using Thesis.MathCrypt.Models;

namespace Thesis.MathCrypt.Implementations;

/// <summary>
/// A saját titkosító algoritmusomat megvalósító osztály.
/// </summary>
public class MathCrypt
    : IMathCrypt
{
    /// <summary>
    /// A titkosító algoritmus kulcsról készített szótárt tároló adattag.<br />
    /// Ahol a szótár kulcsai a titkosító algoritmus kulcsában előforduló karakterek.<br />
    /// A szótár értékei pedig a karakterek előfordulásának helyeinek listája.
    /// </summary>
    private readonly Dictionary<char, ShuffleList<SPoint>> _alphabet;

    /// <summary>
    /// Az oszlopméret ábrázolásához szükséges számjegyek számát tároló adattag.
    /// </summary>
    private int _colDigits;

    /// <summary>
    /// A titkosító algoritmus kulcsát tároló adattag.
    /// </summary>
    private char[][] _key;

    /// <summary>
    /// A sorméret ábrázolásához szükséges számjegyek számát tároló adattag.
    /// </summary>
    private int _rowDigits;

    /// <summary>
    /// Az algoritmus paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    public MathCrypt(char[][] key)
    {
        _alphabet = [];
        _key = key;
        MapKey();
    }

    /// <inheritdoc />
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
            MapKey();
        }
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        StringBuilder stringBuilder = new();
        SPoint currentPoint = new();
        int i = 0;

        while (i <= cipherText.Length - _rowDigits - _colDigits)
        {
            currentPoint.Row += int.Parse(cipherText.Substring(i, _rowDigits));
            currentPoint.Row %= _key.Length;
            i += _rowDigits;

            currentPoint.Column += int.Parse(cipherText.Substring(i, _colDigits));
            currentPoint.Column %= _key[0].Length;
            i += _colDigits;

            stringBuilder.Append(_key[currentPoint.Row][currentPoint.Column]);
        }

        return stringBuilder.ToString();
    }

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        StringBuilder stringBuilder = new();
        SPoint lastPoint = new();

        foreach (char character in plainText)
        {
            if (_alphabet.TryGetValue(character, out ShuffleList<SPoint>? points))
            {
                SPoint point = points.GetNext();

                stringBuilder.Append(CalculateMove(point, lastPoint));

                lastPoint = point;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.CharacterNotFound(character));
            }
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

        stringBuilder.AppendLine($"Column Digits: {_colDigits}");
        stringBuilder.AppendLine($"Row Digits: {_rowDigits}");
        stringBuilder.AppendLine("Alphabet:");
        stringBuilder.AppendLine("{");
        foreach (KeyValuePair<char, ShuffleList<SPoint>> pair in _alphabet.OrderBy(kvp => kvp.Key))
        {
            stringBuilder.Append("  ");
            stringBuilder.Append((int)pair.Key switch
            {
                0 => " NUL  ",
                1 => " SOH  ",
                2 => " STX  ",
                3 => " ETX  ",
                4 => " EOT  ",
                5 => " ENQ  ",
                6 => " ACK  ",
                7 => " BEL  ",
                8 => " BS   ",
                9 => " HT   ",
                10 => " LF   ",
                11 => " VT   ",
                12 => " FF   ",
                13 => " CR   ",
                14 => " SO   ",
                15 => " SI   ",
                16 => " DLE  ",
                17 => " DC1  ",
                18 => " DC2  ",
                19 => " DC3  ",
                20 => " DC4  ",
                21 => " NAK  ",
                22 => " SYN  ",
                23 => " ETB  ",
                24 => " CAN  ",
                25 => " EM   ",
                26 => " SUB  ",
                27 => " ESC  ",
                28 => " FS   ",
                29 => " GS   ",
                30 => " RS   ",
                31 => " US   ",
                32 => "Space ",
                127 => " DEL  ",
                _ => $" {pair.Key}    "
            });
            stringBuilder.Append($"=> {pair.Value}{Environment.NewLine}");
        }
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// A lépés normalizálásánál használt formula.
    /// </summary>
    /// <param name="number">Hány számjegyre kell normalizálni.</param>
    /// <returns>A normalizálási formukla.</returns>
    private static string FormatMove(int number)
    {
        return "{0:D" + number + "}";
    }

    /// <summary>
    /// Kiszámolja hogyan lehet eljutni a megadott kiindulási pontból az új pontba.
    /// </summary>
    /// <param name="newPoint">Az új pont.</param>
    /// <param name="oldPoint">A kiindulási pont.</param>
    /// <returns>A lépés normalizált alakban.</returns>
    private string CalculateMove(SPoint newPoint, SPoint oldPoint)
    {
        StringBuilder stringBuilder = new();
        int vertical, horizontal;

        if (newPoint.Row >= oldPoint.Row)
        {
            vertical = newPoint.Row - oldPoint.Row;
        }
        else
        {
            vertical = newPoint.Row + _key.Length - oldPoint.Row;
        }

        if (vertical.ToString().Length < _rowDigits)
        {
            stringBuilder.Append(string.Format(FormatMove(_rowDigits), vertical));
        }
        else
        {
            stringBuilder.Append(vertical);
        }

        if (newPoint.Column >= oldPoint.Column)
        {
            horizontal = newPoint.Column - oldPoint.Column;
        }
        else
        {
            horizontal = newPoint.Column + _key[0].Length - oldPoint.Column;
        }

        if (horizontal.ToString().Length < _colDigits)
        {
            stringBuilder.Append(string.Format(FormatMove(_colDigits), horizontal));
        }
        else
        {
            stringBuilder.Append(horizontal);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Feltérképezi a kulcsot és egy szótárat készít róla.
    /// </summary>
    private void MapKey()
    {
        _rowDigits = _key.Length.ToString().Length;
        _colDigits = _key[0].Length.ToString().Length;

        for (int j = 0; j < _key.Length; j++)
        {
            for (int i = 0; i < _key[j].Length; i++)
            {
                if (_alphabet.TryGetValue(_key[j][i], out ShuffleList<SPoint>? points))
                {
                    points.Add(new SPoint(j, i));
                }
                else
                {
                    points = [];
                    points.Add(new SPoint(j, i));
                    _alphabet.Add(_key[j][i], points);
                }
            }
        }
    }
}
