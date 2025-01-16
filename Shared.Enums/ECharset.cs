namespace Shared.Enums;

/// <summary>
/// A karakterkészleteket tartalamzó enum.
/// </summary>
public enum ECharset
{
    /// <summary>
    /// Alapértelmezett
    /// </summary>
    Default = 0,

    /// <summary>
    /// Vezérlőkarakterek
    /// </summary>
    ControlCharacters = 1,

    /// <summary>
    /// Írásjelek és szimbólumok
    /// </summary>
    PunctuationAndSymbols = 2,

    /// <summary>
    /// Számjegyek
    /// </summary>
    Digits = 3,

    /// <summary>
    /// Nagybetűk
    /// </summary>
    UppercaseLetters = 4,

    /// <summary>
    /// Kisbetűk
    /// </summary>
    LowercaseLetters = 5
}
