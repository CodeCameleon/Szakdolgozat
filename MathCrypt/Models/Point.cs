namespace MathCrypt.Models;

/// <summary>
/// Egy pontot ábrázoló osztály.
/// </summary>
internal class Point
{
    /// <summary>
    /// A pont sorszáma.
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// A pont oszlopszáma.
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    /// <param name="row">A pont sorszáma.</param>
    /// <param name="column">A pont oszlopszáma.</param>
    public Point(int row, int column)
    {
        Row = row;
        Column = column;
    }

    /// <summary>
    /// Vissza ad egy karakterláncot, amely az aktuális objektumot reprezentálja.
    /// </summary>
    /// <returns>Egy karakterlánc, amely az aktuális objektumot reprezentálja.</returns>
    public override string ToString()
    {
        return $"({Row}, {Column})";
    }
}
