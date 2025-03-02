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
    /// BLAKE2B
    /// </summary>
    [Display(Name = "BLAKE2B")]
    Blake2b = 2,

    /// <summary>
    /// DES
    /// </summary>
    [Display(Name = "DES")]
    Des = 3,

    /// <summary>
    /// Keccak-256
    /// </summary>
    [Display(Name = "Keccak-256")]
    Keccak256 = 4,

    /// <summary>
    /// MathCrypt
    /// </summary>
    [Display(Name = "MathCrypt")]
    MathCrypt = 5,

    /// <summary>
    /// RC2
    /// </summary>
    [Display(Name = "RC2")]
    Rc2 = 6,

    /// <summary>
    /// RIPEMD-160
    /// </summary>
    [Display(Name = "RIPEMD-160")]
    Ripemd160 = 7,

    /// <summary>
    /// RSA
    /// </summary>
    [Display(Name = "RSA")]
    Rsa = 8,

    /// <summary>
    /// SHA-256
    /// </summary>
    [Display(Name = "SHA-256")]
    Sha256 = 9,

    /// <summary>
    /// TripleDES
    /// </summary>
    [Display(Name = "TripleDES")]
    TripleDes = 10
}
