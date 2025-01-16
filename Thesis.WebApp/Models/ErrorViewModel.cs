namespace Thesis.WebApp.Models;

/// <summary>
/// Egy hibaüzenetet ábrázoló nézetmodell.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// A hibás kérés azonosítója.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Meg kell-e jeleníteni az azonosítót.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
