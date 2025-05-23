﻿using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy tesztesetet ábrázoló osztály.
/// </summary>
public class TestCase
{
    /// <summary>
    /// A teszteset azonosítója.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// A teszteset engedélyezve van-e.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// A teszteset bemenete.
    /// </summary>
    [Required]
    public required string Input { get; set; }

    /// <summary>
    /// A teszteset mérete bájtban.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// A tesztesethez tartozó teszteredmények navigációs tulajdonsága.
    /// </summary>
    public virtual List<TestResult>? TestResults { get; set; }
}
