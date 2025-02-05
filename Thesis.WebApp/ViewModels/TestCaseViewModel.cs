using Shared.Enums;
using Shared.Enums.Extensions;
using System.ComponentModel;
using TestResults.Entities;

namespace Thesis.WebApp.ViewModels;

/// <summary>
/// Egy tesztesetet ábrázoló nézetmodell.
/// </summary>
public class TestCaseViewModel
{
    /// <summary>
    /// A teszteset azonosítója.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A teszteset engedélyezve van-e.
    /// </summary>
    [DisplayName("Engedélyezve")]
    public bool Enabled { get; set; }

    /// <summary>
    /// A teszteset bemenete.
    /// </summary>
    [DisplayName("Bemenet")]
    public required string Input { get; set; }

    /// <summary>
    /// A bemenet mérete.
    /// </summary>
    [DisplayName("Méret")]
    public int Size { get; set; }

    /// <summary>
    /// A méret mértékegysége.
    /// </summary>
    [DisplayName("Mértékegység")]
    public ESizeUnit Unit { get; set; }

    /// <summary>
    /// A teszteset entitás explicit átalakítása teszteset nézetmodellé.
    /// </summary>
    /// <param name="testCase">Az átalakítandó teszteset entitás.</param>
    public static explicit operator TestCaseViewModel(TestCase testCase)
    {
        int sizeInBytes;
        ESizeUnit unit;

        if (testCase.Size >= (int)ESizeUnit.MB)
        {
            sizeInBytes = testCase.Size / (int)ESizeUnit.MB;
            unit = ESizeUnit.MB;
        }
        else if (testCase.Size >= (int)ESizeUnit.KB)
        {
            sizeInBytes = testCase.Size / (int)ESizeUnit.KB;
            unit = ESizeUnit.KB;
        }
        else
        {
            sizeInBytes = testCase.Size;
            unit = ESizeUnit.B;
        }

        return new TestCaseViewModel
        {
            Id = testCase.Id,
            Enabled = testCase.Enabled,
            Input = testCase.Input,
            Size = sizeInBytes,
            Unit = unit
        };
    }
}
