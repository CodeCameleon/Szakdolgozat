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
    /// ECIES
    /// </summary>
    [Display(Name = "ECIES")]
    Ecies = 4,

    /// <summary>
    /// ElGamal
    /// </summary>
    [Display(Name = "ElGamal")]
    ElGamal = 5,

    /// <summary>
    /// Keccak-256
    /// </summary>
    [Display(Name = "Keccak-256")]
    Keccak256 = 6,

    /// <summary>
    /// MathCrypt
    /// </summary>
    [Display(Name = "MathCrypt")]
    MathCrypt = 7,

    /// <summary>
    /// RC2
    /// </summary>
    [Display(Name = "RC2")]
    Rc2 = 8,

    /// <summary>
    /// RIPEMD-160
    /// </summary>
    [Display(Name = "RIPEMD-160")]
    Ripemd160 = 9,

    /// <summary>
    /// RSA
    /// </summary>
    [Display(Name = "RSA")]
    Rsa = 10,

    /// <summary>
    /// SHA-256
    /// </summary>
    [Display(Name = "SHA-256")]
    Sha256 = 11,

    /// <summary>
    /// TripleDES
    /// </summary>
    [Display(Name = "TripleDES")]
    TripleDes = 12
}
