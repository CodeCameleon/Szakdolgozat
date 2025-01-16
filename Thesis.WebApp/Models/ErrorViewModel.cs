namespace Thesis.WebApp.Models;

/// <summary>
/// Egy hiba�zenetet �br�zol� n�zetmodell.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// A hib�s k�r�s azonos�t�ja.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Meg kell-e jelen�teni az azonos�t�t.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
