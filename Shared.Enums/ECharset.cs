using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

/// <summary>
/// A karakterkészleteket tartalamzó enum.
/// </summary>
public enum ECharset
{
    /// <summary>
    /// Alapértelmezett
    /// </summary>
    [Display(Name = "Alapértelmezett karakterek")]
    Default = 0,

    /// <summary>
    /// Vezérlő karakterek
    /// </summary>
    [Display(Name = "Vezérlő karakterek")]
    ControlCharacters = 1,

    /// <summary>
    /// Írásjelek és szimbólumok
    /// </summary>
    [Display(Name = "Írásjelek és szimbólumok")]
    PunctuationAndSymbols = 2,

    /// <summary>
    /// Számjegyek
    /// </summary>
    [Display(Name = "Számjegyek")]
    Digits = 3,

    /// <summary>
    /// Nagybetűk
    /// </summary>
    [Display(Name = "Nagybetűk")]
    UppercaseLetters = 4,

    /// <summary>
    /// Kisbetűk
    /// </summary>
    [Display(Name = "Kisbetűk")]
    LowercaseLetters = 5
}
