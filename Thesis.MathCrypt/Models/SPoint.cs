namespace Thesis.MathCrypt.Models;

/// <summary>
/// Egy pontot ábrázoló struktúra.
/// </summary>
internal struct SPoint
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
    /// A pont alapértelmezett konstruktora.
    /// </summary>
    public SPoint()
    {
        Row = 0;
        Column = 0;
    }

    /// <summary>
    /// A pont paraméteres konstruktora.
    /// </summary>
    /// <param name="row">A pont sorszáma.</param>
    /// <param name="column">A pont oszlopszáma.</param>
    public SPoint(int row, int column)
    {
        Row = row;
        Column = column;
    }

    /// <summary>
    /// Vissza ad egy karakterláncot, amely az aktuális objektumot reprezentálja.
    /// </summary>
    /// <returns>Egy karakterlánc, amely az aktuális objektumot reprezentálja.</returns>
    public override readonly string ToString()
    {
        return $"({Row}, {Column})";
    }
}
