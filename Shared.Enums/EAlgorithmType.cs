using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

/// <summary>
/// Az algoritmusok típusait tartalmazó enum.
/// </summary>
public enum EAlgorithmType
{
    /// <summary>
    /// Aszimmetrikus
    /// </summary>
    [Display(Name = "Aszimmetrikus")]
    Asymmetric = 1,

    /// <summary>
    /// Hasító
    /// </summary>
    [Display(Name = "Hasító")]
    Hashing = 2,

    /// <summary>
    /// Szimmetrikus
    /// </summary>
    [Display(Name = "Szimmetrikus")]
    Symmetric = 3
}
