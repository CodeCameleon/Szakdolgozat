using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shared.Enums;
using Shared.Constants;
using TestResults.Services.Interfaces;
using TestResults.Entities;
using System.Text;
using Thesis.WebApp.Constants;

namespace Thesis.WebApp.ViewModels;

/// <summary>
/// Egy tesztesetet létrehozó nézetmodell.
/// </summary>
public class TestCaseCreateViewModel
    : IValidatableObject
{
    /// <summary>
    /// A teszteset bemenetének minimális mérete.
    /// </summary>
    private const int SizeMin = 1;

    /// <summary>
    /// A teszteset bemenetének maximális mérete.
    /// </summary>
    private const int SizeMax = 1024;

    /// <summary>
    /// A teszteset engedélyezve van-e.
    /// </summary>
    [DisplayName(DisplayNames.TestCaseViewModel.Enabled)]
    public bool Enabled { get; set; }

    /// <summary>
    /// A bemenet mérete.
    /// </summary>
    [DisplayName(DisplayNames.TestCaseViewModel.Size)]
    public int? Size { get; set; }

    /// <summary>
    /// A méret mértékegysége.
    /// </summary>
    [DisplayName(DisplayNames.TestCaseViewModel.Unit)]
    public ESizeUnit? Unit { get; set; }

    /// <summary>
    /// A használható karakterkészletek listája.
    /// </summary>
    [DisplayName(DisplayNames.TestCaseViewModel.Charsets)]
    public List<ECharset>? Charsets { get; set; }

    /// <summary>
    /// A teszteset bemenete.
    /// </summary>
    [DisplayName(DisplayNames.TestCaseViewModel.Input)]
    public string? Input { get; set; }

    /// <summary>
    /// A tesztesetet létrehozó nézetmodel explicit átalakítása teszteset entitássá.
    /// </summary>
    /// <param name="testCaseCreateViewModel">Az átalakítandó tesztesetet létrehozó nézetmodell.</param>
    public static explicit operator TestCase(TestCaseCreateViewModel testCaseCreateViewModel)
    {
        if (string.IsNullOrEmpty(testCaseCreateViewModel.Input))
        {
            throw new NullReferenceException(ErrorMessages.TestCaseInputEmpty);
        }

        if (testCaseCreateViewModel.Size.HasValue && testCaseCreateViewModel.Unit.HasValue)
        {
            return new TestCase
            {
                Enabled = testCaseCreateViewModel.Enabled,
                Input = testCaseCreateViewModel.Input,
                Size = testCaseCreateViewModel.Size.Value * (int)testCaseCreateViewModel.Unit.Value
            };
        }
        else
        {
            return new TestCase
            {
                Enabled = testCaseCreateViewModel.Enabled,
                Input = testCaseCreateViewModel.Input,
                Size = Encoding.UTF8.GetByteCount(testCaseCreateViewModel.Input)
            };
        }
    }

    /// <summary>
    /// Leellenőrzi, hogy a nézetmodell megfelelően van-e kitöltve.
    /// </summary>
    /// <param name="validationContext">A validálás kontextusa.</param>
    /// <returns>A validációs hibák.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ITestCaseService testCaseService = validationContext.GetRequiredService<ITestCaseService>();
        int inputMaxSize = GlobalConfiguration.TestCaseInputMaxSize;

        if (string.IsNullOrEmpty(Input) && !Size.HasValue && !Unit.HasValue && (Charsets == null || Charsets.Count == 0))
        {
            yield return new ValidationResult(ErrorMessages.Required.InputOrDetails);
        }
        else if (string.IsNullOrEmpty(Input))
        {
            if (!Size.HasValue)
            {
                yield return new ValidationResult(ErrorMessages.Required.Size, [nameof(Size)]);
            }
            else if (Size <= SizeMin || SizeMax <= Size)
            {
                yield return new ValidationResult(ErrorMessages.SizeIsOutOfRange(SizeMin, SizeMax), [nameof(Size)]);
            }
            else if (Unit.HasValue && Size.Value * (int)Unit.Value > inputMaxSize)
            {
                yield return new ValidationResult(ErrorMessages.InputTooBig, [nameof(Size), nameof(Unit)]);
            }

            if (!Unit.HasValue)
            {
                yield return new ValidationResult(ErrorMessages.Required.Unit, [nameof(Unit)]);
            }

            if (Charsets == null || Charsets.Count == 0)
            {
                yield return new ValidationResult(ErrorMessages.Required.Charsets, [nameof(Charsets)]);
            }
        }
        else
        {
            int inputSize = Encoding.UTF8.GetByteCount(Input);

            if (inputSize > inputMaxSize)
            {
                yield return new ValidationResult(ErrorMessages.InputTooBig, [nameof(Input)]);
            }

            if (Size.HasValue && Unit.HasValue)
            {
                int sizeInBytes = Size.Value * (int)Unit.Value;

                if (inputSize > sizeInBytes)
                {
                    yield return new ValidationResult(ErrorMessages.TestCaseInputCantBeBiggerThenSize, [nameof(Input)]);
                }

                if (testCaseService.ExistsAsync(Input, sizeInBytes).Result)
                {
                    yield return new ValidationResult(ErrorMessages.TestCaseInputExists, [nameof(Input)]);
                }
            }
        }
    }
}
