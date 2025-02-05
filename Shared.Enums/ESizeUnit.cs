using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

/// <summary>
/// Az adatmennyiség mértékegységeit tartalmazó enum.
/// </summary>
public enum ESizeUnit
{
    /// <summary>
    /// Bájt
    /// </summary>
    [Display(Name = "Bájt")]
    B = 1,

    /// <summary>
    /// Kilobájt
    /// </summary>
    [Display(Name = "Kilobájt")]
    KB = 1024,

    /// <summary>
    /// Megabájt
    /// </summary>
    [Display(Name = "Megabájt")]
    MB = 1048576
}
