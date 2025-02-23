using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

/// <summary>
/// Az algoritmusok neveit tartalmazó enum.
/// </summary>
public enum EAlgorithmName
{
    /// <summary>
    /// AES
    /// </summary>
    [Display(Name = "AES")]
    Aes = 1,

    /// <summary>
    /// DES
    /// </summary>
    [Display(Name = "DES")]
    Des = 2,

    /// <summary>
    /// MathCrypt
    /// </summary>
    [Display(Name = "MathCrypt")]
    MathCrypt = 3,

    /// <summary>
    /// RC2
    /// </summary>
    [Display(Name = "RC2")]
    Rc2 = 4,

    /// <summary>
    /// RSA
    /// </summary>
    [Display(Name = "RSA")]
    Rsa = 5,

    /// <summary>
    /// TripleDES
    /// </summary>
    [Display(Name = "TripleDES")]
    TripleDes = 6
}
